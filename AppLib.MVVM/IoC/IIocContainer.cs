using System;

namespace AppLib.MVVM.IoC
{
    public interface IIocContainer
    {
        void Register<T>(Func<T> getter);
        bool Unregister<T>();
        T Resolve<T>();
        event EventHandler<ResolveGetEventArgs> ResolveGet;
    }
}
