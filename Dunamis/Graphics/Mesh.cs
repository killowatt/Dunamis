using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Mesh
    {
        // TODO: utilize the bufferusagehints, make an enum or some shit and make ite ffect the thing
        internal int VertexArrayObject;
        internal int VertexBufferObject;
        internal int IndexBufferObject;

        float[] vertices;
        float[] textureCoordinates;
        float[] normals;
        uint[] indices;

        public Matrix4 ModelMatrix;
        private Shader shader;

        #region Properties
        public float[] Vertices
        {
            get
            {
                return vertices;
            }
            set
            {
                vertices = value;
                update();
            }
        }
        public float[] TextureCoordinates
        {
            get
            {
                return textureCoordinates;
            }
            set
            {
                textureCoordinates = value;
                update();
            }
        }
        public float[] Normals
        {
            get
            {
                return normals;
            }
            set
            {
                normals = value;
                update();
            }
        }
        public uint[] Indices
        {
            get
            {
                return indices;
            }
            set
            {
                indices = value;
                GL.BindVertexArray(VertexArrayObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * Indices.Length), Indices, BufferUsageHint.StaticDraw);
                GL.BindVertexArray(0);
            }
        }
        public Shader Shader
        {
            get
            {
                return shader;
            }
            set
            {
                shader = value;
                shader.Setup(this);
            }
        }
        #endregion

        #region Methods
        private void update()
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            // TODO: make this look prettier?
            long size = sizeof(float) * (vertices.Length + textureCoordinates.Length + normals.Length);
            long offset = 0;

            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), IntPtr.Zero, BufferUsageHint.StaticDraw);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset), new IntPtr(sizeof(float) * vertices.Length), vertices);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset += sizeof(float) * vertices.Length), new IntPtr(sizeof(float) * textureCoordinates.Length), textureCoordinates);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset += sizeof(float) * textureCoordinates.Length), new IntPtr(sizeof(float) * normals.Length), normals);

            GL.BindVertexArray(0);
        }
        #endregion

        public Mesh()
        {
            GL.GenVertexArrays(1, out VertexArrayObject);
            GL.BindVertexArray(VertexArrayObject);

            GL.GenBuffers(1, out IndexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);

            GL.GenBuffers(1, out VertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.BindVertexArray(0);

            vertices = new float[] { };
            textureCoordinates = new float[] { };
            normals = new float[] { };
            indices = new uint[] { };
            ModelMatrix = OpenTK.Matrix4.CreateTranslation(0, 0, 0);
        }
    }
}
