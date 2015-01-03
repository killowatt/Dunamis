using System;

namespace Dunamis.Graphics
{
    public class Camera
    {
        Matrix4 view;      
        Matrix4 projection;
        bool viewCalculated;
        bool projectionCalculated;

        Vector3 position;
        Vector3 rotation;
        Angle fieldOfView;
        Vector2 aspect;

        #region Properties
        public Matrix4 View
        {
            get
            {
                if (!viewCalculated)
                {
                    view = OpenTK.Matrix4.LookAt(position, position + new Vector3(0, 0, -1), OpenTK.Vector3.UnitY) *
                        OpenTK.Matrix4.CreateRotationX(rotation.X) *
                        OpenTK.Matrix4.CreateRotationY(rotation.Y) *
                        OpenTK.Matrix4.CreateRotationZ(rotation.Z);
                    viewCalculated = true;
                }
                return view;
            }
        }
        public Matrix4 Projection
        {
            get
            {
                if (!projectionCalculated)
                {
                    projection = OpenTK.Matrix4.CreatePerspectiveFieldOfView(fieldOfView.Radians, aspect.X / aspect.Y, 0.5f, 1024); // TODO: investigate znear and zfar more
                    projectionCalculated = true;
                }
                return projection;
            }
        }
        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                if (position != value)
                {
                    position = value;
                    viewCalculated = false;
                }
            }
        }
        public float Pitch
        {
            get
            {
                return rotation.X;
            }
            set
            {
                if (rotation.X != value)
                {
                    rotation.X = value;
                    viewCalculated = false;
                }
            }
        }
        public float Yaw
        {
            get
            {
                return rotation.Y;
            }
            set
            {
                if (rotation.Y != value)
                {
                    rotation.Y = value;
                    viewCalculated = false;
                }
            }
        }
        public float Roll
        {
            get
            {
                return rotation.Z;
            }
            set
            {
                if (rotation.Z != value)
                {
                    rotation.Z = value;
                    viewCalculated = false;
                }
            }
        }
        public float FieldOfView
        {
            get
            {
                return fieldOfView.Degrees;
            }
            set
            {
                if (fieldOfView.Degrees != value)
                {
                    fieldOfView.Degrees = value;
                    projectionCalculated = false;
                }
            }
        }
        public Vector2 Aspect
        {
            get
            {
                return aspect;
            }
            set
            {
                if (aspect != value)
                {
                    aspect = value;
                    projectionCalculated = false;
                }
            }
        }
        #endregion

        public Camera()
        {
            position = new Vector3(0.0f, 0.0f, 0.0f);
            rotation = new Vector3(0.0f, 0.0f, 0.0f);
            fieldOfView = Angle.CreateDegrees(90).Radians;
            aspect = new Vector2(16, 9);
        }
        public Camera(Vector3 position, float pitch, float yaw, float roll, float fieldOfView, Vector2 aspect)
        {
            Position = position;
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
            FieldOfView = fieldOfView;
            Aspect = aspect;
        }
    }
}

