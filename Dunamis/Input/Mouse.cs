#pragma warning disable 612
using System;
using System.Collections.Generic;
using OpenTK.Input;
using Dunamis.Graphics;

namespace Dunamis.Input
{
    public class Mouse // TODO: implement events
    {
        MouseDevice device;
        HashSet<Button> downButtons;
        float deltaX;
        float deltaY;

        #region Properties
        public HashSet<Button> DownButtons
        {
            get
            {
                return downButtons;
            }
        }
        public int X
        {
            get
            {
                return device.X;
            }
        }
        public int Y
        {
            get
            {
                return device.Y;
            }
        }
        public float WheelPosition
        {
            get
            {
                return device.WheelPrecise;
            }
        }
        public float DeltaX
        {
            get
            {
                return deltaX;
            }
        }
        public float DeltaY
        {
            get
            {
                return deltaY;
            }
        }
        public string Description
        {
            get
            {
                return device.Description;
            }
        }
        public int ButtonCount
        {
            get
            {
                return device.NumberOfButtons;
            }
        }
        #endregion

        #region Methods
        public bool IsButtonDown(Button button)
        {
            if (downButtons.Contains(button))
            {
                return true;
            }
            return false;
        }
        public bool IsButtonUp(Button button)
        {
            return !IsButtonDown(button);
        }
        #endregion

        #region Events
        private void buttonDown(object sender, MouseButtonEventArgs arguments)
        {
            downButtons.Add((Button)arguments.Button);
        }
        private void buttonUp(object sender, MouseButtonEventArgs arguments)
        {
            downButtons.Remove((Button)arguments.Button);
        }
        private void move(object sender, MouseMoveEventArgs arguments)
        {
            deltaX = arguments.XDelta;
            deltaY = arguments.YDelta;   
        }
        #endregion

        public Mouse(Window window)
        {
            IInputDriver driver = window.NativeWindow.InputDriver; // TODO: switch to OpenTK.Input.Keyboard/Mouse
            device = driver.Mouse[0];

            downButtons = new HashSet<Button>();

            device.ButtonDown += new EventHandler<MouseButtonEventArgs>(buttonDown);
            device.ButtonUp += new EventHandler<MouseButtonEventArgs>(buttonUp);
            device.Move += new EventHandler<MouseMoveEventArgs>(move);
        }
    }
}
