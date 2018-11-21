using System;

namespace Webmaster442.Applib.IoC
{
    /// <summary>
    /// Resolver get event args
    /// </summary>
    public sealed class ResolveGetEventArgs :EventArgs
    {
        /// <summary>
        /// Requested Type
        /// </summary>
        public Type RequestedType { get; internal set; }

        private Delegate _getter;

        /// <summary>
        /// Type getter delegate
        /// </summary>
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