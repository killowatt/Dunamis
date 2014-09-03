#pragma warning disable 612
using System;
using OpenTK.Input;
using Dunamis.Graphics;

namespace Dunamis.Input
{
    public class Mouse
    {
        MouseDevice device;

        #region Properties
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

        #region Events
        private void move(object sender, MouseEventArgs arguments)
        {
            
        }
        #endregion

        // TOOD: add events to these classes (key/ms) + everything else
        public Mouse(Window window)
        {
            IInputDriver driver = window.NativeWindow.InputDriver;
            device = driver.Mouse[0];

            device.Move += new EventHandler<MouseMoveEventArgs>(move);
        }
    }
}
