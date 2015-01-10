namespace Dunamis
{
    public struct Matrix4
    {
        public Vector4 Row0;
        public Vector4 Row1;
        public Vector4 Row2;
        public Vector4 Row3;

        public static readonly Matrix4 Identity = OpenTK.Matrix4.Identity; // TODO: lmao

        #region Methods
        public bool Equals(Matrix4 other)
        {
            return Row0.Equals(other.Row0) && Row1.Equals(other.Row1) && Row2.Equals(other.Row2) && Row3.Equals(other.Row3);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Matrix4 && Equals((Matrix4)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Row0.GetHashCode();
                hashCode = (hashCode * 397) ^ Row1.GetHashCode();
                hashCode = (hashCode * 397) ^ Row2.GetHashCode();
                hashCode = (hashCode * 397) ^ Row3.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        #region Operators
        // Math
        public static Matrix4 operator +(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.Row0 + right.Row0, left.Row1 + right.Row1, left.Row2 + right.Row2, left.Row3 + right.Row3);
        }
        public static Matrix4 operator -(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.Row0 - right.Row0, left.Row1 - right.Row1, left.Row2 - right.Row2, left.Row3 - right.Row3);
        }
        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return OpenTK.Matrix4.Mult(left, right);
        }
        public static Matrix4 operator /(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.Row0 / right.Row0, left.Row1 / right.Row1, left.Row2 / right.Row2, left.Row3 / right.Row3);
        }
        public static Matrix4 operator *(Matrix4 left, float right)
        {
            return new Matrix4(left.Row0 * right, left.Row1 * right, left.Row2 * right, left.Row3 * right);
        }

        // Equality
        public static bool operator ==(Matrix4 left, Matrix4 right)
        {
            return left.Row0 == right.Row0 && left.Row1 == right.Row1 && left.Row2 == right.Row2 && left.Row3 == right.Row3;
        }

        public static bool operator !=(Matrix4 left, Matrix4 right)
        {
            return !(left == right);
        }

        // Conversion
        public static implicit operator Matrix4(OpenTK.Matrix4 matrix)
        {
            return new Matrix4(matrix.Row0, matrix.Row1, matrix.Row2, matrix.Row3);
        }
        public static implicit operator OpenTK.Matrix4(Matrix4 matrix)
        {
            return new OpenTK.Matrix4(matrix.Row0, matrix.Row1, matrix.Row2, matrix.Row3);
        }
        #endregion

        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }
    }
}
