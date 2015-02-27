using System;
using Dunamis.Common.Meshes;
using Dunamis.Common.Shaders;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

namespace Dunamis.Graphics
{
    public class Renderer // TODO: implement safe mode for performance
    { // TODO: window switching?
        // TODO: more additional settings?
        internal GraphicsContext GraphicsContext;
        int renderHeight;
        int renderWidth;

        RenderTexture _renderTexture;
        RenderTextureMesh _renderTextureMesh;
        RenderTextureShader _renderTextureShader;
        bool _transparencyEnabled;

        public Camera Camera;

        #region Properties
        public Color3 ClearColor
        {
            get
            {
                OpenTK.Vector3 clearColor;
                GL.GetFloat(GetPName.ColorClearValue, out clearColor);
                return (Vector3)clearColor;
            }
            set
            {
                GL.ClearColor(new Color4(value.R, value.G, value.B, 255));
            }
        }
        public RenderTexture RenderTexture
        {
            get { return _renderTexture; }
            set
            {
                _renderTexture = value;
                _renderTextureShader = _renderTexture.Shader;
                _renderTextureMesh.Shader = _renderTexture.Shader;
                _renderTextureShader.Texture = _renderTexture.Color;
            }
        }
        public bool TransparencyEnabled
        {
            get
            {
                return _transparencyEnabled;
            }
            set
            {
                if (value)
                {
                    GL.Enable(EnableCap.Blend);
                    GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                    _transparencyEnabled = true;
                }
                else
                {
                    GL.Disable(EnableCap.Blend);
                    _transparencyEnabled = false;
                }
            }
        }
        public bool VerticalSync
        {
            get
            {
                switch (GraphicsContext.SwapInterval)
                {
                    case 1:
                        return true;
                    default:
                        return false;
                }
            }
            set 
            {
                GraphicsContext.SwapInterval = value ? 1 : 0;
            }
        }
        #endregion

        public void Clear()
        {
            GL.Viewport(0, 0, RenderTexture.Width, RenderTexture.Height);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, RenderTexture.FrameBuffer);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }
        public void Display() // TODO: make IDRAWABLE BETTER
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Viewport(0, 0, renderWidth, renderHeight);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            Draw(_renderTextureMesh);
            GraphicsContext.SwapBuffers();
        }
        public void Draw(IDrawable renderable)
        {
            renderable.Draw(this);
        }
        public Renderer(IntPtr handle, int width, int height) // TODO: make a not terrible constructor
        {
            renderWidth = width;
            renderHeight = height;

            GraphicsContext = new GraphicsContext(GraphicsMode.Default, Utilities.CreateWindowsWindowInfo(handle));
            GraphicsContext.LoadAll();

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Camera = new Camera(new Vector3(0.0f, 0.0f, 0.0f), 0, 0, 0, 90, new Vector2(width, height));

            DefaultRenderTextureShader defaultRenderTextureShader = new DefaultRenderTextureShader();
            _renderTextureMesh = new RenderTextureMesh(defaultRenderTextureShader);
            RenderTexture = new RenderTexture(width, height, defaultRenderTextureShader);
        }
        public Renderer(Window window, bool verticalSync)
            : this(window.NativeWindow.WindowInfo.Handle, window.Width, window.Height)
        {
            VerticalSync = verticalSync;
        }
        public Renderer(DunamisControl control, bool verticalSync)
            : this(control.Handle, control.Width, control.Height)
        {
            VerticalSync = verticalSync;
        }
    }
}
