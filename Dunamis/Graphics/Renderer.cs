using System;
using Dunamis.Common.Meshes;
using Dunamis.Common.Shaders;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

namespace Dunamis.Graphics
{
    public class Renderer // TODO: implement safe mode for performance
    { // TODO: window switching? additional settings i.e. transparencY?
        internal GraphicsContext GraphicsContext;
        int renderHeight;
        int renderWidth;

        public Camera Camera;

        RenderTexture _renderTexture;
        RenderTextureMesh _renderTextureMesh;
        RenderTextureShader _renderTextureShader;

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
        public void Display()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.Viewport(0, 0, renderWidth, renderHeight);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            Draw(_renderTextureMesh);
            GraphicsContext.SwapBuffers();
        }
        public void Draw(Mesh mesh) // TODO: Include a way to override mesh shader through this method.
        {
            GL.BindVertexArray(mesh.VertexArray.VertexArrayObject);
            GL.UseProgram(mesh.Shader.ShaderProgram);

            if (!mesh.Shader.Initialized || mesh.Shader.State == ShaderState.Dynamic)
            { 
                mesh.Shader.Model = mesh.Transform;
                mesh.Shader.View = Camera.View;
                mesh.Shader.Projection = Camera.Projection;
            }
            if (!mesh.Shader.Initialized)
            {
                mesh.Shader.Initialize();
                mesh.Shader.Initialized = true;
            }
            if (mesh.Shader.State == ShaderState.Dynamic)
            {
                mesh.Shader.Update();
            }
       
            GL.DrawElements(PrimitiveType.Triangles, mesh.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
        public void Draw(Sprite sprite)
        {
            GL.Disable(EnableCap.DepthTest); // TODO: maybe do this better?

            if (!sprite.Buffered)
            {
                sprite.Mesh.Vertices = sprite.Vertices;
                sprite.Buffered = true;
            }

            GL.BindVertexArray(sprite.Mesh.VertexArray.VertexArrayObject);
            GL.UseProgram(sprite.Mesh.Shader.ShaderProgram);

            if (!sprite.Mesh.Shader.Initialized || sprite.Mesh.Shader.State == ShaderState.Dynamic)
            {
                //mesh.Shader.Model = mesh.Transform;
                sprite.Mesh.Shader.Projection = Camera.Projection2D;
                //mesh.Shader.Projection = Camera.Projection;
            }
            if (!sprite.Mesh.Shader.Initialized)
            {
                sprite.Mesh.Shader.Initialize();
                sprite.Mesh.Shader.Initialized = true;
            }
            if (sprite.Mesh.Shader.State == ShaderState.Dynamic)
            {
                sprite.Mesh.Shader.Update();
            }

            GL.DrawElements(PrimitiveType.Triangles, sprite.Mesh.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.Enable(EnableCap.DepthTest);
        }
        public void Draw(Text text)
        {
           // Draw(text.Sprite);
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
