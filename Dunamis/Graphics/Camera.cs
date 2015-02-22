namespace Dunamis.Graphics
{
    public class Camera // TODO: convert to quat (gltut)
    {
        Matrix4 _view;      
        Matrix4 _projection;
        bool _viewCalculated;
        bool _projectionCalculated;

        Vector3 _position;
        Vector3 _rotation;
        Angle _fieldOfView;
        Vector2 _aspect;
        float _zNear;
        float _zFar;

        internal Matrix4 Projection2D;

        // TODO: add errors if didn't clear/display?
        #region Properties
        public Matrix4 View
        {
            get
            {
                if (!_viewCalculated)
                {
                    _view = OpenTK.Matrix4.LookAt(_position, _position + new Vector3(0, 0, -1), OpenTK.Vector3.UnitY) *
                        OpenTK.Matrix4.CreateRotationZ(_rotation.Z) *
                        OpenTK.Matrix4.CreateRotationY(_rotation.Y) *
                        OpenTK.Matrix4.CreateRotationX(_rotation.X);
                    _viewCalculated = true;
                }
                return _view;
            }
            set
            {
                _view = value;
            }
        }
        public Matrix4 Projection
        {
            get
            {
                if (!_projectionCalculated) // TODO: prevent arugment out of range exception
                {
                    _projection = OpenTK.Matrix4.CreatePerspectiveFieldOfView(_fieldOfView.Radians, _aspect.X / _aspect.Y, 0.5f, 1024); // TODO: investigate znear and zfar more
                    _projectionCalculated = true;
                }
                return _projection;
            }
            set
            {
                _projection = value;
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
                    _viewCalculated = false;
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
                    _viewCalculated = false;
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
                    _viewCalculated = false;
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
                    _viewCalculated = false;
                }
            }
        }
        public float FieldOfView
        {
            get
            {
                return _fieldOfView.Degrees;
            }
            set
            {
                if (_fieldOfView.Degrees != value)
                {
                    _fieldOfView.Degrees = value;
                    _projectionCalculated = false;
                }
            }
        }
        public Vector2 Aspect
        {
            get
            {
                return _aspect;
            }
            set
            {
                if (_aspect != value)
                {
                    _aspect = value;
                    _projectionCalculated = false;
                }
            }
        }
        public float ZNear
        {
            get
            {
                return _zNear;
            }
            set
            {
                if (_zNear != value)
                {
                    _zNear = value;
                    _projectionCalculated = false;
                }
            }
        }
        public float ZFar
        {
            get
            {
                return _zFar;
            }
            set
            {
                if (_zFar != value)
                {
                    _zFar = value;
                    _projectionCalculated = false;
                }
            }
        }
        #endregion

        public Camera()
        {
            _position = new Vector3(0.0f, 0.0f, 0.0f);
            _rotation = new Vector3(0.0f, 0.0f, 0.0f);
            _fieldOfView = Angle.CreateDegrees(90).Radians;
            _aspect = new Vector2(16, 9);
        }
        public Camera(Vector3 position, float pitch, float yaw, float roll, float fieldOfView, Vector2 aspect)
        {
            Position = position;
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
            FieldOfView = fieldOfView;
            Aspect = aspect;

            Projection2D = OpenTK.Matrix4.CreateOrthographicOffCenter(-0.5f, 1280 - 0.5f, 720 - 0.5f, -0.5f, 0, 1); // TODO: 0.375
        }
    }
}

