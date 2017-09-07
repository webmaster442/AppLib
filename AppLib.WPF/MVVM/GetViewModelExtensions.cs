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
        /// <param name="ctrl">Window</param>
        /// <returns>ViewModel associated with a usercontrol</returns>
        public static T GetViewModel<T>(this UserControl ctrl) where T : class, INotifyPropertyChanged, new()
        {
            if (ctrl.DataContext == null)
                return default(T);

            return ctrl.DataContext as T;
        }
    }
}
