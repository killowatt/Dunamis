#pragma warning disable 612
using System;
using System.Collections.Generic;
using OpenTK.Input;
using Dunamis.Graphics;

namespace Dunamis.Input
{
    public class Keyboard
    {
        // TODO: entire project; see if you can make a basic project without referencing opentk. fix it if you can
        // TODO: maybe do dictionary? aka dictionary<key, bool> (key, is down)
        KeyboardDevice device;
        HashSet<Key> downKeys;

        public bool IsKeyDown(Key key)
        {
            if (downKeys.Contains(key))
            {
                return true;
            }
            return false;
        }
        public bool IsKeyUp(Key key)
        {
            if (!downKeys.Contains(key))
            {
                return true;
            }
            return false;
        }

        #region Properties
        public string Description
        {
            get
            {
                return device.Description;
            }
        }
        public int KeyCount
        {
            get
            {
                return device.NumberOfKeys;
            }
        }
        public int FunctionKeyCount
        {
            get
            {
                return device.NumberOfFunctionKeys;
            }
        }
        public int LedCount
        {
            get
            {
                return device.NumberOfLeds;
            }
        }
        #endregion

        #region Events
        private void keyDown(object sender, KeyboardKeyEventArgs arguments)
        {
            downKeys.Add((Key)arguments.Key);
        }
        private void keyUp(object sender, KeyboardKeyEventArgs arguments)
        {
            downKeys.Remove((Key)arguments.Key);
        }
        #endregion

        public Keyboard(Window window)
        {
            IInputDriver driver = window.NativeWindow.InputDriver; // TODO: if keyboard.count <= 0 o no
            device = driver.Keyboard[0];

            downKeys = new HashSet<Key>();

            device.KeyDown += new EventHandler<KeyboardKeyEventArgs>(keyDown);
            device.KeyUp += new EventHandler<KeyboardKeyEventArgs>(keyUp);
        }
    }
}
