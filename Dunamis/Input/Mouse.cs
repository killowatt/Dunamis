#pragma warning disable 612
#pragma warning disable 618
using System.Collections.Generic;
using Dunamis.Graphics;
using OpenTK.Input;

namespace Dunamis.Input
{
    public class Mouse
    {
        HashSet<Button> downButtons;

        private float _wheelPosition = 0.0f;
        private int _currentX = 0;
        private int _currentY = 0;
        private int _xDelta = 0;
        private int _yDelta = 0;

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
            return downButtons.Contains(button);
        }

        public bool IsButtonUp(Button button)
        {
            return !IsButtonDown(button);
        }
        #endregion

        #region Events
        private void ButtonDown(object sender, MouseButtonEventArgs arguments)
        {
            downButtons.Add((Button)arguments.Button);
        }
        private void ButtonUp(object sender, MouseButtonEventArgs arguments)
        {
            downButtons.Remove((Button)arguments.Button);
        }
        private void WheelMove(object sender, MouseWheelEventArgs e)
        {
            _wheelPosition = e.ValuePrecise;
        }
        private void MouseMove(object sender, MouseMoveEventArgs e)
        {
            _currentX = e.X;
            _currentY = e.Y;
            _xDelta = e.XDelta;
            _yDelta = e.YDelta;
        }
        #endregion

        internal Mouse(Window window)
        {
            downButtons = new HashSet<Button>();

            window.NativeWindow.MouseDown += ButtonDown;
            window.NativeWindow.MouseUp += ButtonUp;
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
}
