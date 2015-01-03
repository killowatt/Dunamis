using System;

namespace Dunamis
{
    public struct Quaternion
    {
        Vector4 quaternion;

        #region Properties
        public float X
        {
            get
            {
                return quaternion.X;
            }
            set
            {
                quaternion.X = value;
            }
        }
        public float Y
        {
            get
            {
                return quaternion.Y;
            }
            set
            {
                quaternion.Y = value;
            }
        }
        public float Z
        {
            get
            {
                return quaternion.Z;
            }
            set
            {
                quaternion.Z = value;
            }
        }
        public float W
        {
            get
            {
                return quaternion.W;
            }
            set
            {
                quaternion.W = value;
            }
        }
        #endregion

        public Quaternion(float x, float y, float z, float w)
        {
            quaternion = new Vector4(x, y, z, w);
        }
    }
}
