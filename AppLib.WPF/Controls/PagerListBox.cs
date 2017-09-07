using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// A Listbox with paging and Quick jump menu
    /// </summary>
    public class PagerListBox: ListBox
    {
        private FaImageButton BtnUp;
        private FaImageButton BtnDown;
        private FaImageButton BtnContext;
        private ListBox Internal;


        static PagerListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PagerListBox), new FrameworkPropertyMetadata(typeof(PagerListBox)));
        }

        /// <summary>
        /// Applies the template
        /// </summary>
        public override void OnApplyTemplate()
        {
            BtnUp = GetTemplateChild("BtnUp") as FaImageButton;
            BtnDown = GetTemplateChild("BtnDown") as FaImageButton;
            BtnContext = GetTemplateChild("BtnContext") as FaImageButton;
            Internal = GetTemplateChild("Internal") as ListBox;

            if (Internal != null)
                ((INotifyCollectionChanged)Internal.Items).CollectionChanged += ListView_CollectionChanged;

            if (BtnUp != null)
                BtnUp.Click += BtnUp_Click;
            if (BtnDown != null)
                BtnDown.Click += BtnDown_Click;
            if (BtnContext != null)
            {
                BtnContext.ContextMenu = new ContextMenu();
                BtnContext.Click += BtnContext_Click;
            }

            base.OnApplyTemplate();
        }

        private void ListView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BtnContext.ContextMenu.Items.Clear();
            var items = (from object item in Internal.Items
                         select item.ToString().ToUpper()[0]).Distinct().OrderBy(c => c);

            foreach (var item in items)
            {
                var menu = new MenuItem();
                menu.Header = item;
                menu.Click += Menu_Click;

            }

        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnContext_Click(object sender, RoutedEventArgs e)
        {
            if (BtnContext.ContextMenu.Items.Count > 0)
                BtnContext.ContextMenu.IsOpen = true;
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            if (Internal == null)
                return;
            if (Internal.Items.Count > 0)
                Internal.ScrollIntoView(Internal.Items.Count - 1);
        }

        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            if (Internal == null)
                return;
            if (Internal.Items.Count > 0)
                Internal.ScrollIntoView(Internal.Items[0]);
        }
    }
}
