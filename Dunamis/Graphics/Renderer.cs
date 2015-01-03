using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Dunamis.Common.Meshes;
using Dunamis.Common.Shaders;

namespace Dunamis.Graphics
{
    public class Renderer // TODO: implement safe mode for performance
    {
        GraphicsContext graphicsContext;
        Window window;

        public Camera Camera;
        public RenderTexture RenderTexture;

        RenderTextureMesh renderTextureMesh;
        RenderTextureShader renderTextureShader;

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
        public bool VerticalSync
        {
            get
            {
                switch (graphicsContext.SwapInterval)
                {
                    case 1:
                        return true;
                    default:
                        return false;
                }
            }
            set
            {
                if (value)
                {
                    graphicsContext.SwapInterval = 1;
                }
                else
                {
                    graphicsContext.SwapInterval = 0;
                }
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
            GL.Viewport(0, 0, window.Width, window.Height);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
            Draw(renderTextureMesh);
            graphicsContext.SwapBuffers();
        }

        public void Draw(Mesh mesh) // TODO: Include a way to override mesh shader through this method.
        {
            GL.BindVertexArray(mesh.VertexArrayObject);
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

        public Renderer(Window window, bool verticalSync)
        {
            this.window = window;
            graphicsContext = new GraphicsContext(GraphicsMode.Default, window.NativeWindow.WindowInfo);
            graphicsContext.LoadAll();

            Camera = new Camera(new Vector3(0.0f, 0.0f, 0.0f), 0, 0, 0, 90, new Vector2(window.Width, window.Height));

            RenderTexture = new RenderTexture(window.Width, window.Height);
            renderTextureShader = new RenderTextureShader();
            renderTextureMesh = new RenderTextureMesh(renderTextureShader);
            renderTextureShader.Texture = RenderTexture.Color;

            VerticalSync = verticalSync;
        }
    }
}
