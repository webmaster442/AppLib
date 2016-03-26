﻿using System;
using System.IO.Ports;
using System.Windows;
using System.Windows.Controls;

namespace WPFLib.Dialogs
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
                SerialPort port = new SerialPort();
                port.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), ToStr(StopBits.SelectedItem));
                port.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity), ToStr(Parity.SelectedItem));
                port.BaudRate = Convert.ToInt32(ToStr(Baudrate.SelectedItem));
                port.PortName = ToStr(Ports.SelectedItem);
                port.RtsEnable = (bool)Rts.IsChecked;
                port.DtrEnable = (bool)Dtr.IsChecked;
                port.Handshake = (System.IO.Ports.Handshake)Enum.Parse(typeof(System.IO.Ports.Handshake), ToStr(Handshake.SelectedItem));
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
                ComboBoxItem c = (ComboBoxItem)o;
                return c.Content.ToString();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddItems(Ports.Items, SerialPort.GetPortNames());
            AddItems(Parity.Items, Enum.GetNames(typeof(System.IO.Ports.Parity)));
            AddItems(StopBits.Items, Enum.GetNames(typeof(System.IO.Ports.StopBits)));
            AddItems(Handshake.Items, Enum.GetNames(typeof(System.IO.Ports.Handshake)));
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
    }
}
