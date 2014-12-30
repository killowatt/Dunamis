using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Renderer // TODO: implement safe mode for performance
    {
        GraphicsContext graphicsContext;

        #region Properties
        public Color3 ClearColor
        {
            get
            {
                return new Color3();
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
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }
        public void Display()
        {
            graphicsContext.SwapBuffers();
        }

        public void Draw(Mesh mesh) // TODO: Include a way to override mesh shader through this method.
        {
            GL.BindVertexArray(mesh.VertexArrayObject);
            GL.UseProgram(mesh.Shader.ShaderProgram);

            if (!mesh.Shader.Initialized)
            {
                mesh.Shader.Initialize();
                mesh.Shader.Initialized = true;
            }

            GL.DrawElements(PrimitiveType.Triangles, mesh.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        public Renderer(Window window, bool verticalSync)
        {
            graphicsContext = new GraphicsContext(GraphicsMode.Default, window.NativeWindow.WindowInfo);
            graphicsContext.LoadAll();

            VerticalSync = verticalSync;
        }
    }
}
