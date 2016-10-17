using Microsoft.Expression.Media.Effects;
using System;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using AppLib.Common.Extensions;

namespace AppLib.WPF.Controls
{
    internal static class XamlHelper
    {
        public static void ExecuteOnLoaded(FrameworkElement fe, Action action)
        {
            if (fe.IsLoaded)
            {
                if (action != null)
                {
                    action();
                }
            }
            else
            {
                RoutedEventHandler handler = null;
                handler = delegate
                {
                    fe.Loaded -= handler;

                    if (action != null)
                    {
                        action();
                    }
                };

                fe.Loaded += handler;
            }
        }
    }

    /// <summary>
    /// Abstract class that provides transition selecton
    /// </summary>
    public abstract class TransitionSelector
    {
        /// <summary>
        /// Provides Transition function for the TransitionControl
        /// </summary>
        /// <param name="oldContent">old content stored in the control</param>
        /// <param name="newContent">new content to be presented</param>
        /// <param name="container">container of the items</param>
        /// <returns>A transitionEffect</returns>
        public abstract TransitionEffect GetTransition(object oldContent, object newContent, DependencyObject container);
    }

    /// <summary>
    /// Transition selector for AnimatedTabControl
    /// </summary>
    public class TabControlTransitionSelector : TransitionSelector
    {
        private readonly Random _random = new Random();

        private TransitionEffect[] _transitions;
        private int[] _animcount;

        /// <summary>
        /// Creates a new instance of TabControlTransitionSelector
        /// </summary>
        public TabControlTransitionSelector()
        {
            if (!RenderCapability.IsPixelShaderVersionSupported(2, 0)) return;
            _transitions = new TransitionEffect[]
            {
                new SmoothSwirlGridTransitionEffect(),
                new BlindsTransitionEffect(),
                new CircleRevealTransitionEffect(),
                new CloudRevealTransitionEffect(),
                new FadeTransitionEffect(),
                new PixelateTransitionEffect(),
                new RadialBlurTransitionEffect(),
                new RippleTransitionEffect(),
                new WaveTransitionEffect(),
                new WipeTransitionEffect(),
                new SmoothSwirlGridTransitionEffect(),
                new SlideInTransitionEffect { SlideDirection = SlideDirection.TopToBottom },
                new SlideInTransitionEffect { SlideDirection = SlideDirection.LeftToRight },
                new SlideInTransitionEffect { SlideDirection = SlideDirection.RightToLeft },
                new SlideInTransitionEffect { SlideDirection = SlideDirection.BottomToTop }
            };
            _transitions = (from i in _transitions orderby _random.Next() select i).ToArray();
            _animcount = new int[_transitions.Length];
        }

        /// <summary>
        /// Provides Transition function for the TransitionControl
        /// </summary>
        /// <param name="oldContent">old content stored in the control</param>
        /// <param name="newContent">new content to be presented</param>
        /// <param name="container">container of the items</param>
        /// <returns>A transitionEffect</returns>
        public override TransitionEffect GetTransition(object oldContent, object newContent, DependencyObject container)
        {
            if (_transitions.Length < 1) return null;
            return _random.NextItem(_transitions);
        }
    }
}