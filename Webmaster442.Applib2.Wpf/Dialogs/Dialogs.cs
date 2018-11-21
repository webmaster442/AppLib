using System;

namespace Webmaster442.Applib.Dialogs
{
    /// <summary>
    /// Dialogs
    /// </summary>
    public static class Dialogs
    {
        /// <summary>
        /// Show a Serial port config dialog
        /// </summary>
        /// <param name="configuredPort">Configured serial port or null</param>
        /// <returns>true, if the user clicked on the ok button, flase if on the cancel</returns>
        public static bool? ShowSerialPortConfigDialog(out System.IO.Ports.SerialPort configuredPort)
        {
            BaseWindow bw = new BaseWindow
            {
                Title = "Serial port settings",
            };
            var serialconfig = new SerialPortControl();
            if (bw.ShowDialog() == true)
            {
                configuredPort = serialconfig.Port;
                return true;
            }
            else
            {
                configuredPort = null;
                return false;
            }
        }
    }
}
