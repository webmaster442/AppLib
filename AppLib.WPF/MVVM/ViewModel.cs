using System.ComponentModel;
using System.Windows;

namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// View Model Implementation class
    /// </summary>
    /// <typeparam name="ViewType">View Type</typeparam>
    public abstract class ViewModel<ViewType>: BindableBase where ViewType: IView<ViewModel<ViewType>>
    {
        /// <summary>
        /// Gets wheather the current viewmodel is in designer mode or not
        /// </summary>
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        ViewType View
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of the ViewModel
        /// </summary>
        /// <param name="view">View to inject</param>
        public ViewModel(ViewType view)
        {
            View = view;
            view.ViewModel = this;
        }
    }
}
