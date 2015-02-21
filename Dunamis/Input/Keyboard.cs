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
            return _downKeys.Contains(key);
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

        internal Keyboard(Window window)
        {
            _downKeys = new HashSet<Key>();

            window.NativeWindow.KeyDown += KeyDown;
            window.NativeWindow.KeyUp += KeyUp;
        }
    }
}
