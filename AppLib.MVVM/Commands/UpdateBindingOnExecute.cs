namespace AppLib.MVVM
{
    /// <summary>
    /// Sets the binding direction for binding update
    /// </summary>
    public enum UpdateBindingOnExecute
    {
        /// <summary>
        /// Binding shouldn't be updated
        /// </summary>
        No,
        /// <summary>
        /// Binding should be updated. Target data is copied to the source
        /// </summary>
        Source,
        /// <summary>
        /// Binding should be updated. Source data is copied to the target
        /// </summary>
        Target
    }
}
