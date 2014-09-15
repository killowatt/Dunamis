using System;
using OpenTK;

namespace Dunamis.Graphics
{
    public class Bone
    {
        public string Name;
        public Matrix4 Offset;
        public Matrix4 Transformation;
        public VertexWeight[] VertexWeights;
        public Bone Parent;
        public Bone[] Children;

        public Bone(string name, Matrix4 offset, VertexWeight[] vertexWeights)
        {
            Name = name;
            Offset = offset;
            VertexWeights = vertexWeights;
            Transformation = new Matrix4(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
        }
    }
    public struct VertexWeight
    {
        public uint Vertex;
        public float Weight;

        public VertexWeight(uint vertex, float weight)
        {
            Vertex = vertex;
            Weight = weight;
        }
    }
}
