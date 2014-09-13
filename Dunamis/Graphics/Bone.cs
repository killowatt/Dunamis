using System;
using OpenTK;

namespace Dunamis.Graphics
{
    public struct Bone
    {
        public string Name;
        public Matrix4 Offset;
        public uint[] Vertices;
        public float[] Weights;

        public Bone(string name, Matrix4 offset, uint[] vertices, float[] weights)
        {
            Name = name;
            Offset = offset;
            Vertices = vertices;
            Weights = weights;
        }
    }
}
