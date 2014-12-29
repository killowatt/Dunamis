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

        Shader shader;
        public Matrix4 Transform;
        MeshType type;

        #region Properties
        public float[] Vertices
        {
            get
            {
                return vertices;
            }
        }
        public float[] TextureCoordinates
        {
            get
            {
                return textureCoordinates;
            }
        }
        public float[] Normals
        {
            get
            {
                return normals;
            }
        }
        public uint[] Indices
        {
            get
            {
                return indices;
            }
        }
        public Shader Shader
        {
            get
            {
                return shader;
            }
        }
        //public Vector3 Position
        //{
        //}
        #endregion

        #region Methods
        public void SetMesh(float[] vertices, float[] textureCoordinates, float[] normals, uint[] indices, MeshType type)
        {
            this.vertices = vertices;
            this.textureCoordinates = textureCoordinates;
            this.normals = normals;
            this.indices = indices;

            BufferUsageHint usageHint = new BufferUsageHint();
            if (type == MeshType.Static)
            {
                usageHint = BufferUsageHint.StaticDraw;
            }
            else if (type == MeshType.Dynamic)
            {
                usageHint = BufferUsageHint.DynamicDraw;
            }
            else if (type == MeshType.Stream)
            {
                usageHint = BufferUsageHint.StreamDraw;
            }

            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);

            long size = sizeof(float) * (vertices.Length + textureCoordinates.Length + normals.Length);
            long offset = 0;

            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), IntPtr.Zero, usageHint);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset), new IntPtr(sizeof(float) * vertices.Length), vertices);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset += sizeof(float) * vertices.Length), new IntPtr(sizeof(float) * textureCoordinates.Length), textureCoordinates);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset += sizeof(float) * textureCoordinates.Length), new IntPtr(sizeof(float) * normals.Length), normals);

            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * indices.Length), indices, usageHint);

            GL.BindVertexArray(0);
        }
        public void SetMesh(float[] vertices, float[] textureCoordinates, float[] normals, uint[] indices)
        {
            SetMesh(vertices, textureCoordinates, normals, indices, MeshType.Dynamic);
        }
        public void SetShader(Shader shader)
        {
            this.shader = shader;

            GL.BindVertexArray(VertexArrayObject);
            GL.UseProgram(shader.ShaderProgram);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);

            long offset = 0;

            int vertex = GL.GetAttribLocation(shader.ShaderProgram, "vertex");
            if (vertex > -1)
            {
                GL.VertexAttribPointer(vertex, 3, VertexAttribPointerType.Float, false, 0, new IntPtr(offset));
                GL.EnableVertexAttribArray(vertex);
            }
            int textureCoordinate = GL.GetAttribLocation(shader.ShaderProgram, "textureCoordinate");
            if (textureCoordinate > -1)
            {
                GL.VertexAttribPointer(textureCoordinate, 2, VertexAttribPointerType.Float, false, 0, new IntPtr(offset += sizeof(float) * vertices.Length));
                GL.EnableVertexAttribArray(textureCoordinate);
            }
            int normal = GL.GetAttribLocation(shader.ShaderProgram, "normal");
            if (normal > -1)
            {
                GL.VertexAttribPointer(normal, 3, VertexAttribPointerType.Float, false, 0, new IntPtr(offset += sizeof(float) * textureCoordinates.Length));
            }

            GL.UseProgram(0);
            GL.BindVertexArray(0);
        }
        #endregion

        #region Constructors
        public Mesh()
        {
            GL.GenVertexArrays(1, out VertexArrayObject);
            GL.BindVertexArray(VertexArrayObject);

            GL.GenBuffers(1, out VertexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);

            GL.GenBuffers(1, out IndexBufferObject);
            GL.BindBuffer(BufferTarget.ArrayBuffer, IndexBufferObject);

            GL.BindVertexArray(0);
        }
        public Mesh(float[] vertices, float[] textureCoordinates, float[] normals, uint[] indices, MeshType type, Shader shader)
            : this()
        {
            SetMesh(vertices, textureCoordinates, normals, indices, type);
            SetShader(shader);
        }
        public Mesh(float[] vertices, float[] textureCoordinates, float[] normals, uint[] indices)
            : this()
        {
            SetMesh(vertices, textureCoordinates, normals, indices);
        }
        #endregion
    }
}
