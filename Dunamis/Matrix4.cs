// TODO: make this happen when you dont feel like thinking but feel like being productive

using System;
using System.Runtime.CompilerServices;
using OpenTK;

namespace Dunamis
{
    public struct Matrix4 // DUNAMIS VALUE STRUCTURE 2.0 OH BOY, STACK THAT STUFF. INLINE METHODDSSSSS
    {
        internal OpenTK.Matrix4 Matrix; // TODO: maybe make all internal thingies reference this instead of using the operator?

        #region Properties
        public Vector4 Row0
        {
            get
            {
                return Matrix.Row0;
            }
            set
            {
                Matrix.Row0 = value;
            }
        }
        public Vector4 Row1
        {
            get
            {
                return Matrix.Row1;
            }
            set
            {
                Matrix.Row1 = value;
            }
        }
        public Vector4 Row2
        {
            get
            {
                return Matrix.Row2;
            }
            set
            {
                Matrix.Row2 = value;
            }
        }
        public Vector4 Row3
        {
            get
            {
                return Matrix.Row3;
            }
            set
            {
                Matrix.Row3 = value;
            }
        }
        public float[] Array
        {
            get
            {
                return new float[]
                {
                    Row0.X, Row0.Y, Row0.Z, Row0.W,
                    Row1.X, Row1.Y, Row1.Z, Row1.W,
                    Row2.X, Row2.Y, Row2.Z, Row2.W,
                    Row3.X, Row3.Y, Row3.Z, Row3.W
                };
            }
        }
        #endregion

        #region Translation
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 CreateTranslation(Vector3 vector)
        {
            return OpenTK.Matrix4.CreateTranslation(vector);
        }
        #endregion

        #region Rotation
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 CreateRotationX(float angle)
        {
            return OpenTK.Matrix4.CreateRotationX(angle);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 CreateRotationY(float angle)
        {
            return OpenTK.Matrix4.CreateRotationY(angle);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 CreateRotationZ(float angle)
        {
            return OpenTK.Matrix4.CreateRotationZ(angle);
        }
        #endregion

        #region Misc
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float zNear, float zFar)
        {
            return OpenTK.Matrix4.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, zNear, zFar);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4 LookAt(Vector3 camera, Vector3 target, Vector3 up)
        {
            return OpenTK.Matrix4.LookAt(camera, target, up);
        }
        #endregion

        #region Operators
        public static implicit operator OpenTK.Matrix4(Matrix4 matrix)
        {
            return new OpenTK.Matrix4(matrix.Row0, matrix.Row1, matrix.Row2, matrix.Row3);
        }
        public static implicit operator Matrix4(OpenTK.Matrix4 matrix)
        {
            return new Matrix4(matrix.Row0, matrix.Row1, matrix.Row2, matrix.Row3);
        }
        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return (OpenTK.Matrix4)left * (OpenTK.Matrix4)right;
        }
        #endregion

        public static Matrix4 Identity
        {
            get
            {
                return OpenTK.Matrix4.Identity;
            }
        }

        public Matrix4(Vector4 row0, Vector4 row1, Vector4 row2, Vector4 row3)
        {
            Matrix = new OpenTK.Matrix4(row0, row1, row2, row3);
        }
        public Matrix4(float m00, float m01, float m02, float m03,
            float m10, float m11, float m12, float m13,
            float m20, float m21, float m22, float m23,
            float m30, float m31, float m32, float m33)
        {
            Matrix = new OpenTK.Matrix4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
        }

    }
}
