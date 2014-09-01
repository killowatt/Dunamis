using System;
using Dunamis.Common.Shaders;
using Dunamis.Graphics;

namespace Dunamis.Common.Meshes
{
    public class RenderTextureMesh : Mesh
    {
        public RenderTextureMesh(RenderTextureShader shader)
        {
            Vertices = new float[]
            {
                -1, -1, 0,
                1, -1, 0,
                1, 1, 0,
                -1, 1, 0,
            };
            TextureCoordinates = new float[]
            {
                0, 0,
                1, 0,
                1, 1,
                0, 1
            };
            Indices = new uint[]
            {
                0, 1, 2,
                2, 3, 0
            };
            Shader = shader;
        }
    }
}
