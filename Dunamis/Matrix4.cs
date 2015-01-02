using System;

namespace Dunamis
{
    public struct Matrix4
    {
        public Vector4 Row0;
        public Vector4 Row1;
        public Vector4 Row2;
        public Vector4 Row3;

        public static implicit operator Matrix4(OpenTK.Matrix4 matrix)
        {
            return new Matrix4(matrix.Row0, matrix.Row1, matrix.Row2, matrix.Row3);
        }
        public static implicit operator OpenTK.Matrix4(Matrix4 matrix)
        {
            return new OpenTK.Matrix4(matrix.Row0, matrix.Row1, matrix.Row2, matrix.Row3);
        }

        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return new Matrix4(left.Row0 * right.Row0, left.Row1 * right.Row1, left.Row2 * right.Row2, left.Row3 * right.Row3);
        }

        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }
    }
}
