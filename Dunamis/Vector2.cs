using System;

namespace Dunamis
{
    public struct Vector2
    {
        public float X;
        public float Y;

        #region Operators
        // Math
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }
        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X / right.X, left.Y / right.Y);
        }

        // Conversion
        public static explicit operator Vector2(Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
        public static explicit operator Vector2(Vector4 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
        public static implicit operator OpenTK.Vector2(Vector2 vector)
        {
            return new OpenTK.Vector2(vector.X, vector.Y);
        }
        public static implicit operator Vector2(OpenTK.Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }
        #endregion

        #region Constructors
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        #endregion
    }
}
