using System;

namespace Dunamis
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        // Directions
        public static readonly Vector3 Up = new Vector3(0, 1, 0);
        public static readonly Vector3 Down = new Vector3(0, -1, 0);
        public static readonly Vector3 Front = new Vector3(0, 0, -1);
        public static readonly Vector3 Back = new Vector3(0, 0, 1);
        public static readonly Vector3 Left = new Vector3(-1, 0, 0);
        public static readonly Vector3 Right = new Vector3(1, 0, 0);

        #region Properties
        public float Length
        {
            get
            {
                return (float)System.Math.Sqrt(X * X + Y * Y + Z * Z);
            }
        }
        public float this[int index]
        {
            get
            {
                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;
                throw new IndexOutOfRangeException("Index out of range: " + index);
            }
            set
            {
                if (index == 0) X = value;
                else if (index == 1) Y = value;
                else if (index == 2) Z = value;
                else throw new IndexOutOfRangeException("Index out of range: " + index);
            }
        }
        #endregion

        #region Methods

        #region Override
        public bool Equals(Vector3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector3 && Equals((Vector3)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
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
        }
        public void ApplyQuaternion(Quaternion q)
        {
            Quaternion i = new Quaternion();
            i.X = q.W * X + q.Y * Z - q.Z * Y;
            i.Y = q.W * Y + q.Z * X - q.X * Z;
            i.Z = q.W * Z + q.X * Y - q.Y * X;
            i.W = -q.X * X - q.Y * Y - q.Z * Z;

            X = i.X * q.W + i.W * -q.X + i.Y * -q.Z - i.Z * -q.Y;
            Y = i.Y * q.W + i.W * -q.Y + i.Z * -q.X - i.X * -q.Z;
            Z = i.Z * q.W + i.W * -q.Z + i.X * -q.Y - i.Y * -q.X;
        }

        public static float Dot(Vector3 left, Vector3 right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }
        public static Vector3 Lerp(Vector3 left, Vector3 right, float blend)
        {
            left.X = blend * (right.X - left.X) + left.X;
            left.Y = blend * (right.Y - left.Y) + left.Y;
            left.Z = blend * (right.Z - left.Z) + left.Z;
            return left;
        }
        #endregion

        #endregion

        #region Operators

        #region Math
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            left.X += right.X;
            left.Y += right.Y;
            left.Z += right.Z;
            return left;
        }
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            left.X -= right.X;
            left.Y -= right.Y;
            left.Z -= right.Z;
            return left;
        }
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            return left;
        }
        public static Vector3 operator *(Vector3 vector, float scale)
        {
            vector.X *= scale;
            vector.Y *= scale;
            vector.Z *= scale;
            return vector;
        }
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            left.X *= right.X;
            left.Y *= right.Y;
            left.Z *= right.Z;
            return left;
        }
        public static Vector3 operator /(Vector3 vector, float scale)
        {
            float multiply = 1.0f / scale;
            vector.X *= multiply;
            vector.Y *= multiply;
            vector.Z *= multiply;
            return vector;
        }
        public static Vector3 operator -(Vector3 vector)
        {
            vector.X = -vector.X;
            vector.Y = -vector.Y;
            vector.Z = -vector.Z;
            return vector;
        }
        #endregion

        #region Equality
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return !left.Equals(right);
        }
        #endregion

        #region Conversion
        public static implicit operator Vector3(Vector2 vector)
        {
            return new Vector3(vector.X, vector.Y, 0);
        }
        public static explicit operator Vector3(Vector4 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
        public static implicit operator OpenTK.Vector3(Vector3 vector)
        {
            return new OpenTK.Vector3(vector.X, vector.Y, vector.Z);
        }
        public static implicit operator Vector3(OpenTK.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
        #endregion

        #endregion

        #region Constructors
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }
        public Vector3(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = 0.0f;
        }
        public Vector3(Vector3 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }
        public Vector3(Vector4 vector)
        {
            X = vector.X;
            Y = vector.Y;
            Z = vector.Z;
        }
        #endregion
    }
}
