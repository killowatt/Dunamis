using System;
using OpenTK;

namespace Dunamis
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public static readonly Vector4 UnitX = new Vector4(1, 0, 0, 0);
        public static readonly Vector4 UnitY = new Vector4(0, 1, 0, 0);
        public static readonly Vector4 UnitZ = new Vector4(0, 0, 1, 0);
        public static readonly Vector4 UnitW = new Vector4(0, 0, 0, 1);
        public static readonly Vector4 Zero = new Vector4(0, 0, 0, 0);
        public static readonly Vector4 One = new Vector4(1, 1, 1, 1);

        #region Properties
        public float Length
        {
            get
            {
                return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
            }
        }
        public float this[int index]
        {
            get
            {
                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;
                if (index == 3) return W;
                throw new IndexOutOfRangeException("Index out of range: " + index);
            }
            set
            {
                if (index == 0) X = value;
                else if (index == 1) Y = value;
                else if (index == 2) Z = value;
                else if (index == 3) W = value;
                else throw new IndexOutOfRangeException("Index out of range: " + index);
            }
        }
        #endregion

        #region Methods

        #region Override
        public bool Equals(Vector4 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector4 && Equals((Vector4)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                hashCode = (hashCode * 397) ^ W.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        #region Math
        public void Normalize()
        {
            float scale = 1.0f / Length;
            X *= scale;
            Y *= scale;
            Z *= scale;
            W *= scale;
        }
        public static float Dot(Vector4 left, Vector4 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
        }
        public static Vector4 Lerp(Vector4 left, Vector4 right, float blend)
        {
            left.X = blend * (right.X - left.X) + left.X;
            left.Y = blend * (right.Y - left.Y) + left.Y;
            left.Z = blend * (right.Z - left.Z) + left.Z;
            left.W = blend * (right.W - left.W) + left.W;
            return left;
        }
        #endregion

        #endregion

        #region Operators

        #region Math
        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            left.W += right.W;
            return left;
        }
        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            left.W -= right.W;
            return left;
        }
        public static Vector4 operator *(Vector4 left, Vector4 right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            left.W *= right.W;
            return left;
        }
        public static Vector4 operator *(Vector4 vector, float scale)
        {
            vector.X *= scale;
            vector.Y *= scale;
            vector.Z *= scale;
            vector.W *= scale;
            return vector;
        }
        public static Vector4 operator /(Vector4 left, Vector4 right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            left.W *= right.W;
            return left;
        }
        public static Vector4 operator /(Vector4 vector, float scale)
        {
            float multiply = 1.0f / scale;
            vector.X *= multiply;
            vector.Y *= multiply;
            vector.Z *= multiply;
            vector.W *= multiply;
            return vector;
        }
        public static Vector4 operator -(Vector4 vector)
        {
            vector.X = -vector.X;
            vector.Y = -vector.Y;
            vector.Z = -vector.Z;
            vector.W = -vector.W;
            return vector;
        }
        #endregion

        #region Equality
        public static bool operator ==(Vector4 left, Vector4 right) // TODO: add equality operators to all dunamis value types
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region Conversion
        public static implicit operator Vector4(Vector2 vector)
        {
            return new Vector4(vector.X, vector.Y, 0, 0);
        }
        public static implicit operator Vector4(Vector3 vector)
        {
            return new Vector4(vector.X, vector.Y, vector.Z, 0);
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

        #endregion

        #region Constructors
        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4(float value)
            : this(value, value, value, value)
        {
        }
        public Vector4(Vector2 vector)
            : this(vector.X, vector.Y, 0, 0)
        {
        }
        public Vector4(Vector3 vector)
            : this(vector.X, vector.Y, vector.Z, 0)
        {
        }
        public Vector4(Vector3 vector, float w)
            : this(vector.X, vector.Y, vector.Z, w)
        {
        }
        public Vector4(Vector4 vector)
            : this(vector.X, vector.Y, vector.Z, vector.W)
        {
        }
        #endregion
    }
}
