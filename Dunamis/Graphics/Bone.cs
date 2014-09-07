using System;
using OpenTK;

namespace Dunamis.Graphics
{
    public struct Bone
    {
        public string Name;
        public Matrix4 Offset;
        public VertexWeight[] Weights;

        public Bone(string name, Matrix4 offset, VertexWeight[] weights)
        {
            Name = name;
            Offset = offset;
            Weights = weights;
        }
    }
    public struct VertexWeight
    {
        public int Index;
        public float Weight;

        public VertexWeight(int index, float weight)
        {
            Index = index;
            Weight = weight;
        }
    }
}
