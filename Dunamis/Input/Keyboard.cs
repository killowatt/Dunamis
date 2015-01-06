#pragma warning disable 612
#pragma warning disable 618
using System.Collections.Generic;
using Dunamis.Graphics;
using OpenTK.Input;

namespace Dunamis.Input
{
    public class Keyboard
    {
        KeyboardDevice device;
        HashSet<Key> _downKeys;

        #region Methods
        public bool IsKeyDown(Key key)
        {
            if (_downKeys.Contains(key))
            {
                return true;
            }
            return false;
        }
        public bool IsKeyUp(Key key)
        {
            return !IsKeyDown(key);
        }
        #endregion

        #region Events
        private void KeyDown(object sender, KeyboardKeyEventArgs arguments)
        {
            _downKeys.Add((Key)arguments.Key);
        }
        private void KeyUp(object sender, KeyboardKeyEventArgs arguments)
        {
            _downKeys.Remove((Key)arguments.Key);
        }
        #endregion

        public Keyboard(Window window)
        {
            IInputDriver driver = window.NativeWindow.InputDriver; // TODO: if keyboard.count <= 0 o no
            device = driver.Keyboard[0];

            _downKeys = new HashSet<Key>();

            device.KeyDown += KeyDown;
            device.KeyUp += KeyUp;
        }
    }
}
