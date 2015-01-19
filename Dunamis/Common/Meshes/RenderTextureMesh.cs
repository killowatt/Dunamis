using Dunamis.Common.Shaders;
using Dunamis.Graphics;

namespace Dunamis.Common.Meshes
{
    public class RenderTextureMesh : Mesh
    { // TODO: readonly these
        public static readonly float[] AVertices = {
                -1, -1, 0,
                1, -1, 0,
                1, 1, 0,
                -1, 1, 0
            };
        public static readonly float[] ATextureCoordinates = { // TODO: COPY. PRIVATE READONLY THIS, THEN MAKE A PROPERTY THAT COPIES THE ARRAY AND RETURNS IT
                0, 0,
                1, 0,
                1, 1,
                0, 1
            };
        public static readonly uint[] AIndices = {
                0, 1, 2,
                2, 3, 0
            };
        public RenderTextureMesh(RenderTextureShader shader)
            : base(AVertices, ATextureCoordinates, new float[0], AIndices, shader)
        {
        }
    }
}
