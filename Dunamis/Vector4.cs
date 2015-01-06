using OpenTK;

namespace Dunamis
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        #region Methods
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

        // Equality
        public static bool operator ==(Vector4 left, Vector4 right) // TODO: add equality operators to all dunamis value types
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z && left.W == right.W;
        }

        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return !(left == right);
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
        public static implicit operator Vector4d(Vector4 vector)
        {
            return new Vector4d(vector.X, vector.Y, vector.Z, vector.W);
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
        //public Vector4(byte r, byte g, byte b, byte a)
        //{
        //    X = r / 255;
        //    Y = g / 255;
        //    Z = b / 255;
        //    W = a / 255;
        //}
        #endregion
    }
}
