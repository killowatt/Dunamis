using System;

namespace Dunamis
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public static implicit operator OpenTK.Vector4(Vector4 vector)
        {
            return new OpenTK.Vector4(vector.X, vector.Y, vector.Z, vector.W);
        }
        public static implicit operator Vector4(OpenTK.Vector4 vector)
        {
            return new Vector4(vector.X, vector.Y, vector.Z, vector.W);
        }

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
