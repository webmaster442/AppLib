using System;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using Webmaster442.Applib.Shaders.Transition;
using Webmaster442.Applib.Controls;
using Webmaster442.Applib.Extensions;

namespace Webmaster442.Applib.Internals
{
    internal static class XamlHelper
    {
        public static void ExecuteOnLoaded(FrameworkElement fe, Action action)
        {
            if (fe.IsLoaded)
            {
                action?.Invoke();
            }
            else
            {
                RoutedEventHandler handler = null;
                handler = delegate
                {
                    fe.Loaded -= handler;
                    action?.Invoke();
                };

                fe.Loaded += handler;
            }
        }
    }

    /// <summary>
    /// Transition selector for AnimatedTabControl
    /// </summary>
    public class TabControlTransitionSelector : TransitionSelector
    {
        private readonly Random _random = new Random();

        private Transition[] _transitions;
        private int[] _animcount;

        /// <summary>
        /// Creates a new instance of TabControlTransitionSelector
        /// </summary>
        public TabControlTransitionSelector()
        {
            if (!RenderCapability.IsPixelShaderVersionSupported(2, 0)) return;
            _transitions = new Transition[]
            {
                new TransitionWave(),
                new TransitonSaturate(),
                new TransitionRipple(),
                new TransitionRadialBlur(),
                new TransitionPixelateOut(),
                new TransitionPixelate(),
                new TransitionMostBright(),
                new TransitionLeastBright(),
                new TransitionFold(),
                new TransitionFade(),
                new TransitionCircleStretch(),
                new TransitionBandedSwirl(),
                new TransitionBlinds(),
                new TransitionBlood(),
                new TransitionCircleReveal(),
                new TransitionDrop(),
                new TransitionRadialWiggle(),
                new TransitionRandomCircleReveal(),
                new TransitionSlideIn(TransitionSlideIn.SlideDirection.Down),
                new TransitionSlideIn(TransitionSlideIn.SlideDirection.Left),
                new TransitionSlideIn(TransitionSlideIn.SlideDirection.LeftDown),
                new TransitionSlideIn(TransitionSlideIn.SlideDirection.LeftUp),
                new TransitionSlideIn(TransitionSlideIn.SlideDirection.Right),
                new TransitionSlideIn(TransitionSlideIn.SlideDirection.RightDown),
                new TransitionSlideIn(TransitionSlideIn.SlideDirection.RightUp),
                new TransitionSlideIn(TransitionSlideIn.SlideDirection.Up),
                new TransitionSwirl(),
                new TransitionWater()
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
        public override Transition GetTransition(object oldContent, object newContent, DependencyObject container)
        {
            if (_transitions.Length < 1) return null;
            return _random.NextItem(_transitions);
        }
    }
}