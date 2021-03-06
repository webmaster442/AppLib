﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Webmaster442.Applib.IoC
{
    /// <summary>
    /// Implementation of IoC container
    /// </summary>
    public sealed class IoCContainer : IIocContainer
    {
        private readonly Dictionary<Type, Delegate> _dictionary;
        private readonly object _lock;

        /// <summary>
        /// Creates a new instance of IoC container
        /// </summary>
        public IoCContainer()
        {
            _dictionary = new Dictionary<Type, Delegate>();
            _lock = new object();
        }

        /// <inheritdoc/>
        public void Register<T>(Func<T> getter)
        {
            lock (_lock)
            {
                _dictionary[typeof(T)] = getter ?? throw new ArgumentNullException("getter");
            }
        }

        /// <inheritdoc/>
        public void Register<TPublic, TImplementation>(ConstructorInfo constructor = null)
        {
            MethodInfo _getMethod = typeof(IoCContainer).GetMethod("Get");

            if (constructor == null)
            {
                var constructors = typeof(TImplementation).GetConstructors();
                if (constructors.Length == 0)
                    throw new InvalidOperationException("The type " + typeof(TImplementation).FullName + " doesn't have any public constructor.");

                if (constructors.Length == 1)
                    constructor = constructors[0];
                else
                {
                    var maxParameters = constructors.Select(c => c.GetParameters().Length).Max();

                    constructor = constructors.Where(c => c.GetParameters().Length == maxParameters).Single();
                }
            }

            var iocContainerExpression = Expression.Constant(this);

            var parameters = constructor.GetParameters();
            int parameterCount = parameters.Length;
            var arguments = new Expression[parameterCount];
            for (int i = 0; i < parameterCount; i++)
            {
                var parameter = parameters[i];
                var typedGet = _getMethod.MakeGenericMethod(parameter.ParameterType);
                var argument = Expression.Call(iocContainerExpression, typedGet);
                arguments[i] = argument;
            }

            var newExpression = Expression.New(constructor, arguments);
            var lamda = Expression.Lambda<Func<TPublic>>(newExpression);
            var implementedDelegate = lamda.Compile();

            Register<TPublic>(implementedDelegate);
        }

        /// <inheritdoc/>
        public bool Unregister<T>()
        {
            lock (_lock)
            {
                return _dictionary.Remove(typeof(T));
            }
        }

        private Func<T> TryGetGetter<T>()
        {
            lock (_lock)
            {
                if (!_dictionary.TryGetValue(typeof(T), out Delegate untypedDelegate))
                {
                    var allHandlers = ResolveGet;
                    if (allHandlers != null)
                    {
                        var args = new ResolveGetEventArgs();
                        args.RequestedType = typeof(T);

                        foreach (EventHandler<ResolveGetEventArgs> handler in allHandlers.GetInvocationList())
                        {
                            handler(this, args);
                            if (args.Getter != null)
                            {
                                untypedDelegate = args.Getter;

                                _dictionary.Add(typeof(T), args.Getter);

                                break;
                            }
                        }
                    }
                }

                Func<T> getter = (Func<T>)untypedDelegate;
                return getter;
            }
        }

        /// <inheritdoc/>
        public T Resolve<T>()
        {
            lock (_lock)
            {
                var getter = TryGetGetter<T>();

                if (getter == null)
                    throw new InvalidOperationException("It is not possible to find an instance to the given type: " + typeof(T).FullName);

                return getter();
            }
        }

        /// <inheritdoc/>
        public bool CanResolve<T>()
        {
            lock (_lock)
            {
                var getter = TryGetGetter<T>();
                return getter != null;
            }
        }

        /// <inheritdoc/>
        public event EventHandler<ResolveGetEventArgs> ResolveGet;
    }
}
