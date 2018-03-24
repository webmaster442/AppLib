using System;

namespace AppLib.WPF.Dialogs
{
    /// <summary>
    /// Dialogs
    /// </summary>
    public static class Dialogs
    {
        /// <summary>
        /// Show an Error dialog
        /// </summary>
        /// <param name="ex">Exception to show</param>
        /// <returns>true, if the user clicked on the ok button</returns>
        public static void ShowErrorDialog(Exception ex)
        {
            BaseWindow bw = new BaseWindow
            {
                Title = "An Exception occured",
                DialogContent = new ErrorControl(ex)
            };
            bw.BtnCancel.Visibility = System.Windows.Visibility.Collapsed;
            bw.ShowDialog();
        }

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
