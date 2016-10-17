using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;

namespace AppLib.WPF.Dialogs
{
    /// <summary>
    /// Serial Port configuration dialog
    /// </summary>
    public partial class SerialPortDialog : Window
    {
        /// <summary>
        /// Creates a new instance of SerialPortDialog
        /// </summary>
        public SerialPortDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the confugured Serial port
        /// </summary>
        public SerialPort Port
        {
            get
            {
                var port = new SerialPort();
                port.StopBits = (StopBits)Enum.Parse(typeof(StopBits), ToStr(StopBits.SelectedItem));
                port.Parity = (Parity)Enum.Parse(typeof(Parity), ToStr(Parity.SelectedItem));
                port.BaudRate = Convert.ToInt32(ToStr(Baudrate.SelectedItem));
                port.PortName = ToStr(Ports.SelectedItem);
                port.RtsEnable = (bool)Rts.IsChecked;
                port.DtrEnable = (bool)Dtr.IsChecked;
                port.Handshake = (Handshake)Enum.Parse(typeof(Handshake), ToStr(Handshake.SelectedItem));
                return port;
            }
        }

        private void AddItems(ItemCollection coll, object[] Items)
        {
            foreach (var i in Items) coll.Add(i);
        }

        private string ToStr(object o)
        {
            if (o is string) return (string)o;
            else
            {
                var c = (ComboBoxItem)o;
                return c.Content.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddItems(Ports.Items, SerialPort.GetPortNames());
            AddItems(Parity.Items, Enum.GetNames(typeof(Parity)));
            AddItems(StopBits.Items, Enum.GetNames(typeof(StopBits)));
            AddItems(Handshake.Items, Enum.GetNames(typeof(Handshake)));
            Ports.SelectedIndex = 0;
            Handshake.SelectedIndex = 0;
            Parity.SelectedIndex = 0;
            StopBits.SelectedIndex = 1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        /// <summary>
        /// Selects a port name from the port selector, if the port is present on the system
        /// </summary>
        /// <param name="name">name of the port</param>
        public void SelectPortName(string name)
        {
            int index = 0;
            int i = 0;
            foreach (string item in Ports.Items)
            {
                if (item == name)
                {
                    index = i;
                    break;
                }
                i++;
            }
        }
    }
}
