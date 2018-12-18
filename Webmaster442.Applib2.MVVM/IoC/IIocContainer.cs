using System;
using System.Reflection;

namespace Webmaster442.Applib.IoC
{
    /// <summary>
    /// Inversion of Control container interface
    /// </summary>
    public interface IIocContainer
    {
        /// <summary>
        /// Register a type into the container
        /// </summary>
        /// <typeparam name="T">Type to regiser</typeparam>
        /// <param name="getter">Type constructor function</param>
        void Register<T>(Func<T> getter);
        /// <summary>
        /// Register a type into the container
        /// </summary>
        /// <typeparam name="TPublic">Interface to register</typeparam>
        /// <typeparam name="TImplementation">Implementation of Interface</typeparam>
        /// <param name="constructor">Constructor info</param>
        void Register<TPublic, TImplementation>(ConstructorInfo constructor = null);
        /// <summary>
        /// Unregister a type from the resolver
        /// </summary>
        /// <typeparam name="T">Type to unregister</typeparam>
        /// <returns>true, if type is unregistered</returns>
        bool Unregister<T>();
        /// <summary>
        /// Resolve a type from the IOC container
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>an instance of the resolved type</returns>
        T Resolve<T>();
        /// <summary>
        /// Type resolved event
        /// </summary>
        event EventHandler<ResolveGetEventArgs> ResolveGet;
        /// <summary>
        /// Get if a type can be resolved or not
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>true, if type can be resolved, otherwise false</returns>
        bool CanResolve<T>();
    }
}
