using System;
using OTK = OpenTK;

namespace Dunamis
{
    public class Quaternion
    {
        private OTK.Quaternion _quaternion;

        internal OTK.Quaternion OpenTK
        {
            get { return _quaternion; }
            set { _quaternion = value; }
        }

        public float X
        {
            get { return _quaternion.X; }
            set { _quaternion.X = value; }
        }
        public float Y
        {
            get { return _quaternion.Y; }
            set { _quaternion.Y = value; }
        }
        public float Z
        {
            get { return _quaternion.Z; }
            set { _quaternion.Z = value; }
        }
        public float W
        {
            get { return _quaternion.W; }
            set { _quaternion.W = value; }
        }

        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        public Quaternion(float x = 0f, float y = 0f, float z = 0f, float w = 0f)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        internal Quaternion(OTK.Quaternion q)
        {
            _quaternion = q;
        }

        public void Normalize()
        {
            _quaternion.Normalize();
        }

        public static Quaternion FromAxisAngle(Vector3 axis, float degrees)
        {
            return new Quaternion(OTK.Quaternion.FromAxisAngle((OTK.Vector3)axis, degrees));
        }

        public static Quaternion FromAxisAngle(Vector3 axis, Angle angle)
        {
            return Quaternion.FromAxisAngle(axis, angle.Degrees);
        }

        public static Quaternion FromAngles(float pitch, float roll, float yaw)
        {
            return new Quaternion(OTK.Quaternion.FromEulerAngles(pitch, yaw, roll));
        }

        public static Quaternion FromAngles(Vector3 angles)
        {
            return FromAngles(angles.X, angles.Y, angles.Z);
        }

        public static Quaternion Conjugate(Quaternion q)
        {
            return new Quaternion(OTK.Quaternion.Conjugate(q.OpenTK));
        }

        public static Quaternion Invert(Quaternion q)
        {
            return new Quaternion(OTK.Quaternion.Invert(q.OpenTK));
        }

        public static Quaternion Slerp(Quaternion a, Quaternion b, float blend)
        {
            return new Quaternion(OTK.Quaternion.Slerp(a.OpenTK, b.OpenTK, blend));
        }

        #region Operators

        public static Quaternion operator +(Quaternion a, Quaternion b)
        {
            a.OpenTK = a.OpenTK + b.OpenTK;
            return a;
        }

        public static Quaternion operator -(Quaternion a, Quaternion b)
        {
            a.OpenTK = a.OpenTK - b.OpenTK;
            return a;
        }

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            a.OpenTK = a.OpenTK * b.OpenTK;
            return a;
        }

        public static Quaternion operator *(Quaternion a, float scalar)
        {
            a.OpenTK = a.OpenTK * scalar;
            return a;
        }

        public static Quaternion operator *(float scalar, Quaternion a)
        {
            a.OpenTK = a.OpenTK * scalar;
            return a;
        }

        public static bool operator ==(Quaternion left, Quaternion right)
        {
            return left.OpenTK == right.OpenTK;
        }

        public static bool operator !=(Quaternion left, Quaternion right)
        {
            return left.OpenTK != right.OpenTK;
        }
        #endregion
    }
}