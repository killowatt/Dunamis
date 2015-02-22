#pragma warning disable 612
#pragma warning disable 618
using System;
using System.Collections.Generic;
using Dunamis.Graphics;
using OpenTK.Input;

namespace Dunamis.Input
{
    public class Keyboard
    {
        KeyboardDevice device;
        HashSet<Key> _downKeys;

        public event EventHandler<DunamisKeyboardEventArgs> KeyDown;
        public event EventHandler<DunamisKeyboardEventArgs> KeyUp;

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
        private void _KeyDown(object sender, KeyboardKeyEventArgs arguments)
        {
            _downKeys.Add((Key)arguments.Key);
            Down(sender, new DunamisKeyboardEventArgs((Key)arguments.Key));
        }
        private void _KeyUp(object sender, KeyboardKeyEventArgs arguments)
        {
            _downKeys.Remove((Key)arguments.Key);
            Up(sender, new DunamisKeyboardEventArgs((Key)arguments.Key));
        }
        #endregion

        internal Keyboard(Window window)
        {
            _downKeys = new HashSet<Key>();

            window.NativeWindow.KeyDown += _KeyDown;
            window.NativeWindow.KeyUp += _KeyUp;
        }
    }

    public class DunamisKeyboardEventArgs : EventArgs
    {
        public Key Key;
        public DunamisKeyboardEventArgs(Key k)
        {
            this.Key = k;
        }
    }
}
