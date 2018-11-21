using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Webmaster442.Applib.Panels
{
    /// <summary>
    /// Weighted panel
    /// </summary>
    public class WeightedPanel: Panel
    {
        internal class ChildAndRect
        {
            public UIElement Element { get; set; }
            public Rect Rectangle { get; set; }
        }

        private static FrameworkPropertyMetadata weightMetadata = 
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsParentArrange);

        /// <summary>
        /// Dependency property for setting item weight
        /// </summary>
        public static readonly DependencyProperty WeightProperty =
            DependencyProperty.RegisterAttached("Weight", typeof(double), typeof(WeightedPanel), weightMetadata);

        /// <summary>
        /// setter for weight
        /// </summary>
        /// <param name="depObj"></param>
        /// <param name="value"></param>
        public static void SetWeight(DependencyObject depObj, double value)
        {
            depObj.SetValue(WeightProperty, value);
        }

        /// <inhreitdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            double totalWeight = TotalChildWeight();

            foreach (ChildAndRect child in ChildrenTreemapOrder(InternalChildren.Cast<UIElement>(), availableSize))
                child.Element.Measure(child.Rectangle.Size);

            return availableSize;
        }

        /// <inhreitdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (ChildAndRect child in ChildrenTreemapOrder(InternalChildren.Cast<UIElement>(), finalSize))
                child.Element.Arrange(child.Rectangle);

            return finalSize;
        }

        private double TotalChildWeight()
        {
            double weightSum = 0;
            foreach (UIElement elem in InternalChildren)
                weightSum += (double)elem.GetValue(WeightProperty);

            return weightSum;
        }

        /// <summary>
        /// Return child elements orderd by weight (largest to
        /// smallest), passing back Rect for each child
        /// (size and location), implementing a (crude)
        /// treemap.
        /// </summary>
        /// <param name="elems">Child elements to measure/arrange</param>
        /// <param name="containerSize">Available container size</param>
        /// <returns></returns>
        private IEnumerable<ChildAndRect> ChildrenTreemapOrder(IEnumerable<UIElement> elems, Size containerSize)
        {
            double remainingWeight = TotalChildWeight();

            double top = 0.0;
            double left = 0.0;

            // Alternate between left edge and top edge
            bool leftEdge;

            // Sort by weight
            var childrenByWeight = elems.OrderByDescending(
                e => (double)e.GetValue(WeightProperty));

            // Allocate space for each child, one at a time.
            // Moving left to right, top to bottom
            foreach (var child in childrenByWeight)
            {
                leftEdge = (containerSize.Width - left) > (containerSize.Height - top);

                Size size;

                double childWeight = (double)child.GetValue(WeightProperty);
                double pctArea = childWeight / remainingWeight;
                remainingWeight -= childWeight;

                // Entire height, proportionate width
                if (leftEdge)
                    size = new Size(pctArea * (containerSize.Width - left), containerSize.Height - top);

                // Top edge - Entire width, proportionate height
                else
                    size = new Size(containerSize.Width - left, pctArea * (containerSize.Height - top));

                yield return new ChildAndRect { Element = child, Rectangle = new Rect(new Point(left, top), size) };

                if (leftEdge)
                    left += size.Width;
                else
                    top += size.Height;
            }
        }
    }
}
