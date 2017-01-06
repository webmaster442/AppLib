using AppLib.Common.PInvoke;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace AppLib.WPF
{
    public class WindowThumbnail : FrameworkElement
    {
        public WindowThumbnail()
        {
            this.LayoutUpdated += new EventHandler(Thumbnail_LayoutUpdated);
            this.Unloaded += new RoutedEventHandler(Thumbnail_Unloaded);
        }

        public static DependencyProperty SourceProperty;
        public static DependencyProperty ClientAreaOnlyProperty;

        static WindowThumbnail()
        {
            SourceProperty = DependencyProperty.Register(
                "Source",
                typeof(IntPtr),
                typeof(WindowThumbnail),
                new FrameworkPropertyMetadata(
                    IntPtr.Zero,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    delegate (DependencyObject obj, DependencyPropertyChangedEventArgs args)
                    {
                        ((WindowThumbnail)obj).InitialiseThumbnail((IntPtr)args.NewValue);
                    }));

            ClientAreaOnlyProperty = DependencyProperty.Register(
                "ClientAreaOnly",
                typeof(bool),
                typeof(WindowThumbnail),
                new FrameworkPropertyMetadata(
                    false,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    delegate (DependencyObject obj, DependencyPropertyChangedEventArgs args)
                    {
                        ((WindowThumbnail)obj).UpdateThumbnail();
                    }));

            OpacityProperty.OverrideMetadata(
                typeof(WindowThumbnail),
                new FrameworkPropertyMetadata(
                    1.0,
                    FrameworkPropertyMetadataOptions.Inherits,
                    delegate (DependencyObject obj, DependencyPropertyChangedEventArgs args)
                    {
                        ((WindowThumbnail)obj).UpdateThumbnail();
                    }));
        }

        public IntPtr Source
        {
            get { return (IntPtr)this.GetValue(SourceProperty); }
            set { this.SetValue(SourceProperty, value); }
        }

        public bool ClientAreaOnly
        {
            get { return (bool)this.GetValue(ClientAreaOnlyProperty); }
            set { this.SetValue(ClientAreaOnlyProperty, value); }
        }

        public new double Opacity
        {
            get { return (double)this.GetValue(OpacityProperty); }
            set { this.SetValue(OpacityProperty, value); }
        }

        private HwndSource target;
        private IntPtr thumb;

        private void InitialiseThumbnail(IntPtr source)
        {
            // release the old thumbnail
            if (IntPtr.Zero != thumb) ReleaseThumbnail();

            if (IntPtr.Zero != source)
            {
                // find our parent hwnd
                target = (HwndSource)HwndSource.FromVisual(this);

                // if we have one, we can attempt to register the thumbnail
                if (target != null && 0 == DwmApi.DwmRegisterThumbnail(target.Handle, source, out thumb))
                    GetThumbnail();
            }
        }

        private void GetThumbnail()
        {
            var props = new DWM_THUMBNAIL_PROPERTIES();
            props.fVisible = false;
            props.fSourceClientAreaOnly = ClientAreaOnly;
            props.opacity = (byte)(255 * Opacity);
            props.dwFlags = dwFlags.DWM_TNP_VISIBLE | dwFlags.DWM_TNP_SOURCECLIENTAREAONLY | dwFlags.DWM_TNP_OPACITY;
            DwmApi.DwmUpdateThumbnailProperties(thumb, ref props);
        }

        private void ReleaseThumbnail()
        {
            DwmApi.DwmUnregisterThumbnail(thumb);
            this.thumb = IntPtr.Zero;
            this.target = null;
        }

        private void UpdateThumbnail()
        {
            if (IntPtr.Zero != thumb) GetThumbnail();
        }

        private void Thumbnail_Unloaded(object sender, RoutedEventArgs e)
        {
            ReleaseThumbnail();
        }

        // this is where the magic happens
        private void Thumbnail_LayoutUpdated(object sender, EventArgs e)
        {
            if (IntPtr.Zero == thumb) InitialiseThumbnail(Source);

            if (IntPtr.Zero != thumb)
            {
                if (!target.RootVisual.IsAncestorOf(this))
                {
                    //we are no longer in the visual tree
                    ReleaseThumbnail();
                    return;
                }

                GeneralTransform transform = TransformToAncestor(target.RootVisual);
                Point a = transform.Transform(new Point(0, 0));
                Point b = transform.Transform(new Point(ActualWidth, ActualHeight));

                var props = new DWM_THUMBNAIL_PROPERTIES();
                props.fVisible = true;
                props.rcDestination = new Common.PInvoke.Rect(
                    (int)Math.Ceiling(a.X),
                    (int)Math.Ceiling(a.Y),
                    (int)Math.Ceiling(b.X),
                    (int)Math.Ceiling(b.Y));
                props.dwFlags = dwFlags.DWM_TNP_VISIBLE | dwFlags.DWM_TNP_RECTDESTINATION;
                DwmApi.DwmUpdateThumbnailProperties(thumb, ref props);
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            SIZE size;
            DwmApi.DwmQueryThumbnailSourceSize(thumb, out size);
            double scale = 1;
            // our preferred size is the thumbnail source size
            // if less space is available, we scale appropriately
            if (size.cX > availableSize.Width)
                scale = availableSize.Width / size.cX;
            if (size.cY > availableSize.Height)
                scale = Math.Min(scale, availableSize.Height / size.cY);

            return new Size(size.cX * scale, size.cY * scale); ;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            SIZE size;
            DwmApi.DwmQueryThumbnailSourceSize(thumb, out size);
            // scale to fit whatever size we were allocated
            double scale = finalSize.Width / size.cX;
            scale = Math.Min(scale, finalSize.Height / size.cY);
            return new Size(size.cX * scale, size.cY * scale);
        }
    }
}
