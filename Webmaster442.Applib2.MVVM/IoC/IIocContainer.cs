using System;
using System.Reflection;

namespace Webmaster442.Applib.IoC
{
    public interface IIocContainer
    {
        void Register<T>(Func<T> getter);
        void RegisterCallingConstructor<TPublic, TImplementation>(ConstructorInfo constructor = null);
        bool Unregister<T>();
        T Resolve<T>();
        event EventHandler<ResolveGetEventArgs> ResolveGet;
        bool CanResolve<T>();
    }
}
