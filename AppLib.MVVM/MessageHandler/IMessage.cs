using System;

namespace AppLib.MVVM.MessageHandler
{
    /// <summary>
    /// Message Target interface
    /// </summary>
    public interface IMessageClient
    {
        /// <summary>
        /// Message reciever ID;
        /// </summary>
        Guid MessageReciverID { get; }
    }

    /// <summary>
    /// Typed Message target interface
    /// </summary>
    /// <typeparam name="Tmsg">Message type</typeparam>
    public interface IMessageClient<Tmsg>: IMessageClient
    {
        /// <summary>
        /// Handler for a message
        /// </summary>
        /// <param name="message">Message to handle</param>
        void HandleMessage(Tmsg message);
    }
}
