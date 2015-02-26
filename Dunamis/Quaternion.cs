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

        /// <summary>
        /// The X component of this quaternion.
        /// </summary>
        public float X
        {
            get { return _quaternion.X; }
            set { _quaternion.X = value; }
        }
        /// <summary>
        /// The Y component of this quaternion.
        /// </summary>
        public float Y
        {
            get { return _quaternion.Y; }
            set { _quaternion.Y = value; }
        }
        /// <summary>
        /// The Z component of this quaternion.
        /// </summary>
        public float Z
        {
            get { return _quaternion.Z; }
            set { _quaternion.Z = value; }
        }
        /// <summary>
        /// The W component of this quaternion.
        /// </summary>
        public float W
        {
            get { return _quaternion.W; }
            set { _quaternion.W = value; }
        }

        /// <summary>
        /// An identity quaternion.
        /// </summary>
        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        /// <summary>
        /// Creates a quaternion with the specified values.
        /// </summary>
        /// <param name="x">The X component of the quaternion.</param>
        /// <param name="y">The Y component of the quaternion.</param>
        /// <param name="z">The Z component of the quaternion.</param>
        /// <param name="w">The W component of the quaternion.</param>
        public Quaternion(float x = 0f, float y = 0f, float z = 0f, float w = 0f)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Creates a new quaternion from the specified OpenTK quaternion object.
        /// </summary>
        /// <param name="q">The OpenTK quaternion object.</param>
        internal Quaternion(OTK.Quaternion q)
        {
            _quaternion = q;
        }

        /// <summary>
        /// Normalizes this quaternion.
        /// </summary>
        public void Normalize()
        {
            _quaternion.Normalize();
        }

        /// <summary>
        /// Creates a new quaternion from the specified axis-angle orientation.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>A new quaternion representing the axis-angle orientation.</returns>
        public static Quaternion FromAxisAngle(Vector3 axis, float degrees)
        {
            return new Quaternion(OTK.Quaternion.FromAxisAngle((OTK.Vector3)axis, degrees));
        }

        /// <summary>
        /// Creates a new quaternion from the specified axis-angle orientation.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="degrees">The angle.</param>
        /// <returns>A new quaternion representing the axis-angle orientation.</returns>
        public static Quaternion FromAxisAngle(Vector3 axis, Angle angle)
        {
            return Quaternion.FromAxisAngle(axis, angle.Degrees);
        }

        /// <summary>
        /// Creates a new quaternion from the specified euler angles.
        /// </summary>
        /// <param name="pitch">The pitch angle.</param>
        /// <param name="roll">The roll angle.</param>
        /// <param name="yaw">The yaw angle.</param>
        /// <returns>A new quaternion representing a rotation of the specified euler angles.</returns>
        public static Quaternion FromAngles(float pitch, float roll, float yaw)
        {
            return new Quaternion(OTK.Quaternion.FromEulerAngles(pitch, yaw, roll));
        }

        /// <summary>
        /// Creates a new quaternion from the specified euler angles.
        /// </summary>
        /// <param name="angles">The euler angles in (pitch, roll, yaw) order.</param>
        /// <returns>A new quaternion representing a rotation of the specified euler angles.</returns>
        public static Quaternion FromAngles(Vector3 angles)
        {
            return FromAngles(angles.X, angles.Y, angles.Z);
        }

        /// <summary>
        /// Creates a new quaternion representing the conjugate of the specified quaternion.
        /// </summary>
        /// <param name="q">The quaternion to conjugate.</param>
        /// <returns>A new quaternion representing the conjugate of the specified quaternion.</returns>
        public static Quaternion Conjugate(Quaternion q)
        {
            return new Quaternion(OTK.Quaternion.Conjugate(q.OpenTK));
        }

        /// <summary>
        /// Creates a new quaternion that is the inverse of the specified quaternion.
        /// </summary>
        /// <param name="q">The quaternion to invert.</param>
        /// <returns>A new quaternion that is the inverse of the specified quaternion.</returns>
        public static Quaternion Invert(Quaternion q)
        {
            return new Quaternion(OTK.Quaternion.Invert(q.OpenTK));
        }

        /// <summary>
        /// Creates a new quaternion representing the spherical linear interpolation of two specified quaternions.
        /// </summary>
        /// <param name="a">The first quaternion.</param>
        /// <param name="b">The second quaternion.</param>
        /// <param name="blend">The degree of blending.</param>
        /// <returns>A new quaternion representing the slerp of the two specified quaternions.</returns>
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