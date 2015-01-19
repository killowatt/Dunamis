using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Mesh // TODO: recreate mesh class with ideas in mind.
    {
        internal VertexArray VertexArray;
        public Shader Shader;

        Matrix4 _transform;
        bool _transformCalculated;
        Vector3 _position;
        Vector3 _rotation;
        Vector3 _scale;

        #region Properties
        public float[] Vertices
        {
            get { return VertexArray.Vertices; }
            set { VertexArray.Vertices = value; }
        }
        public float[] TextureCoordinates
        {
            get { return VertexArray.TextureCoordinates; }
            set { VertexArray.TextureCoordinates = value; }
        }
        public float[] Normals
        {
            get { return VertexArray.Normals; }
            set { VertexArray.Normals = value; }
        }
        public uint[] Indices
        {
            get { return VertexArray.Indices; }
            set { VertexArray.Indices = value; }
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
        public Mesh(float[] vertices, float[] textureCoordinates, float[] normals, uint[] indices, Shader shader) // TODO: CREATE A DEFAULT SHADER
        {
            Shader = shader;
            VertexArray = new VertexArray(vertices, textureCoordinates, normals, indices);
            _transform = Matrix4.Identity;
            _scale = new Vector3(1);
        }
        public Mesh(Shader shader)
            : this(new float[0], new float[0], new float[0], new uint[0], shader)
        {
        }
        #endregion
    }
}
