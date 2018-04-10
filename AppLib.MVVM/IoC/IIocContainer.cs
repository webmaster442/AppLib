using System;
using System.Reflection;

namespace AppLib.MVVM.IoC
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
