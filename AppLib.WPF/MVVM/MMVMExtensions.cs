using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// MVVM Extension methoods
    /// </summary>
    public static class MVVMExtensions
    {
        /// <summary>
        /// Attach a ViewModel
        /// </summary>
        /// <typeparam name="T">Type of ViewModel</typeparam>
        /// <param name="window">Window</param>
        /// <param name="ViewModel">ViewModel to set</param>
        public static void SetViewModel<T>(this Window window, T ViewModel)
        {
            window.DataContext = ViewModel;
        }

        /// <summary>
        /// Attach a ViewModel
        /// </summary>
        /// <typeparam name="T">Type of ViewModel</typeparam>
        /// <param name="control">UserControl</param>
        /// <param name="ViewModel">ViewModel to set</param>
        public static void SetViewModel<T>(this UserControl control, T ViewModel)
        {
            control.DataContext = ViewModel;
        }

        /// <summary>
        /// Get ViewModel
        /// </summary>
        /// <typeparam name="T">Type of ViewModel</typeparam>
        /// <param name="control">UserControl</param>
        /// <returns>ViewModel</returns>
        public static T GetViewModel<T>(this UserControl control)
        {
            return (T)control.DataContext;
        }

        /// <summary>
        /// Get ViewModel
        /// </summary>
        /// <typeparam name="T">Type of ViewModel</typeparam>
        /// <param name="window">UserControl</param>
        /// <returns>ViewModel</returns>
        public static T GetViewModel<T>(this Window window)
        {
            return (T)window.DataContext;
        }

        /// <summary>
        /// Execute an action when the associated viewmodel for the control is not null
        /// </summary>
        /// <typeparam name="T">Type of viewmodel</typeparam>
        /// <param name="w">Window</param>
        /// <param name="action">Action to perform</param>
        public static void ViewModelAction<T>(this Window w, Action<T> action) where T : class, INotifyPropertyChanged
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
        public static void ViewModelAction<T>(this UserControl w, Action<T> action) where T : class, INotifyPropertyChanged
        {
            var viewmodel = GetViewModel<T>(w);
            if (viewmodel != null)
                action?.Invoke(viewmodel);
        }
    }
}
