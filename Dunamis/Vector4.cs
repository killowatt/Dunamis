using System;

namespace Dunamis
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        #region Operators
        // Math
        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }
        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }
        public static Vector4 operator *(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
        }
        public static Vector4 operator /(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
        }

        // Conversion
        public static implicit operator Vector4(Vector2 vector)
        {
            return new Vector4(vector.X, vector.Y, 0, 0);
        }
        public static implicit operator Vector4(Vector3 vector)
        {
            return new Vector4(vector.X, vector.Y, vector.Z, 0);
        }
        public static implicit operator Vector4(Color4 color)
        {
            return new Vector4(color.R, color.G, color.B, color.A);
        }
        public static implicit operator OpenTK.Vector4(Vector4 vector)
        {
            return new OpenTK.Vector4(vector.X, vector.Y, vector.Z, vector.W);
        }
        public static implicit operator Vector4(OpenTK.Vector4 vector)
        {
            return new Vector4(vector.X, vector.Y, vector.Z, vector.W);
        }
        #endregion

        #region Constructors
        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4(byte r, byte g, byte b, byte a)
        {
            X = r / 255;
            Y = g / 255;
            Z = b / 255;
            W = a / 255;
        }
        #endregion
    }
}
