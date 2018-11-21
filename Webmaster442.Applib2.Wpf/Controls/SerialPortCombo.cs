using System.IO.Ports;
using System.Windows.Controls;

namespace Webmaster442.Applib.Controls
{
    /// <summary>
    /// A combo box that displays the available serial ports
    /// </summary>
    public class SerialPortCombo: ComboBox
    {
        /// <summary>
        /// Creates a new instance of SerialPortCombo
        /// </summary>
        public SerialPortCombo()
        {
            Refresh();
        }

        /// <summary>
        /// Refreshes the list of available ports
        /// </summary>
        public void Refresh()
        {
            Items.Clear();
            IsEnabled = false;
            var ports = SerialPort.GetPortNames();
            foreach (var item in ports) Items.Add(item);
            if (ports.Length > 0) IsEnabled = true;
            else Items.Add("No ports detected");
            SelectedIndex = 0;
        }

        /// <summary>
        /// Opens the currently selected port as an arduino,
        /// Preconfigures the serial port
        /// </summary>
        /// <param name="baudrate">the baud rate to be used during communication</param>
        /// <returns>A preconfigured serial port with arduino settings</returns>
        public SerialPort OpenSelectedAsArduino(int baudrate)
        {
            var selected = SelectedItem as string;
            var port = new SerialPort(selected);
            port.BaudRate = baudrate;
            port.StopBits = StopBits.One;
            port.Parity = Parity.None;
            port.DataBits = 8;
            port.DtrEnable = true;
            port.Open();
            return port;
        }
    }
}
