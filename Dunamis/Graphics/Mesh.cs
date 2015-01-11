using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Mesh
    {
        internal int VertexArrayObject;
        internal int VertexBufferObject;
        internal int IndexBufferObject;

        float[] _vertices;
        float[] _textureCoordinates;
        float[] _normals;
        uint[] _indices;

        Shader _shader;
        MeshType _type;

        Matrix4 _transform;
        bool _transformCalculated;
        Vector3 _position;
        Vector3 _rotation;
        Vector3 _scale;

        #region Properties
        public float[] Vertices
        {
            get
            {
                return _vertices;
            }
        }
        public float[] TextureCoordinates
        {
            get
            {
                return _textureCoordinates;
            }
        }
        public float[] Normals
        {
            get
            {
                return _normals;
            }
        }
        public uint[] Indices
        {
            get
            {
                return _indices;
            }
        }
        public Shader Shader
        {
            get
            {
                return _shader;
            }
        }
        public MeshType Type
        {
            get
            {
                return _type;
            }
        }
        public Matrix4 Transform
        {
            get
            {
                if (!_transformCalculated)
                {
                    _transform = OpenTK.Matrix4.CreateRotationX(_rotation.X) *
                        OpenTK.Matrix4.CreateRotationY(_rotation.Y) *
                        OpenTK.Matrix4.CreateRotationZ(_rotation.Z) * 
                        OpenTK.Matrix4.CreateTranslation(_position) *
                        OpenTK.Matrix4.CreateScale(_scale);
                    _transformCalculated = true;
                }
                return _transform;
            }
        }
        public Vector3 Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    _transformCalculated = false;
                }
            }
        }
        public float X
        {
            get
            {
                return _position.X;
            }
            set
            {
                if (_position.X != value)
                {
                    _position.X = value;
                    _transformCalculated = false;
                }
            }
        }
        public float Y
        {
            get
            {
                return _position.Y;
            }
            set
            {
                if (_position.Y != value)
                {
                    _position.Y = value;
                    _transformCalculated = false;
                }
            }
        }
        public float Z
        {
            get
            {
                return _position.Z;
            }
            set
            {
                if (_position.Z != value)
                {
                    _position.Z = value;
                    _transformCalculated = false;
                }
            }
        }
        public float Pitch
        {
            get
            {
                return _rotation.X;
            }
            set
            {
                if (_rotation.X != value)
                {
                    _rotation.X = value;
                    _transformCalculated = false;
                }
            }
        }
        public float Yaw
        {
            get
            {
                return _rotation.Y;
            }
            set
            {
                if (_rotation.Y != value)
                {
                    _rotation.Y = value;
                    _transformCalculated = false;
                }
            }
        }
        public float Roll
        {
            get
            {
                return _rotation.Z;
            }
            set
            {
                if (_rotation.Z != value)
                {
                    _rotation.Z = value;
                    _transformCalculated = false;
                }
            }
        }
        public Vector3 Scale
        {
            get
            {
                return _scale;
            }
        }
        #endregion

        #region Methods
        public void SetMesh(float[] vertices = null, float[] textureCoordinates = null, float[] normals = null, uint[] indices = null, MeshType type = MeshType.Static) // TODO: include documentation that states how meshtype works with relation to how often you via this method
        {
            _vertices = vertices ?? new float[0];
            _textureCoordinates = textureCoordinates ?? new float[0];
            _normals = normals ?? new float[0];
            _indices = indices ?? new uint[0];
            _type = type;

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

            long size = sizeof(float) * (_vertices.Length + _textureCoordinates.Length + _normals.Length);
            long offset = 0;

            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(size), IntPtr.Zero, usageHint);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset), new IntPtr(sizeof(float) * _vertices.Length), _vertices);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset += sizeof(float) * _vertices.Length), new IntPtr(sizeof(float) * _textureCoordinates.Length), textureCoordinates);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(offset += sizeof(float) * _textureCoordinates.Length), new IntPtr(sizeof(float) * _normals.Length), normals);

            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * _indices.Length), indices, usageHint);

            GL.BindVertexArray(0);
        }
        public void SetShader(Shader shader) // TODO: maybe put this in a property instead.
        {
            _shader = shader;

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
                GL.VertexAttribPointer(textureCoordinate, 2, VertexAttribPointerType.Float, false, 0, new IntPtr(offset += sizeof(float) * _vertices.Length));
                GL.EnableVertexAttribArray(textureCoordinate);
            }
            int normal = GL.GetAttribLocation(shader.ShaderProgram, "normal");
            if (normal > -1)
            {
                GL.VertexAttribPointer(normal, 3, VertexAttribPointerType.Float, false, 0, new IntPtr(offset += sizeof(float) * _textureCoordinates.Length));
            }

            GL.UseProgram(0);
            GL.BindVertexArray(0);
        }
        public void SetScale(float scale)
        {
            if (new Vector3(scale) != _scale)
            {
                _scale = new Vector3(scale);
                _transformCalculated = false;
            }
        }
        public void SetScale(Vector3 scale)
        {
            if (scale != _scale)
            {
                _scale = scale;
                _transformCalculated = false;
            }
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
            _transform = Matrix4.Identity;
            _scale = new Vector3(1);
        }
        public Mesh(float[] vertices = null, float[] textureCoordinates = null, float[] normals = null, uint[] indices = null, MeshType type = MeshType.Static, Shader shader = null) // TODO: CREATE A DEFAULT SHADER
            : this()
        {
            SetMesh(vertices, textureCoordinates, normals, indices, type);
            SetShader(shader);
        }
        #endregion
    }
}
