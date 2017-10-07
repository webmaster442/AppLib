﻿using System.Globalization;
using System.Windows.Controls;

namespace AppLib.WPF.BindingValidators
{
    /// <summary>
    /// Validation Rule for doubles
    /// </summary>
    public class DoubleValidation : ValidationRule
    {
        /// <summary>
        /// Minimum value
        /// </summary>
        public double? Min { get; set; }

        /// <summary>
        /// Maximum value
        /// </summary>
        public double? Max { get; set; }

        /// <summary>
        /// Validates the input. See <see cref="ValidationRule.Validate(object, CultureInfo)"/>
        /// </summary>
        /// <param name="value">input value, must be string</param>
        /// <param name="cultureInfo">input culture</param>
        /// <returns>A ValidationResult</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double parsed = 0;
            var str = value as string;

            if (string.IsNullOrEmpty(str))
                return new ValidationResult(false, CommonErrors.NullInput);

            if (double.TryParse(str, NumberStyles.Any, cultureInfo, out parsed))
            {
                if (Min != null && parsed < Min)
                    return new ValidationResult(false, CommonErrors.MinValue + Min);

                if (Max != null && parsed > Max)
                    return new ValidationResult(false, CommonErrors.MaxValue + Max);

                return new ValidationResult(true, null);
            }
            else
                return new ValidationResult(false, CommonErrors.Inputerror);
        }
    }

}
