using System;
using System.Globalization;
using System.Windows.Controls;

namespace AppLib.WPF.BindingValidators
{
    /// <summary>
    /// Validation rule of URLs
    /// </summary>
    public class UrlValidator : ValidationRule
    {
        /// <summary>
        /// Creates a new instance of UrlValidator
        /// </summary>
        public UrlValidator()
        {
            this.UriKind = UriKind.RelativeOrAbsolute;
        }

        /// <summary>
        /// Gets or sets the accepted UriKind. Default is RelativeOrAbsolute
        /// </summary>
        public UriKind UriKind { get; set; }

        /// <summary>
        /// Validates the input. See <see cref="ValidationRule.Validate(object, CultureInfo)"/>
        /// </summary>
        /// <param name="value">input value, must be string</param>
        /// <param name="cultureInfo">input culture</param>
        /// <returns>A ValidationResult</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Uri parsed;

            var str = value as string;

            if (string.IsNullOrEmpty(str))
                return new ValidationResult(false, CommonErrors.NullInput);

            if (Uri.TryCreate(str, UriKind, out parsed))
            {
                return new ValidationResult(true, null);
            }
            else
                return new ValidationResult(false, "Can't parse uri for " + UriKind.ToString() + " type");
        }
    }
}
