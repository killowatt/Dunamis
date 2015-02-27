namespace Dunamis
{
    public struct Matrix4
    {
        /// <summary>
        /// The first row of the matrix.
        /// </summary>
        public Vector4 Row0;
        /// <summary>
        /// The second row of the matrix.
        /// </summary>
        public Vector4 Row1;
        /// <summary>
        /// The third row of the matrix.
        /// </summary>
        public Vector4 Row2;
        /// <summary>
        /// The fourth row of the matrix.
        /// </summary>
        public Vector4 Row3;

        /// <summary>
        /// The matrix identity.
        /// </summary>
        public static readonly Matrix4 Identity = OpenTK.Matrix4.Identity; // TODO: lmao

        #region Methods
        /// <summary>
        /// Extracts the rotation component of this matrix.
        /// </summary>
        /// <returns>The rotation component of this matrix.</returns>
        public Quaternion ExtractRotation()
        {
            return new Quaternion(((OpenTK.Matrix4)this).ExtractRotation());
        }
        /// <summary>
        /// Extracts the translation component of this matrix.
        /// </summary>
        /// <returns>The translation component of this matrix.</returns>
        public Vector3 ExtractTranslation()
        {
            return ((OpenTK.Matrix4)this).ExtractTranslation();
        }
        /// <summary>
        /// Extracts the projection component of this matrix.
        /// </summary>
        /// <returns>The projection component of this matrix.</returns>
        public Vector4 ExtractProjection()
        {
            return ((OpenTK.Matrix4)this).ExtractProjection();
        }
        /// <summary>
        /// Extracts the scale component of this matrix.
        /// </summary>
        /// <returns>The scale component of this matrix.</returns>
        public Vector3 ExtractScale()
        {
            return ((OpenTK.Matrix4)this).ExtractScale();
        }
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
        /// <summary>
        /// Creates a new matrix from the specified angle in radians.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A matrix representing an X rotation of the specified angle.</returns>
        public static Matrix4 CreateRotationX(float angle)
        {
            return OpenTK.Matrix4.CreateRotationX(angle);
        }
        /// <summary>
        /// Creates a new matrix from the specified angle object.
        /// </summary>
        /// <param name="angle">The angle object.</param>
        /// <returns>A matrix representing an X rotation of the specified angle.</returns>
        public static Matrix4 CreateRotationX(Angle angle)
        {
            return OpenTK.Matrix4.CreateRotationX(angle.Radians);
        }
        /// <summary>
        /// Creates a new matrix from the specified angle in radians.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A matrix representing a Y rotation of the specified angle.</returns>
        public static Matrix4 CreateRotationY(float angle)
        {
            return OpenTK.Matrix4.CreateRotationY(angle);
        }
        /// <summary>
        /// Creates a new matrix from the specified angle object.
        /// </summary>
        /// <param name="angle">The angle object.</param>
        /// <returns>A matrix representing an Y rotation of the specified angle.</returns>
        public static Matrix4 CreateRotationY(Angle angle)
        {
            return OpenTK.Matrix4.CreateRotationY(angle.Radians);
        }
        /// <summary>
        /// Creates a new matrix from the specified angle in radians.
        /// </summary>
        /// <param name="angle">The angle in radians.</param>
        /// <returns>A matrix representing a Z rotation of the specified angle.</returns>
        public static Matrix4 CreateRotationZ(float angle)
        {
            return OpenTK.Matrix4.CreateRotationZ(angle);
        }
        /// <summary>
        /// Creates a new matrix from the specified angle object.
        /// </summary>
        /// <param name="angle">The angle object.</param>
        /// <returns>A matrix representing an Z rotation of the specified angle.</returns>
        public static Matrix4 CreateRotationZ(Angle angle)
        {
            return OpenTK.Matrix4.CreateRotationZ(angle.Radians);
        }
        /// <summary>
        /// Creates a new matrix from the specified vector.
        /// </summary>
        /// <param name="vector">The vector to translate by.</param>
        /// <returns>A matrix representing a translation of the specified vector.</returns>
        public static Matrix4 CreateTranslation(Vector3 vector)
        {
            return OpenTK.Matrix4.CreateTranslation(vector);
        }
        /// <summary>
        /// Creates a matrix from the specified scale amount in a vector.
        /// </summary>
        /// <param name="scale">The vector to scale by.</param>
        /// <returns>A matrix representing a scale of the specified vector.</returns>
        public static Matrix4 CreateScale(Vector3 scale)
        {
            return OpenTK.Matrix4.CreateScale(scale);
        }
        /// <summary>
        /// Creates a matrix from the specified scale amount.
        /// </summary>
        /// <param name="scale">The amount to scale by.</param>
        /// <returns>A matrix representing a scale of the specified value.</returns>
        public static Matrix4 CreateScale(float scale)
        {
            return OpenTK.Matrix4.CreateScale(scale);
        }
        /// <summary>
        /// Creates a matrix representing an orthographic projection.
        /// </summary>
        /// <param name="width">The projection width.</param>
        /// <param name="height">The projection height.</param>
        /// <param name="zNear">The projection's near parameter.</param>
        /// <param name="zFar">The projection's far parameter.</param>
        /// <returns>A matrix representing an orthographic projection.</returns>
        public static Matrix4 CreateOrthographic(float width, float height, float zNear, float zFar)
        {
            return OpenTK.Matrix4.CreateOrthographic(width, height, zNear, zFar);
        }
        /// <summary>
        /// Creates a matrix that "looks at" a specified target point.
        /// </summary>
        /// <param name="eye">The point that the matrix will originate from.</param>
        /// <param name="target">The point that the matrix will point to.</param>
        /// <param name="up">The up direction.</param>
        /// <returns>A matrix that looks at the specified target.</returns>
        public static Matrix4 LookAt(Vector3 eye, Vector3 target, Vector3 up)
        {
            return OpenTK.Matrix4.LookAt(eye, target, up);
        }
        /// <summary>
        /// Creates a matrix from the specified axis-angle orientation.
        /// </summary>
        /// <param name="axis">The axis of the orientation.</param>
        /// <param name="angle">The angle of the orientation.</param>
        /// <returns>A matrix that represents the specified axis-angle orientation.</returns>
        public static Matrix4 FromAxisAngle(Vector3 axis, float angle)
        {
            return OpenTK.Matrix4.CreateFromAxisAngle(axis, angle);
        }

        /// <summary>
        /// Creates a matrix with the specified values.
        /// </summary>
        /// <param name="row0">The first row of the matrix.</param>
        /// <param name="row1">The second row of the matrix.</param>
        /// <param name="row2">The third row of the matrix.</param>
        /// <param name="row3">The fourth row of the matrix.</param>
        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }
    }
}
