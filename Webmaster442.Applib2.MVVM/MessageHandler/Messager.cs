using System;
using System.Collections.Generic;
using System.Linq;
using Webmaster442.Applib.Extensions;

namespace Webmaster442.Applib.MessageHandler
{
    /// <summary>
    /// A class implementing message sending
    /// </summary>
    public class Messenger
    {
        private readonly List<Handler> _handlers;

        private static Messenger _instance;

        /// <summary>
        /// Gets the current instance of the MessageSender
        /// </summary>
        public static Messenger Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Messenger();
                return _instance;
            }
        }

        private Messenger()
        {
            _handlers = new List<Handler>();
        }

        /// <summary>
        /// Subscribe to message notifications
        /// </summary>
        /// <param name="subscriber">subscriber</param>
        public void SubScribe(IMessageClient subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            lock (_handlers)
            {
                if (_handlers.Any(h => h.IsTargetFor(subscriber)))
                    return;

                _handlers.Add(new Handler(subscriber));
            }
        }

        /// <summary>
        /// UnSubscribe from message notifications
        /// </summary>
        /// <param name="subscriber">UnSubscriber</param>
        public void UnSubscribe(IMessageClient subscriber)
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
        public bool SendMessage(Guid target, object message)
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

        public bool SendMessage(Type target, Guid id, object message)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (id == null)
                throw new ArgumentNullException(nameof(id));

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            ClearDeadHandlers();

            var targethandler = (from h in _handlers
                                 where h.HasUid(id) &&
                                       h.IsTypeof(target)
                                 select h).FirstOrDefault();

            if (targethandler == null)
                return false;

            return targethandler.CallHandler(message);
        }

        /// <summary>
        /// Send a message to the specific type of targets
        /// </summary>
        /// <param name="targettype">Target type</param>
        /// <param name="message">Message to send</param>
        /// <returns>true, if the sending was successfull to all clients</returns>
        public bool SendMessage(Type targettype, object message)
        {
            if (targettype == null)
                throw new ArgumentNullException(nameof(targettype));

            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var targethandler = from h in _handlers
                                where h.IsTypeof(targettype)
                                select h;

            var res = true;
            foreach (var target in targethandler)
            {
                if (!target.CallHandler(message))
                    res = false;
                
            }
            return res;
        }

        /// <summary>
        /// Send a message to every subscriber that can handle the message
        /// </summary>
        /// <param name="message">Message to send</param>
        /// <returns>true, if the sending was successfull to all clients</returns>
        public bool SendMessage(object message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            ClearDeadHandlers();
            var res = true;
            foreach (var h in _handlers)
            {
                if (!h.CallHandler(message))
                    res = false;
            }
            return res;
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
