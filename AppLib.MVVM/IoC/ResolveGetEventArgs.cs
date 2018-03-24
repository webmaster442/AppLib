using System;

namespace AppLib.MVVM.IoC
{
    public sealed class ResolveGetEventArgs :EventArgs
    {
        public Type RequestedType { get; internal set; }

        private Delegate _getter;
        public Delegate Getter
        {
            get
            {
                return _getter;
            }
            set
            {
                if (value != null)
                {
                    Type funcType = typeof(Func<>).MakeGenericType(RequestedType);
                    if (value.GetType() != funcType)
                        throw new InvalidOperationException("The Getter must be of type: " + funcType.FullName);
                }

                _getter = value;
            }
        }
    }

}