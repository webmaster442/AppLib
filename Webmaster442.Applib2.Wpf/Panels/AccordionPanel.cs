using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Webmaster442.Applib.Panels
{
    /// <summary>
    /// Accordion like panel
    /// Has a special behaviour for expanders: Only one can be expanded at a time
    /// </summary>
    public class AccordionPanel : Panel
    {
        /// <summary>
        /// Creates a new instance of AccordionPanel
        /// </summary>
        public AccordionPanel()
        {
            AddHandler(Expander.ExpandedEvent, new RoutedEventHandler(ChildExpanded));
        }

        private Expander FindExpander(UIElement e)
        {
            while (e != null && !(e is Expander))
            {
                if (VisualTreeHelper.GetChildrenCount(e) == 1)
                    e = VisualTreeHelper.GetChild(e, 0) as UIElement;
                else
                    e = null;
            }
            return (Expander)e;
        }

        private void ChildExpanded(object sender, RoutedEventArgs e)
        {
            foreach (UIElement child in InternalChildren)
            {
                var expander = FindExpander(child);

                if (expander != null && expander != e.OriginalSource)
                {
                    expander.IsExpanded = false;
                }
            }
        }

        private bool CanResize(UIElement e)
        {
            Expander expander = FindExpander(e);
            return expander != null && expander.IsExpanded;
        }

        /// <summary>
        /// Overrides <see cref="FrameworkElement.MeasureOverride(Size)"/>
        /// </summary>
        /// <param name="availableSize">Avaliable size</param>
        /// <returns>size to render to</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            double requiredHeight = 0;
            double requiredWidth = 0;
            double resizableHeight = 0;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);
                requiredHeight += child.DesiredSize.Height;
                if (child.DesiredSize.Width > requiredWidth)
                    requiredWidth = child.DesiredSize.Width;

                if (CanResize(child))
                {
                    resizableHeight += child.DesiredSize.Height;
                }
            }

            if (requiredHeight > availableSize.Height)
            {
                double pixelsToLose = requiredHeight - availableSize.Height;

                foreach (UIElement child in InternalChildren)
                {
                    double height = child.DesiredSize.Height;

                    if (CanResize(child))
                    {
                        height -= (child.DesiredSize.Height / resizableHeight) * pixelsToLose;
                        child.Measure(new Size(availableSize.Width, height));

                    }
                }
            }

            return new Size(requiredWidth, requiredHeight);
        }

        /// <summary>
        /// Overrides <see cref="FrameworkElement.ArrangeOverride(Size)"/>
        /// </summary>
        /// <param name="finalSize">Final size</param>
        /// <returns>Rendered size</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            double totalHeight = 0;
            double resizableHeight = 0;

            foreach (UIElement child in Children)
            {
                totalHeight += child.DesiredSize.Height;

                if (CanResize(child))
                {
                    resizableHeight += child.DesiredSize.Height;
                }
            }

            double pixelsToLose = totalHeight - finalSize.Height;
            double y = 0;

            foreach (UIElement child in InternalChildren)
            {
                double height = child.DesiredSize.Height;

                if (pixelsToLose > 0 && CanResize(child))
                {
                    height -= (child.DesiredSize.Height / resizableHeight) * pixelsToLose;
                }

                child.Arrange(new Rect(0, y, finalSize.Width, height));
                y += height;
            }

            return base.ArrangeOverride(finalSize);
        }
    }
}
