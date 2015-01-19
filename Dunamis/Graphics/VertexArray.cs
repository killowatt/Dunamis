using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    internal sealed class VertexArray
    {
        internal int VertexArrayObject;

        int verticesID;
        int textureCoordinatesID;
        int normalsID;
        int indicesID;

        float[] _vertices;
        float[] _textureCoordinates;
        float[] _normals;
        uint[] _indices;

        public float[] Vertices
        {
            get { return _vertices; }
            set
            {
                _vertices = value;
                GL.BindBuffer(BufferTarget.ArrayBuffer, verticesID);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * _vertices.Length), _vertices, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(0);
            }
        }
        public float[] TextureCoordinates
        {
            get { return _textureCoordinates; }
            set
            {
                _textureCoordinates = value;
                GL.BindBuffer(BufferTarget.ArrayBuffer, textureCoordinatesID);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * _textureCoordinates.Length), _textureCoordinates, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(1);
            }
        }
        public float[] Normals
        {
            get { return _normals; }
            set
            {
                _normals = value;
                GL.BindBuffer(BufferTarget.ArrayBuffer, normalsID);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * _normals.Length), _normals, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(2);
            }
        }
        public uint[] Indices
        {
            get { return _indices; }
            set
            {
                _indices = value;
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, indicesID);
                GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(uint) * _indices.Length), _indices,
                    BufferUsageHint.StaticDraw);
            }
        }

        internal VertexArray(float[] vertices, float[] textureCoordinates, float[] normals, uint[] indices)
        {
            GL.GenVertexArrays(1, out VertexArrayObject);
            GL.BindVertexArray(VertexArrayObject);

            _vertices = vertices;
            _textureCoordinates = textureCoordinates;
            _normals = normals;
            _indices = indices;

            GL.GenBuffers(1, out verticesID);
            GL.GenBuffers(1, out textureCoordinatesID);
            GL.GenBuffers(1, out normalsID);
            GL.GenBuffers(1, out indicesID);

            Vertices = vertices;
            TextureCoordinates = textureCoordinates;
            Normals = normals;
            Indices = indices;

            GL.BindVertexArray(0);
        }
    }
}
