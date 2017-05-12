namespace AppLib.Common.MessageHandler
{
    /// <summary>
    /// Message Target interface
    /// </summary>
    public interface IMessageTarget
    {
        /// <summary>
        /// Message reciever ID;
        /// </summary>
        UId MessageReciverID { get; }
    }

    /// <summary>
    /// Typed Message target interface
    /// </summary>
    /// <typeparam name="Tmsg">Message type</typeparam>
    public interface IMessageTarget<Tmsg>: IMessageTarget
    {
        /// <summary>
        /// Handler for a message
        /// </summary>
        /// <param name="message">Message to handle</param>
        void HandleMessage(Tmsg message);
    }
}
