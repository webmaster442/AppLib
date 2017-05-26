using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AppLib.Common.Extensions;

namespace AppLib.Common.MessageHandler
{
    internal class Handler
    {
        private readonly WeakReference _wref;
        private readonly Dictionary<Type, MethodInfo> _supported;

        public Handler(IMessageTarget o)
        {
            _wref = new WeakReference(o);
            _supported = new Dictionary<Type, MethodInfo>();
            Inspect(o);
        }

        private void Inspect(IMessageTarget o)
        {
            var ifaces = o.GetType().GetInterfaces()
                .Where(x => typeof(IMessageTarget).IsAssignableFrom(x) && x.IsGenericType);

            foreach (var iface in ifaces)
            {
                var t = iface.GetGenericArguments()[0];
                var m = iface.GetMethod("HandleMessage");
                _supported.AddOrUpdate(t, m);
            }

        }

        public bool CallHandler(object param)
        {
            if (param == null)
                throw new ArgumentException(nameof(param));

            if (_wref.Target == null)
                return false;

            var t = param.GetType();

            foreach (var supported in _supported)
            {
                if (supported.Key.IsAssignableFrom(t))
                {
                    supported.Value.Invoke(_wref.Target, new[] { param });
                    return true;
                }
            }
            return false;
        }

        public bool IsTargetNull
        {
            get { return _wref.Target == null; }
        }

        public bool IsTargetFor(IMessageTarget o)
        {
            return _wref.Target == o;
        }


        public bool HasUid(UId search)
        {
            if (_wref.Target == null)
                return false;
            return (_wref.Target as IMessageTarget).MessageReciverID == search;
        }
    }
}
