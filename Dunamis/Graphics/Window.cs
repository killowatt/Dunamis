using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Platform;

namespace Dunamis.Graphics
{
    public class Window
    {
        internal NativeWindow NativeWindow;
        public static readonly int DefaultDisplay = -1;

        #region Properties
        public int Width
        {
            get
            {
                return NativeWindow.Width;
            }
            set
            {
                NativeWindow.Width = value;
            }
        }
        public int Height
        {
            get
            {
                return NativeWindow.Height;
            }
            set
            {
                NativeWindow.Height = value;
            }
        }
        public string Title
        {
            get
            {
                return NativeWindow.Title;
            }
            set
            {
                NativeWindow.Title = value;
            }
        }
        public WindowType Type
        {
            get
            {
                if (NativeWindow.WindowBorder == WindowBorder.Fixed && NativeWindow.WindowState == WindowState.Normal)
                {
                    return WindowType.Window;
                }
                if (NativeWindow.WindowBorder == WindowBorder.Hidden && NativeWindow.WindowState == WindowState.Normal)
                {
                    return WindowType.BorderlessWindow;
                }
                if (NativeWindow.WindowBorder == WindowBorder.Hidden && NativeWindow.WindowState == WindowState.Fullscreen)
                {
                    return WindowType.Fullscreen;
                }
                return WindowType.Unknown;
            }
            set
            {
                if (value == WindowType.Window)
                {
                    NativeWindow.WindowBorder = WindowBorder.Fixed;
                    NativeWindow.WindowState = WindowState.Normal;
                }
                else if (value == WindowType.BorderlessWindow)
                {
                    NativeWindow.WindowBorder = WindowBorder.Hidden;
                    NativeWindow.WindowState = WindowState.Normal;
                }
                else if (value == WindowType.Fullscreen)
                {
                    NativeWindow.WindowBorder = WindowBorder.Hidden;
                    NativeWindow.WindowState = WindowState.Fullscreen;
                }
            }
        }
        public bool Visible
        {
            get
            {
                return NativeWindow.Visible;
            }
            set
            {
                NativeWindow.Visible = value;
            }
        }
        #endregion

        #region Methods
        public void Update()
        {
            NativeWindow.ProcessEvents();
        }
        #endregion

        public Window(int width, int height, string title, WindowType type, int display, bool visible)
        {
            NativeWindow = new NativeWindow(width, height, title, GameWindowFlags.Default, GraphicsMode.Default, DisplayDevice.GetDisplay((DisplayIndex)display));
            Type = type;
            Visible = visible;
        }
    }
}
