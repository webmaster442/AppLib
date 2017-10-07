using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Windows.Controls;

namespace AppLib.WPF.BindingValidators
{
    /// <summary>
    /// Validation rule for IP adress
    /// </summary>
    public class IPValidation : ValidationRule
    {
        /// <summary>
        /// IP Adress versions
        /// </summary>
        public enum Versions
        {
            /// <summary>
            /// IPv4 is accepted
            /// </summary>
            V4,
            /// <summary>
            /// IPv6 is accepted
            /// </summary>
            V6,
            /// <summary>
            /// IPv4 and IPv6 are accepted
            /// </summary>
            Both
        }

        /// <summary>
        /// Creates a new instance of IPValidation
        /// </summary>
        public IPValidation()
        {
            Version = Versions.Both;
        }

        /// <summary>
        /// Gets or sets the accepted version
        /// </summary>
        public Versions Version { get; set; }

        /// <summary>
        /// Validates the input. See <see cref="ValidationRule.Validate(object, CultureInfo)"/>
        /// </summary>
        /// <param name="value">input value, must be string</param>
        /// <param name="cultureInfo">input culture</param>
        /// <returns>A ValidationResult</returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            IPAddress parsed;

            var str = value as string;

            if (string.IsNullOrEmpty(str))
                return new ValidationResult(false, CommonErrors.NullInput);

            if (IPAddress.TryParse(str, out parsed))
            {
                if (parsed.AddressFamily == AddressFamily.InterNetwork && Version == Versions.V6)
                    return new ValidationResult(false, "IPv6 address was expected, got IPv4 address");

                if (parsed.AddressFamily == AddressFamily.InterNetworkV6 && Version == Versions.V4)
                    return new ValidationResult(false, "IPv4 address was expected, got IPv6 address");

                return new ValidationResult(true, null);
            }
            else
                return new ValidationResult(false, CommonErrors.Inputerror);
        }
    }


}
