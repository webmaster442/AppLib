using System.Collections.Generic;

namespace Webmaster442.Applib.Converters
{
    /// <summary>
    /// Base class for Multi valuie converters
    /// </summary>
    /// <typeparam name="T">Converter type</typeparam>
    public abstract class MultiConverterBase<T>: ConverterBase<T> where T : new()
    {
        /// <summary>
        /// Cast all inputs to a specified type
        /// </summary>
        /// <typeparam name="TargetType">Target type</typeparam>
        /// <param name="inputs">collection of inputs</param>
        /// <returns>Input objects casted to type</returns>
        protected IEnumerable<TargetType> CastInput<TargetType>(params object[] inputs)
        {
            List<TargetType> castedInputs = new List<TargetType>(inputs.Length);

            foreach (var item in inputs)
            {
                if (item is TargetType)
                {
                    castedInputs.Add((TargetType)item);
                }
            }

            return castedInputs;
        }
    }
}
