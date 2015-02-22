﻿using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public enum ShaderState
    {
        Static,
        Dynamic
    }
    public enum ShaderType
    {
        Vertex,
        Fragment,
        Geometry
    }
    public abstract class Shader // TODO: automatically add input and camera uniforms at top of shader and tell user to write just main?
    {
        internal int ShaderProgram; // TODO: IDisposable shader/mesh/texture?
        internal int VertexShader;
        internal int FragmentShader;
        internal int GeometryShader = -1;

        Dictionary<string, int> _parameters;
        Dictionary<string, int> _textures;

        internal bool Initialized;
        protected internal Matrix4 Model;
        protected internal Matrix4 View;
        protected internal Matrix4 Projection;
        public ShaderState State;

        public abstract void Initialize(); // TODO: prevent outside classes from calling this whilst letting renderer call them
        public abstract void Update();

        #region Methods
        protected void AddParameter(string identifer, float value)
        {
            int location = GL.GetUniformLocation(ShaderProgram, identifer);
            GL.Uniform1(location, value);
            _parameters.Add(identifer, location);
        }
        protected void AddParameter(string identifier, Color4 color)
        {
            int location = GL.GetUniformLocation(ShaderProgram, identifier);
            GL.Uniform4(location, color.R / 255, color.G / 255, color.B / 255, color.A / 255);
            _parameters.Add(identifier, location);
        }
        protected void AddParameter(string identifier, Matrix4 matrix, bool transpose)
        {
            OpenTK.Matrix4 reference = matrix;
            int location = GL.GetUniformLocation(ShaderProgram, identifier);
            GL.UniformMatrix4(location, transpose, ref reference);
            _parameters.Add(identifier, location);
        }
        protected void UpdateParameter(string identifier, float value)
        {
            GL.Uniform1(_parameters[identifier], value);
        }
        protected void UpdateParameter(string identifier, Color4 color)
        {
            GL.Uniform4(_parameters[identifier], color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f);
        }
        protected void UpdateParameter(string identifier, Matrix4 matrix, bool transpose)
        {
            OpenTK.Matrix4 reference = matrix;
            GL.UniformMatrix4(_parameters[identifier], transpose, ref reference);
        }
        protected void AddTexture(string identifier, Texture texture)
        {
            int number = _textures.Count;
            int location = GL.GetUniformLocation(ShaderProgram, identifier);
            GL.ActiveTexture((TextureUnit)33984 + number);
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureId);
            GL.Uniform1(location, number);

            _parameters.Add(identifier, location);
            _textures.Add(identifier, number);
        }
        protected void UpdateTexture(string identifier, Texture texture)
        {
            GL.ActiveTexture((TextureUnit)33984 + _textures[identifier]);
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureId);
            GL.Uniform1(_parameters[identifier], _textures[identifier]);
        }
        public bool GetCompileStatus(ShaderType type)
        {
            int status = 0;
            if (type == ShaderType.Vertex)
            {
                GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out status);
            }
            else if (type == ShaderType.Fragment)
            {
                GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out status);
            }
            else if (type == ShaderType.Geometry)
            {
                GL.GetShader(GeometryShader, ShaderParameter.CompileStatus, out status);
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
            if (type == ShaderType.Vertex)
            {
                return GL.GetShaderInfoLog(VertexShader);
            }
            if (type == ShaderType.Geometry)
            {
                return GL.GetShaderInfoLog(GeometryShader);
            }
            return "GetCompileLog: Invalid Shader Type."; // TODO: standardize invalid method string returns? replace with error? etc.
        }

        #endregion

        protected Shader(string vertexSource, string fragmentSource, string geometrySource, ShaderState state)
        {
            VertexShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, vertexSource);
            GL.CompileShader(VertexShader);

            FragmentShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, fragmentSource);
            GL.CompileShader(FragmentShader);

            if (geometrySource != null)
            {
                GeometryShader = GL.CreateShader(OpenTK.Graphics.OpenGL.ShaderType.GeometryShader);
                GL.ShaderSource(GeometryShader, geometrySource);
                GL.CompileShader(GeometryShader);
            }

            ShaderProgram = GL.CreateProgram();
            GL.AttachShader(ShaderProgram, VertexShader);
            GL.AttachShader(ShaderProgram, FragmentShader);
            if (geometrySource != null) GL.AttachShader(ShaderProgram, GeometryShader);

            GL.BindFragDataLocation(ShaderProgram, 0, "color"); // TODO: remove
            GL.BindAttribLocation(ShaderProgram, 0, "vertex");
            GL.BindAttribLocation(ShaderProgram, 1, "textureCoordinate");
            GL.BindAttribLocation(ShaderProgram, 2, "normal");

            GL.LinkProgram(ShaderProgram);

            _parameters = new Dictionary<string, int>();
            _textures = new Dictionary<string, int>();

            State = state;
            Initialized = false;
        }
        protected Shader(string vertexSource, string fragmentSource, ShaderState state)
            : this(vertexSource, fragmentSource, null, state)
        {
        }
    }
}
