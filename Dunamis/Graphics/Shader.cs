using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public abstract class Shader
    {
        internal int ShaderProgram;
        internal int VertexShader;
        internal int FragmentShader;

        private Dictionary<string, int> uniforms;
        private Dictionary<string, int> textures;

        internal bool Initialized;
        public ShaderState State;

        public abstract void Initialize();
        public abstract void Update();

        #region Methods
        protected void addTexture(string identifier, Texture texture)
        {
            int number = textures.Count;
            textures.Add(identifier, number);

            GL.ActiveTexture((TextureUnit)33984 + number);
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.Uniform1(GL.GetUniformLocation(ShaderProgram, identifier), number);
        }
        protected void updateTexture(string identifier, Texture texture)
        {
            int number = textures[identifier];

            GL.ActiveTexture((TextureUnit)33984 + number);
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.Uniform1(GL.GetUniformLocation(ShaderProgram, identifier), number);
        }
        protected void addUniform(string identifier, Matrix4 matrix, bool transpose)
        {
            int uniform = GL.GetUniformLocation(ShaderProgram, identifier);
            uniforms.Add(identifier, uniform);
            GL.UniformMatrix4(uniform, transpose, ref matrix.Matrix);
        }
        protected void updateUniform(string identifier, Matrix4 matrix, bool transpose)
        {
            GL.UniformMatrix4(uniforms[identifier], transpose, ref matrix.Matrix);
        }
        protected void addUniform(string identifier, Matrix4[] matrices, bool transpose)
        {
            int uniform = GL.GetUniformLocation(ShaderProgram, identifier);
            uniforms.Add(identifier, uniform);
            List<float> matrixCollection = new List<float>();
            foreach (Matrix4 matrix in matrices)
            {
                foreach (float element in matrix.Array)
                {
                    matrixCollection.Add(element);
                }
            }
            GL.UniformMatrix4(uniform, matrices.Length, false, matrixCollection.ToArray());
        }
        // TODO: make these properties?
        public bool GetCompileStatus(ShaderType type)
        {
            int status = 0;
            if (type == ShaderType.Fragment)
            {
                GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out status);
            }
            else
            {
                GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out status);
            }

            switch (status)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    return false;
            }
        }
        public string GetCompileLog(ShaderType type)
        {
            if (type == ShaderType.Fragment)
            {
                return GL.GetShaderInfoLog(FragmentShader);
            }
            else
            {
                return GL.GetShaderInfoLog(VertexShader);
            }
        }
        internal void Setup(Mesh mesh)
        {
            GL.BindVertexArray(mesh.VertexArrayObject);
            GL.UseProgram(ShaderProgram);
            GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.VertexBufferObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.IndexBufferObject);

            // TODO: add error handling so if the shader doesn't have an attribute make the program shit
            long offset = 0;
            // TODO: print things if > -1 is true
            int vertex = GL.GetAttribLocation(ShaderProgram, "vertex");
            if (vertex > -1)
            {
                GL.VertexAttribPointer(vertex, 3, VertexAttribPointerType.Float, false, 0, new IntPtr(offset));
                GL.EnableVertexAttribArray(vertex);
            }

            int textureCoordinate = GL.GetAttribLocation(ShaderProgram, "textureCoordinate");
            if (textureCoordinate > -1)
            {
                GL.VertexAttribPointer(textureCoordinate, 2, VertexAttribPointerType.Float, false, 0, new IntPtr(offset += sizeof(float) * mesh.Vertices.Length));
                GL.EnableVertexAttribArray(textureCoordinate);
            }

            int normal = GL.GetAttribLocation(ShaderProgram, "normal");
            if (normal > -1)
            {
                GL.VertexAttribPointer(normal, 3, VertexAttribPointerType.Float, false, 0, new IntPtr(offset += sizeof(float) * mesh.TextureCoordinates.Length));
                GL.EnableVertexAttribArray(normal);
            }

            int boneVertex = GL.GetAttribLocation(ShaderProgram, "boneVertex");
            int boneWeight = GL.GetAttribLocation(ShaderProgram, "boneWeight");
            if (boneVertex > -1 || boneWeight > -1)
            {
                GL.VertexAttribPointer(boneVertex, 4, VertexAttribPointerType.Int, false, 0, new IntPtr(offset += sizeof(float) * mesh.Normals.Length));
                GL.EnableVertexAttribArray(boneVertex);
                GL.VertexAttribPointer(boneWeight, 4, VertexAttribPointerType.Float, false, 0, new IntPtr(offset += sizeof(int) * mesh.BoneIndices.Length));
                GL.EnableVertexAttribArray(boneWeight);
            }

            GL.UseProgram(0);
            GL.BindVertexArray(0);
        }
        #endregion

        protected Shader(string vertexSource, string fragmentSource, ShaderState state)
        {
            VertexShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, vertexSource);
            GL.CompileShader(VertexShader);

            FragmentShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, fragmentSource);
            GL.CompileShader(FragmentShader);

            ShaderProgram = GL.CreateProgram();
            GL.AttachShader(ShaderProgram, VertexShader);
            GL.AttachShader(ShaderProgram, FragmentShader);
            GL.BindFragDataLocation(ShaderProgram, 0, "color");
            GL.LinkProgram(ShaderProgram);

            textures = new Dictionary<string, int>();
            uniforms = new Dictionary<string, int>();

            State = state;
            Initialized = false;
        }
    }
}
