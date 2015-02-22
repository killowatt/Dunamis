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
        public event EventHandler<DunamisKeyboardEventArgs> KeyPress;

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
            if(KeyDown != null)
                KeyDown(sender, new DunamisKeyboardEventArgs((Key)arguments.Key));
        }
        private void _KeyUp(object sender, KeyboardKeyEventArgs arguments)
        {
            _downKeys.Remove((Key)arguments.Key);
            if(KeyUp != null)
                KeyUp(sender, new DunamisKeyboardEventArgs((Key)arguments.Key));
        }
        private void _KeyPress(object sender, OpenTK.KeyPressEventArgs e)
        {
            if(KeyPress != null)
                KeyPress(sender, new DunamisKeyboardEventArgs(e.KeyChar));
        }
        #endregion

        internal Keyboard(Window window)
        {
            _downKeys = new HashSet<Key>();

            window.NativeWindow.KeyDown += _KeyDown;
            window.NativeWindow.KeyUp += _KeyUp;
            window.NativeWindow.KeyPress += _KeyPress;
        }
    }

    public class DunamisKeyboardEventArgs : EventArgs
    {
        public Key Key;
        public char KeyChar;

        public DunamisKeyboardEventArgs(Key k)
        {
            this.Key = k;
        }

        public DunamisKeyboardEventArgs(char k)
        {
            this.KeyChar = k;
        }
    }
}
