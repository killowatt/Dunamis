#pragma warning disable 612
#pragma warning disable 618
using System.Collections.Generic;
using Dunamis.Graphics;
using OpenTK.Input;

namespace Dunamis.Input
{
    public class Mouse
    {
        MouseDevice device;
        HashSet<Button> downButtons;

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
        #endregion

        public Mouse(Window window)
        {
            IInputDriver driver = window.NativeWindow.InputDriver;
            device = driver.Mouse[0];

            downButtons = new HashSet<Button>();

            device.ButtonDown += ButtonDown;
            device.ButtonUp += ButtonUp;
        }
    }
}
