using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppLib.Common.PInvoke
{
    /// <summary>
    /// Platform Invokes to Dwmapi.dll
    /// </summary>
    public static class DwmApi
    {
        /// <summary>
        /// Gets the current theme colors
        /// </summary>
        /// <param name="pars">Colors in a DWMCOLORIZATIONPARAMS structure</param>
        [DllImport("dwmapi.dll", EntryPoint = "#127")]
        public static extern void DwmGetColorizationParameters(ref DWMCOLORIZATIONPARAMS pars);
    }
}
