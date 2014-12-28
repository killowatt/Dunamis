using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public abstract class Shader
    {
        internal int ShaderProgram;
        internal int VertexShader;
        internal int FragmentShader;

        Dictionary<string, int> parameters;
        Dictionary<string, int> textures;

        internal bool Initialized;
        public ShaderState State;

        public abstract void Initialize();
        public abstract void Update();

        protected void addParameter(string identifer, float value)
        {
            int location = GL.GetUniformLocation(ShaderProgram, identifer);
            GL.Uniform1(location, value);

            parameters.Add(identifer, location);
        }
        protected void updateParameter(string identifier, float value)
        {
            GL.Uniform1(parameters[identifier], value);
        }
        protected void addTexture(string identifier, Texture texture)
        {
            int number = textures.Count;
            int location = GL.GetUniformLocation(ShaderProgram, identifier);
            GL.ActiveTexture((TextureUnit)33984 + number);
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.Uniform1(location, number);

            parameters.Add(identifier, location);
            textures.Add(identifier, number);
        }
        protected void updateTexture(string identifier, Texture texture)
        {
            GL.ActiveTexture((TextureUnit)33984 + textures[identifier]);
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.Uniform1(parameters[identifier], textures[identifier]);
        }

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

            parameters = new Dictionary<string, int>();
            textures = new Dictionary<string, int>();

            State = state;
            Initialized = false;
        }
    }
}
