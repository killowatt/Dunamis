using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Platform;

namespace Dunamis.Graphics
{
    public class Window
    {
        NativeWindow nativeWindow;
        public static readonly int DefaultDisplay = -1;

        #region Properties
        public int Width
        {
            get
            {
                return nativeWindow.Width;
            }
            set
            {
                nativeWindow.Width = value;
            }
        }
        public int Height
        {
            get
            {
                return nativeWindow.Height;
            }
            set
            {
                nativeWindow.Height = value;
            }
        }
        public string Title
        {
            get
            {
                return nativeWindow.Title;
            }
            set
            {
                nativeWindow.Title = value;
            }
        }
        public WindowType Type
        {
            get
            {
                if (nativeWindow.WindowBorder == WindowBorder.Fixed && nativeWindow.WindowState == WindowState.Normal)
                {
                    return WindowType.Window;
                }
                if (nativeWindow.WindowBorder == WindowBorder.Hidden && nativeWindow.WindowState == WindowState.Normal)
                {
                    return WindowType.BorderlessWindow;
                }
                if (nativeWindow.WindowBorder == WindowBorder.Hidden && nativeWindow.WindowState == WindowState.Fullscreen)
                {
                    return WindowType.Fullscreen;
                }
                return WindowType.Unknown;
            }
            set
            {
                if (value == WindowType.Window)
                {
                    nativeWindow.WindowBorder = WindowBorder.Fixed;
                    nativeWindow.WindowState = WindowState.Normal;
                }
                else if (value == WindowType.BorderlessWindow)
                {
                    nativeWindow.WindowBorder = WindowBorder.Hidden;
                    nativeWindow.WindowState = WindowState.Normal;
                }
                else if (value == WindowType.Fullscreen)
                {
                    nativeWindow.WindowBorder = WindowBorder.Hidden;
                    nativeWindow.WindowState = WindowState.Fullscreen;
                }
            }
        }
        public bool Visible
        {
            get
            {
                return nativeWindow.Visible;
            }
            set
            {
                nativeWindow.Visible = value;
            }
        }
        internal IWindowInfo WindowInfo
        {
            get
            {
                return nativeWindow.WindowInfo;
            }
        }
        #endregion

        #region Methods
        public void Update()
        {
            nativeWindow.ProcessEvents();
        }
        #endregion

        public Window(int width, int height, string title, WindowType type, int display, bool visible)
        {
            nativeWindow = new NativeWindow(width, height, title, GameWindowFlags.Default, GraphicsMode.Default, DisplayDevice.GetDisplay((DisplayIndex)display));
            Type = type;
            Visible = visible;
        }
    }
}
