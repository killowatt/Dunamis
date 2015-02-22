using System;

namespace Dunamis
{
    public class Quaternion
    {
        public float X;
        public float Y;
        public float Z;
        // We're getting IMAGINARY up in this class.
        public float W;

        public static readonly Quaternion Identity = new Quaternion(0, 0, 0, 1);

        public Quaternion(float x = 0f, float y = 0f, float z = 0f, float w = 0f)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        //https://github.com/mrdoob/three.js/blob/master/src/math/Quaternion.js
        public static Quaternion FromAxisAngle(Vector3 axis, float degrees)
        {
            float sin = (float)Math.Sin(degrees / 2f);

            return new Quaternion(axis.X * sin, axis.Y * sin, axis.Z * sin, (float)Math.Cos(degrees / 2f));
        }

        public static Quaternion FromAxisAngle(Vector3 axis, Angle angle)
        {
            return Quaternion.FromAxisAngle(axis, angle.Degrees);
        }

        public static Quaternion FromAngles(float pitch, float roll, float yaw, EulerOrder order = EulerOrder.XYZ)
        {
            Quaternion q = new Quaternion();

            float c1 = (float)Math.Cos(pitch / 2);
            float c2 = (float)Math.Cos(roll / 2);
            float c3 = (float)Math.Cos(yaw / 2);
            float s1 = (float)Math.Sin(pitch / 2);
            float s2 = (float)Math.Sin(roll / 2);
            float s3 = (float)Math.Sin(yaw / 2);

            if (order == EulerOrder.XYZ)
            {
                q.X = s1 * c2 * c3 + c1 * s2 * s3;
                q.Y = c1 * s2 * c3 - s1 * c2 * s3;
                q.Z = c1 * c2 * s3 + s1 * s2 * c3;
                q.W = c1 * c2 * c3 - s1 * s2 * s3;
            }
            else if (order == EulerOrder.YXZ)
            {
                q.X = s1 * c2 * c3 + c1 * s2 * s3;
                q.Y = c1 * s2 * c3 - s1 * c2 * s3;
                q.Z = c1 * c2 * s3 - s1 * s2 * c3;
                q.W = c1 * c2 * c3 + s1 * s2 * s3;
            }
            else if (order == EulerOrder.ZXY)
            {
                q.X = s1 * c2 * c3 - c1 * s2 * s3;
                q.Y = c1 * s2 * c3 + s1 * c2 * s3;
                q.Z = c1 * c2 * s3 + s1 * s2 * c3;
                q.W = c1 * c2 * c3 - s1 * s2 * s3;
            }
            else if (order == EulerOrder.ZYX)
            {
                q.X = s1 * c2 * c3 - c1 * s2 * s3;
                q.Y = c1 * s2 * c3 + s1 * c2 * s3;
                q.Z = c1 * c2 * s3 - s1 * s2 * c3;
                q.W = c1 * c2 * c3 + s1 * s2 * s3;
            }
            else if (order == EulerOrder.YZX)
            {
                q.X = s1 * c2 * c3 + c1 * s2 * s3;
                q.Y = c1 * s2 * c3 + s1 * c2 * s3;
                q.Z = c1 * c2 * s3 - s1 * s2 * c3;
                q.W = c1 * c2 * c3 - s1 * s2 * s3;
            }
            else if (order == EulerOrder.XZY)
            {
                q.X = s1 * c2 * c3 - c1 * s2 * s3;
                q.Y = c1 * s2 * c3 - s1 * c2 * s3;
                q.Z = c1 * c2 * s3 + s1 * s2 * c3;
                q.W = c1 * c2 * c3 + s1 * s2 * s3;
            }

            return q;
        }

        public static Quaternion FromAngles(Vector3 angles, EulerOrder order = EulerOrder.XYZ)
        {
            return FromAngles(angles.X, angles.Y, angles.Z, order);
        }

        public static float Dot(Quaternion q, Vector4 v)
        {
            return q.X * v.X + q.Y * v.Y + q.Z * v.Z + q.W * v.W; 
        }

        public static Quaternion operator *(Quaternion a, Quaternion b)
        {
            Quaternion q = new Quaternion();

            q.X = a.X * b.W + a.W * b.X + a.Y * b.Z - a.Z * b.Y;
            q.Y = a.Y * b.W + a.W * b.Y + a.Z * b.X - a.X * b.Z;
            q.Z = a.Z * b.W + a.W * b.Z + a.X * b.Y - a.Y * b.X;
            q.W = a.W * b.W - a.X * b.X - a.Y * b.Y - a.Z * b.Z;

            return q;
        }
    }

    public enum EulerOrder
    {
        XYZ,
        YXZ,
        ZXY,
        ZYX,
        YZX,
        XZY
    }
}