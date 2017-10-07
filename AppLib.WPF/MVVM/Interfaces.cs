namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// A generic View Interface
    /// </summary>
    /// <typeparam name="ViewModelType">ViewModel type</typeparam>
    public interface IView<ViewModelType>
    {
        /// <summary>
        /// Get or set the ViewModel
        /// </summary>
        ViewModelType ViewModel { get; set; }
    }

    /// <summary>
    /// A View that can be cloded
    /// </summary>
    /// <typeparam name="ViewModelType">ViewModel type</typeparam>
    public interface ICloseableView<ViewModelType>: IView<ViewModelType>
    {
        /// <summary>
        /// Close the view
        /// </summary>
        void Close();
    }
}
