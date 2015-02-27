using System;
using Dunamis.Common.Shaders;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Sprite : IDrawable
    {
        internal Mesh Mesh;
        float[] vertices;
        internal bool Buffered;

        public Texture Texture;
        public SpriteShader Shader;

        int x;
        int y;
        int width;
        int height;

        public float[] Vertices
        {
            get
            {
                if (!Buffered)
                {
                    vertices = new float[] { x, y, 0, x + width, y, 0, x, y + height, 0, x + width, y + height, 0 };
                    Mesh.Vertices = vertices;
                }
                return vertices;
            }
        }
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                Buffered = false;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                Buffered = false;
            }
        }
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                Buffered = false;
            }
        }
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                Buffered = false;
            }
        }
        public float Rotation
        {
            get { return Mesh.Roll; }
            set { Mesh.Roll = value; }
        }

        public void Draw(Renderer renderer)
        {
            GL.Disable(EnableCap.DepthTest); // TODO: maybe do this better?

            if (!Buffered)
            {
                Mesh.Vertices = Vertices;
                Buffered = true;
            }

            GL.BindVertexArray(Mesh.VertexArray.VertexArrayObject);
            GL.UseProgram(Mesh.Shader.ShaderProgram);

            if (!Mesh.Shader.Initialized || Mesh.Shader.State == ShaderState.Dynamic)
                Mesh.Shader.Projection = renderer.Camera.Projection2D;
            if (!Mesh.Shader.Initialized)
            {
                Mesh.Shader.Initialize();
                Mesh.Shader.Initialized = true;
            }
            if (Mesh.Shader.State == ShaderState.Dynamic)
            {
                Mesh.Shader.Update();
            }

            GL.DrawElements(PrimitiveType.Triangles, Mesh.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.Enable(EnableCap.DepthTest);
        }

        public Sprite(int width, int height, int x, int y, Texture texture)
        {
            Shader = new SpriteShader();
            Shader.Texture = texture;
            Mesh = new Mesh(Shader);
            Mesh.Indices = new uint[] { 0, 1, 2, 2, 3, 1 };
            Mesh.TextureCoordinates = new float[] { 0, 0, 1, 0, 0, 1, 1, 1 };
            vertices = new float[12];
            Width = width;
            Height = height;
            X = x;
            Y = y;
            Texture = texture;
        }
        public Sprite(int width, int height, Texture texture)
            : this(width, height, 0, 0, texture)
        {
        }
    }
}
