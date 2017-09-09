using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// Get ViewModel extensions
    /// </summary>
    public static class GetViewModelExtensions
    {
        /// <summary>
        /// Get the ViewModel associated with a Window
        /// </summary>
        /// <typeparam name="T">ViewModel Type</typeparam>
        /// <param name="w">Window</param>
        /// <returns>ViewModel associated with a Window</returns>
        public static T GetViewModel<T>(this Window w) where T: class, INotifyPropertyChanged, new()
        {
            if (w.DataContext == null)
                return default(T);

            return w.DataContext as T;
        }


        /// <summary>
        /// Get the ViewModel associated with a usercontrol
        /// </summary>
        /// <typeparam name="T">ViewModel Type</typeparam>
        /// <param name="ctrl">User control</param>
        /// <returns>ViewModel associated with a usercontrol</returns>
        public static T GetViewModel<T>(this UserControl ctrl) where T : class, INotifyPropertyChanged, new()
        {
            if (ctrl.DataContext == null)
                return default(T);

            return ctrl.DataContext as T;
        }

        /// <summary>
        /// Execute an action when the associated viewmodel for the control is not null
        /// </summary>
        /// <typeparam name="T">Type of viewmodel</typeparam>
        /// <param name="w">Window</param>
        /// <param name="action">Action to perform</param>
        public static void ViewModelAction<T>(this Window w, Action<T> action) where T : class, INotifyPropertyChanged, new()
        {
            var viewmodel = GetViewModel<T>(w);
            if (viewmodel != null)
                action?.Invoke(viewmodel);
        }

        /// <summary>
        /// Execute an action when the associated viewmodel for the control is not null
        /// </summary>
        /// <typeparam name="T">Type of viewmodel</typeparam>
        /// <param name="w">UserControl</param>
        /// <param name="action">Action to perform</param>
        public static void ViewModelAction<T>(this UserControl w, Action<T> action) where T : class, INotifyPropertyChanged, new()
        {
            var viewmodel = GetViewModel<T>(w);
            if (viewmodel != null)
                action?.Invoke(viewmodel);
        }
    }
}
