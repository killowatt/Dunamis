using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Mesh
    {
        internal int VertexArrayObject;
        internal int VertexBufferObject;
        internal int IndexBufferObject;

        float[] vertices;
        float[] textureCoordinates;
        float[] normals;
        uint[] indices;

        public Mesh()
        {
            GL.GenVertexArrays(1, out VertexArrayObject);
            GL.BindVertexArray(VertexArrayObject);

            GL.GenBuffers(1, out VertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.GenBuffers(1, out IndexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, IndexBufferObject);

            GL.BindVertexArray(0);

            vertices = new float[] { };
            textureCoordinates = new float[] { };
            normals = new float[] { };
            indices = new uint[] { };
        }
    }
}
