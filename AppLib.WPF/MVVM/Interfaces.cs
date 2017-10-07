namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// A generic View Interface
    /// </summary>
    public interface IView
    {
    }

    /// <summary>
    /// A View that can be cloded
    /// </summary>
    public interface ICloseableView<ViewModelType>: IView
    {
        /// <summary>
        /// Close the view
        /// </summary>
        void Close();
    }
}
