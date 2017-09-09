namespace AppLib.WPF.MVVM
{
    /// <summary>
    /// An Interface for view Implementation
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Close the view
        /// </summary>
        void Close();
    }

    /// <summary>
    /// A ViewModel with reference to the view
    /// </summary>
    public class ViewModel<IViewType>: BindableBase where IViewType : IView
    {
        /// <summary>
        /// View associated with the viewmodel
        /// </summary>
        public IViewType View { get; set; }
    }
}
