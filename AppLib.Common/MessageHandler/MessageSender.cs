using AppLib.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppLib.Common.MessageHandler
{
    /// <summary>
    /// A class implementing message sending
    /// </summary>
    public class MessageSender
    {
        private readonly List<Handler> _handlers;

        private static MessageSender _instance;

        /// <summary>
        /// Gets the current instance of the MessageSender
        /// </summary>
        public static MessageSender Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MessageSender();
                return _instance;
            }
        }

        private MessageSender()
        {
            _handlers = new List<Handler>();
        }

        /// <summary>
        /// Subscribe to message notifications
        /// </summary>
        /// <param name="subscriber">subscriber</param>
        public void SubScribe(IMessageTarget subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            lock (_handlers)
            {
                if (_handlers.Any(h => h.IsTargetFor(subscriber)))
                    return;
            }
        }

        /// <summary>
        /// UnSubscribe from message notifications
        /// </summary>
        /// <param name="subscriber">UnSubscriber</param>
        public void UnSubscribe(IMessageTarget subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            lock (_handlers)
            {
                var h = _handlers.FirstOrDefault(hndl => hndl.IsTargetFor(subscriber));
                if (h != null)
                    _handlers.Remove(h);
            }
        }

        /// <summary>
        /// Send a message to a specific target
        /// </summary>
        /// <param name="target">Target uid</param>
        /// <param name="message">message to send</param>
        /// <returns>true, if the sending was successfull</returns>
        public bool SendMessage(UId target, object message)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            ClearDeadHandlers();

            var targethandler = (from h in _handlers
                                 where h.HasUid(target)
                                 select h).FirstOrDefault();

            if (targethandler == null)
                return false;

            return targethandler.CallHandler(message);
        }

        private void ClearDeadHandlers()
        {
            var remove = (from handler in _handlers
                          where handler.IsTargetNull
                          select handler).ToStack();


            if (remove.Count > 0)
            {
                lock (_handlers)
                {
                    while (remove.Count >0)
                    {
                        _handlers.Remove(remove.Pop());
                    }
                }
            }
        }
    }
}
