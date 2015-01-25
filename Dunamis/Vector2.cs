using System;

namespace Dunamis
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);
        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);

        #region Properties
        public float Length
        {
            get
            {
                return (float)System.Math.Sqrt(X * X + Y * Y);
            }
        }
        public float this[int index]
        {
            get
            {
                if (index == 0) return X;
                if (index == 1) return Y;
                throw new IndexOutOfRangeException("Index out of range: " + index);
            }
            set
            {
                if (index == 0) X = value;
                else if (index == 1) Y = value;
                else throw new IndexOutOfRangeException("Index out of range: " + index);
            }
        }
        #endregion

        #region Methods

        #region Override
        public bool Equals(Vector2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2 && Equals((Vector2)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode()*397) ^ Y.GetHashCode();
            }
        }
        #endregion

        #region Math
        public void Normalize()
        {
            float scale = 1.0f / this.Length;
            X *= scale;
            Y *= scale;
        }
        public static float Dot(Vector2 left, Vector2 right)
        {
            return left.X * right.X + left.Y * right.Y;
        }
        public static Vector2 Lerp(Vector2 left, Vector2 right, float blend)
        {
            left.X = blend * (right.X - left.X) + left.X;
            left.Y = blend * (right.Y - left.Y) + left.Y;
            return left;
        }
        #endregion

        #endregion

        #region Operators

        #region Math
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            return left;
        }
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            return left;
        }
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            return left;
        }
        public static Vector2 operator *(Vector2 vector, float scale)
        {
            vector.X *= scale;
            vector.Y *= scale;
            return vector;
        }
        public static Vector2 operator /(Vector2 left, Vector2 right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            return left;
        }
        public static Vector2 operator /(Vector2 vector, float scale)
        {
            float multiply = 1.0f / scale;
            vector.X *= multiply;
            vector.Y *= multiply;
            return vector;
        }
        public static Vector2 operator -(Vector2 vector)
        {
            vector.X = -vector.X;
            vector.Y = -vector.Y;
            return vector;
        }
        #endregion

        #region Equality
        public static bool operator ==(Vector2 left, Vector2 right) // TODO: add equality operators to all dunamis value types
        {
            return left.Equals(right);
        }
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region Conversion
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

        #endregion

        #region Constructors
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2(float value)
        {
            X = value;
            Y = value;
        }
        public Vector2(Vector2 v)
        {
            X = v.X;
            Y = v.Y;
        }
        public Vector2(Vector3 v)
        {
            X = v.X;
            Y = v.Y;
        }
        public Vector2(Vector4 v)
        {
            X = v.X;
            Y = v.Y;
        }
        #endregion
    }
}
