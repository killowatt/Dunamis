using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using Dunamis.Input;

namespace Dunamis.Graphics
{
    public class Window
    {
        internal NativeWindow NativeWindow;

        public event EventHandler Closing;
        public readonly Mouse Mouse;
        public readonly Keyboard Keyboard;

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
        public int X
        {
            get
            {
                return NativeWindow.X;
            }
            set
            {
                NativeWindow.X = value;
            }
        }
        public int Y
        {
            get
            {
                return NativeWindow.X;
            }
            set
            {
                NativeWindow.Y = value;
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
        public bool CursorVisible
        {
            get
            {
                return NativeWindow.CursorVisible;
            }
            set
            {
                NativeWindow.CursorVisible = value;
            }
        }
        public bool Focused
        {
            get { return NativeWindow.Focused; }
        }
        public Icon Icon
        {
            get
            {
                return NativeWindow.Icon;
            }
            set
            {
                NativeWindow.Icon = value;
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
                return 0;
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
        #endregion

        #region Methods
        public void Update()
        {
            // If the mouse doesn't move, the mouse delta values don't get updated.
            Mouse.ResetDelta();
            NativeWindow.ProcessEvents();
        }
        public void Close()
        {
            NativeWindow.Close();
        }
        #endregion

        #region Events
        void IsClosing()
        {
            if (Closing != null)
            {
                Closing(this, EventArgs.Empty);
            }
        }
        void WindowClosing(object o, EventArgs arguments)
        {
            IsClosing();
            Close();
        }
        #endregion

        #region Constructors
        public Window(int width, int height, string title, WindowType type, int display, bool visible)
        {
            NativeWindow = new NativeWindow(width, height, title, GameWindowFlags.Default, GraphicsMode.Default, DisplayDevice.GetDisplay((DisplayIndex)display));
            NativeWindow.Closing += WindowClosing;

            Keyboard = new Keyboard(this);
            Mouse = new Mouse(this);

            Type = type;
            Visible = visible;
        }
        public Window(int width, int height, string title, WindowType type, int display)
            : this(width, height, title, type, display, true)
        {
        }
        public Window(int width, int height, string title, WindowType type)
            : this(width, height, title, type, Display.DefaultDisplay)
        {
        }
        public Window(int width, int height, string title)
            : this(width, height, title, WindowType.Window)
        {
        }
        public Window(int width, int height)
            : this(width, height, "Dunamis")
        {
        }
        #endregion
    }
}
