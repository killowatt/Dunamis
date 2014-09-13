using System;

namespace Dunamis
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;
        // TODO: upgrade vectors to matrix4 style structure
        // TODO: implement implicit conversions to all opentk types
        // TODO: add == operator, + operator, etc. for all data types like this

        public OpenTK.Vector3 ToToolkit() // TODO: is this needed?
        {
            return new OpenTK.Vector3(X, Y, Z);
        }
        public static implicit operator OpenTK.Vector3(Vector3 vector)
        {
            return new OpenTK.Vector3(vector.X, vector.Y, vector.Z);
        }
        public static implicit operator Vector3(OpenTK.Vector3 vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
