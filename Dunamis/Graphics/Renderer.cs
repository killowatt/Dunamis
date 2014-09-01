using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Dunamis.Common.Meshes;
using Dunamis.Common.Shaders;

namespace Dunamis.Graphics
{
    // TODO: IDEA: safe mode; no safe mode = just keep track of vars as we change em' safe mode = get values directly from gl (why? performance, yo.)
    public class Renderer
    {
        GraphicsContext graphicsContext;

        #region Fields
        Window currentWindow;

        int currentVertexArray;
        int currentIndexBuffer;
        int currentShader;

        RenderTextureMesh renderTextureMesh;
        RenderTextureShader renderTextureShader;
        #endregion

        #region Properties
        public Color3 ClearColor
        {
            get
            {
                OpenTK.Vector3 clearColor;
                GL.GetFloat(GetPName.ColorClearValue, out clearColor);
                return new Color3(clearColor.X, clearColor.Y, clearColor.Z);
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
        public Window Window
        {
            get
            {
                return currentWindow;
            }
            // TODO: set current window
        }
        #endregion

        #region Methods
        public void DrawMesh(Mesh mesh)
        {
            if (currentVertexArray != mesh.VertexArrayObject)
            {
                currentVertexArray = mesh.VertexArrayObject;
                GL.BindVertexArray(mesh.VertexArrayObject);
            }
            if (currentIndexBuffer != mesh.IndexBufferObject)
            {
                currentIndexBuffer = mesh.IndexBufferObject;
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.IndexBufferObject);
            }
            if (currentShader != mesh.Shader.ShaderProgram)
            {
                currentShader = mesh.Shader.ShaderProgram;
                GL.UseProgram(mesh.Shader.ShaderProgram);
            }

            if (!mesh.Shader.Initialized)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VertexBufferObject); // TODO: see about removing these
                mesh.Shader.Initialize();
                mesh.Shader.Initialized = true;
            }
            if (mesh.Shader.State == ShaderState.Dynamic)
            {
                mesh.Shader.Update();
            }

            GL.DrawElements(PrimitiveType.Triangles, mesh.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }
        public void DrawTexture(Texture texture)
        {
            renderTextureShader.Texture = texture;
            renderTextureMesh.Shader = renderTextureShader; // TODO: optimize this
            DrawMesh(renderTextureMesh);
        }
        public void Clear()
        {
            GL.Viewport(0, 0, currentWindow.Width, currentWindow.Height);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }
        public void Display()
        {
            graphicsContext.SwapBuffers();
        }
        #endregion

        public Renderer(Window window, bool verticalSync)
        {
            currentWindow = window;
            graphicsContext = new GraphicsContext(GraphicsMode.Default, window.WindowInfo);
            graphicsContext.MakeCurrent(currentWindow.WindowInfo);
            graphicsContext.LoadAll();

            renderTextureShader = new RenderTextureShader();
            renderTextureMesh = new RenderTextureMesh(renderTextureShader);

            GL.Enable(EnableCap.DepthTest);

            VerticalSync = verticalSync;
        }
    }
}
