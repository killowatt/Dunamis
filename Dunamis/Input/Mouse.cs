#pragma warning disable 612
#pragma warning disable 618
using System;
using System.Collections.Generic;
using Dunamis.Graphics;
using OpenTK.Input;

namespace Dunamis.Input
{
    public class Mouse
    {
        private HashSet<Button> _downButtons;
        private Window _window;

        private float _wheelPosition = 0.0f;
        private int _currentX = 0;
        private int _currentY = 0;
        private int _xDelta = 0;
        private int _yDelta = 0;

        public bool Locked = false;

        public event EventHandler<DunamisMouseButtonEventArgs> ButtonDown;
        public event EventHandler<DunamisMouseButtonEventArgs> ButtonUp;
        public event EventHandler<DunamisMouseMoveEventArgs> Move;
        public event EventHandler<DunamisMouseWheelEventArgs> Wheel;

        #region Properties

        public int X
        {
            get
            {
                return _currentX;
            }
        }
        public int XDelta
        {
            get
            {
                return _xDelta;
            }
        }
        public int Y
        {
            get
            {
                return _currentY;
            }
        }
        public int YDelta
        {
            get
            {
                return _yDelta;
            }
        }
        public float WheelPosition
        {
            get
            {
                return _wheelPosition;
            }
        }
        #endregion

        #region Methods
        public bool IsButtonDown(Button button)
        {
            return _downButtons.Contains(button);
        }

        public bool IsButtonUp(Button button)
        {
            return !IsButtonDown(button);
        }

        public MouseState GetState()
        {
            return new MouseState(this);
        }
        #endregion

        #region Events
        private void _ButtonDown(object sender, MouseButtonEventArgs arguments)
        {
            if (!_window.Focused) return;
            _downButtons.Add((Button)arguments.Button);
            if(ButtonDown != null)
                ButtonDown(sender, new DunamisMouseButtonEventArgs((Button)arguments.Button));
        }
        private void _ButtonUp(object sender, MouseButtonEventArgs arguments)
        {
            if (!_window.Focused) return;
            _downButtons.Remove((Button)arguments.Button);
            if(ButtonUp != null)
                ButtonUp(sender, new DunamisMouseButtonEventArgs((Button)arguments.Button));
        }
        private void WheelMove(object sender, MouseWheelEventArgs e)
        {
            if (!_window.Focused) return;
            _wheelPosition = e.ValuePrecise;
            if(Wheel != null)
                Wheel(sender, new DunamisMouseWheelEventArgs(e.Value, e.ValuePrecise, e.Delta));
        }
        private void MouseMove(object sender, MouseMoveEventArgs e)
        {
            if (!_window.Focused) return;
            _currentX = e.X;
            _currentY = e.Y;
            _xDelta = e.XDelta;
            _yDelta = e.YDelta;
            if(Move != null)
                Move(sender, new DunamisMouseMoveEventArgs(e.X, e.Y, e.XDelta, e.YDelta));
        }
        #endregion

        internal Mouse(Window window)
        {
            _downButtons = new HashSet<Button>();
            _window = window;
            window.NativeWindow.MouseDown += _ButtonDown;
            window.NativeWindow.MouseUp += _ButtonUp;
            window.NativeWindow.MouseWheel += WheelMove;
            window.NativeWindow.MouseMove += MouseMove;
        }

        // Called at the end of the update to clear the mouse delta for next move.
        internal void ResetDelta()
        {
            _xDelta = 0;
            _yDelta = 0;
        }
    }

    public class DunamisMouseMoveEventArgs
    {
        public int X, Y, XDelta, YDelta;

        public DunamisMouseMoveEventArgs(int x, int y, int xDelta, int yDelta)
        {
            X = x;
            Y = y;
            XDelta = xDelta;
            YDelta = yDelta;
        }
    }

    public class DunamisMouseButtonEventArgs
    {
        public Button Button;

        public DunamisMouseButtonEventArgs(Button button)
        {
            Button = button;
        }
    }

    public class DunamisMouseWheelEventArgs
    {
        public int Position;
        public float PositionPrecise;
        public int Delta;

        public DunamisMouseWheelEventArgs(int position, float precise, int delta)
        {
            Position = position;
            PositionPrecise = precise;
            Delta = delta;
        }
    }
}
