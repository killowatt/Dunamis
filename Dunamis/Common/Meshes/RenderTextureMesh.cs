using Dunamis.Common.Shaders;
using Dunamis.Graphics;

namespace Dunamis.Common.Meshes
{
    public class RenderTextureMesh : Mesh
    { // TODO: readonly these

        public static float[] RenderTextureVertices
        {
            get
            {
                return new float[]
                {
                    -1, -1, 0,
                    1, -1, 0,
                    1, 1, 0,
                    -1, 1, 0
                };
            }
        }
        public static float[] RenderTextureTextureCoordinates // TODO: this is a terrible name jesus christ
        {
            get
            {
                return new float[]
                {
                    0, 0,
                    1, 0,
                    1, 1,
                    0, 1
                };
            }
        }
        public static uint[] RenderTextureIndices
        {
            get
            {
                return new uint[]
                {
                    0, 1, 2,
                    2, 3, 0
                };
            }
        }
        public RenderTextureMesh(RenderTextureShader shader)
            : base(RenderTextureVertices, RenderTextureTextureCoordinates, new float[0], RenderTextureIndices, shader)
        {
        }
    }
}
