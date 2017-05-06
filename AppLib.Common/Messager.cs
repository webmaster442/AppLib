using System.Collections.Generic;
using System.Linq;

namespace AppLib.Common
{
    /// <summary>
    /// A simple class for sending and recieving messages
    /// </summary>
    public sealed class Messager
    {
        private static Messager _instance;
        private HashSet<IMessageTarget> _targets;

        private Messager()
        {
            _targets = new HashSet<IMessageTarget>();
        }

        /// <summary>
        /// Gets the current instance of the messager
        /// </summary>
        public static Messager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Messager();
                return _instance;
            }
        }

        public void Send(UId sender, UId target, object content)
        {
            var m = new Message
            {
                Sender = sender,
                Target = target,
                Content = content
            };
            var q = (from i in _targets
                     where i.Identifier == target
                     select i).FirstOrDefault();
            if (q != null)
                q.HandleMessage(m);
        }

        public void Brodecast(UId sender, object content)
        {
            var m = new Message
            {
                Sender = sender,
                Target = null,
                Content = content
            };

            foreach (var target in _targets)
                target.HandleMessage(m);
        }

        public void Subscribe(IMessageTarget target)
        {
            _targets.Add(target);
        }

        public void Unsubscribe(IMessageTarget target)
        {
            _targets.Remove(target);
        }
    }

    public class Message
    {
        /// <summary>
        /// Identifier of sender
        /// </summary>
        public UId Sender { get; set; }
        /// <summary>
        /// Identifier of target
        /// </summary>
        public UId Target { get; set; }

        /// <summary>
        /// Message Content
        /// </summary>
        public object Content { get; set; }
    }

    /// <summary>
    /// Message target interface
    /// </summary>
    public interface IMessageTarget
    {
        /// <summary>
        /// 
        /// </summary>
        UId Identifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        void HandleMessage(Message m);
    }
}
