using System;
using OpenTK;

namespace Dunamis.Graphics
{
    // TODO: consider reverse calculating to get lookat, position, etc
    // TODO: consider making calculated into projectionCalculated and viewCalculated, sacrificing perty codes
    public class Camera
    {
        Matrix4 projection;
        Matrix4 view;

        float fieldOfView;
        Vector3 position;
        Vector2 rotation;
        Vector2 aspect;
        bool calculated;

        #region Properties
        public Matrix4 Projection
        {
            get
            {
                if (!calculated)
                {
                    calculate();
                }
                return projection;
            }
        }
        public Matrix4 View
        {
            get
            {
                if (!calculated)
                {
                    calculate();
                }
                return view;
            }
        }
        public float FieldOfView
        {
            get
            {
                return fieldOfView;
            }
            set
            {
                if (fieldOfView != value)
                {
                    fieldOfView = value;
                    calculated = false;
                }
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
                if (!position.Equals(value))
                {
                    position = value;
                    calculated = false;
                }
            }
        }
        public Vector2 Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                if (!rotation.Equals(value))
                {
                    rotation = value;
                    calculated = false;
                }
            }
        }
        public float X
        {
            get
            {
                return position.X;
            }
            set
            {
                if (position.X != value)
                {
                    position.X = value;
                    calculated = false;
                }
            }
        }
        public float Y
        {
            get
            {
                return position.Y;
            }
            set
            {
                if (position.Y != value)
                {
                    position.Y = value;
                    calculated = false;
                }
            }
        }
        public float Z
        {
            get
            {
                return position.Z;
            }
            set
            {
                if (position.Z!= value)
                {
                    position.Z = value;
                    calculated = false;
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
                    calculated = false;
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
                    calculated = false;
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
                if (!aspect.Equals(value))
                {
                    aspect = value;
                    calculated = false;
                }
            }
        }
        #endregion

        public void LookAt(Vector3 location)
        {
            // TODO: fix it, it doesnt really work that well
            //location.ToToolkit().Normalize();
            //float pitch = (float)Math.Asin(-location.Y);
            //float yaw = (float)Math.Atan2(location.X, -location.Z);
            //rotation.X = pitch * (180 / (float)Math.PI);
            //rotation.Y = yaw * (180 / (float)Math.PI) + 180;
            //calculate();

            // TEMP FIX
            view = Matrix4.LookAt(position, location, new OpenTK.Vector3(0, 1, 0));
            projection = Matrix4.CreatePerspectiveFieldOfView((fieldOfView * (float)Math.PI / 180), aspect.X / aspect.Y, 0.5f, 1024);
            calculated = true;
        }

        private void calculate()
        {
            projection = Matrix4.CreatePerspectiveFieldOfView((fieldOfView * (float)Math.PI / 180), aspect.X / aspect.Y, 0.5f, 1024);

            Matrix4 translation = Matrix4.CreateTranslation(position);
            Matrix4 rotationX = Matrix4.CreateRotationX(rotation.X * ((float)Math.PI / 180));
            Matrix4 rotationY = Matrix4.CreateRotationY(rotation.Y * ((float)Math.PI / 180));
            view = translation * (rotationX * rotationY);

            calculated = true;
        }

        public Camera(Vector3 position, Vector2 rotation, float fieldOfView, Vector2 aspect)
        {
            Position = position;
            Rotation = rotation;
            FieldOfView = fieldOfView;
            Aspect = aspect;
        }
    }
}
