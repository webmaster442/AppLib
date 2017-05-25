using System;
using System.ComponentModel;
using System.Windows;

namespace AppLib.WPF.Controls
{
    /// <summary>
    /// A docked window
    /// </summary>
    public class DockedWindow : Window
    {
        private Window _parent;
        private DockDirections _direction;

        /// <summary>
        /// Dock target window
        /// </summary>
        public Window DockTarget
        {
            get { return _parent; }
            set
            {
                if (_parent == value) return;
                if (_parent != null && value == null) Unsubscribe();
                _parent = value;
                if (_parent == null) return;
                _parent.SizeChanged += _parent_SizeChanged;
                _parent.LocationChanged += _parent_LocationChanged;
                _parent.StateChanged += _parent_StateChanged;
                _parent.Closed += _parent_Closed;
                _parent.Activated += _parent_Activated;
                MoveWindow();
            }
        }

        /// <summary>
        /// Dock direction
        /// </summary>
        public DockDirections DockDirection
        {
            get { return _direction; }
            set
            {
                if (_direction == value) return;
                _direction = value;
                MoveWindow();
            }
        }

        private void Unsubscribe()
        {
            _parent.SizeChanged -= _parent_SizeChanged;
            _parent.LocationChanged -= _parent_LocationChanged;
            _parent.StateChanged -= _parent_StateChanged;
            _parent.Closed -= _parent_Closed;
            _parent.Activated -= _parent_Activated;
        }

        /// <summary>
        /// On closing override
        /// </summary>
        /// <param name="e">Cancel event args</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_parent == null) return;
            Unsubscribe();
            _parent = null;
            base.OnClosing(e);

        }

        private void _parent_Activated(object sender, EventArgs e)
        {
            Topmost = true;
            Topmost = false;
        }

        private void _parent_Closed(object sender, EventArgs e)
        {
            Close();
        }

        private void MoveWindow()
        {
            switch (DockDirection)
            {
                case DockDirections.Bottom:
                    Left = _parent.Left;
                    Top = _parent.Top + _parent.Height;
                    Width = _parent.Width;
                    break;
                case DockDirections.Left:
                    Top = _parent.Top;
                    Left = _parent.Left - Width;
                    Height = _parent.Height;
                    break;
                case DockDirections.Top:
                    Left = _parent.Left;
                    Top = _parent.Top - Height;
                    Width = _parent.Width;
                    break;
                case DockDirections.Right:
                    Top = _parent.Top;
                    Left = _parent.Left + _parent.Width;
                    Height = _parent.Height;
                    break;
            }
        }

        private void _parent_StateChanged(object sender, EventArgs e)
        {
            WindowState = _parent.WindowState;
        }

        private void _parent_LocationChanged(object sender, EventArgs e)
        {
            MoveWindow();
        }

        private void _parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MoveWindow();
        }
    }

    /// <summary>
    /// Dock directions
    /// </summary>
    public enum DockDirections
    {
        /// <summary>
        /// Left
        /// </summary>
        Left,
        /// <summary>
        /// Rigth
        /// </summary>
        Right,
        /// <summary>
        /// Top
        /// </summary>
        Top,
        /// <summary>
        /// Bottom
        /// </summary>
        Bottom
    }
}
