using System;
using Dunamis.Common.Meshes;
using Dunamis.Common.Shaders;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Renderer // TODO: implement safe mode for performance
    {
        GraphicsContext _graphicsContext;
        Window _window;

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
                switch (_graphicsContext.SwapInterval)
                {
                    case 1:
                        return true;
                    default:
                        return false;
                }
            }
            set 
            {
                _graphicsContext.SwapInterval = value ? 1 : 0;
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
            GL.Viewport(0, 0, _window.Width, _window.Height);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            Draw(_renderTextureMesh);
            _graphicsContext.SwapBuffers();
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
        }
        public void Draw(Text text)
        {
            Draw(text.Sprite);
        }

        public Renderer(Window window, bool verticalSync) // TODO: add additional constructors
        {
            _window = window;
            _graphicsContext = new GraphicsContext(GraphicsMode.Default, window.NativeWindow.WindowInfo);
            _graphicsContext.LoadAll();

            GL.Enable(EnableCap.DepthTest);

            Camera = new Camera(new Vector3(0.0f, 0.0f, 0.0f), 0, 0, 0, 90, new Vector2(window.Width, window.Height));

            DefaultRenderTextureShader defaultRenderTextureShader = new DefaultRenderTextureShader();
            _renderTextureMesh = new RenderTextureMesh(defaultRenderTextureShader);
            RenderTexture = new RenderTexture(window.Width, window.Height, defaultRenderTextureShader);

            VerticalSync = verticalSync;
        }
    }
}
