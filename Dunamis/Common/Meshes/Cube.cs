using System.IO.IsolatedStorage;
using Dunamis.Common.Shaders;
using Dunamis.Graphics;

namespace Dunamis.Common.Meshes
{
    public class Cube : Mesh
    { // TODO: readonly these
        public static readonly new float[] AVertices = // TODO: FIX THIS SHIT
        {
            -1, 1, -1,
            -1, 1, 1,
            1, 1, 1,
            1, 1, -1,
            1, -1, -1,
            1, -1, 1,
            -1, -1, 1,
            -1, -1, -1
        };
        public new static readonly float[] ATextureCoordinates =
        {
            0, 0,
            0, 1,
            0, 1,
            0, 0,
            0, 1,
            1, 0,
            0, 1,
            1, 1
        };
        public static readonly new uint[] AIndices =
        {
            0, 1, 2,
            2, 3, 0,
            4, 5, 7,
            7, 6, 5,
            3, 4, 5,
            3, 2, 5,
            6, 5, 2,
            1, 6, 2,
            7, 6, 1,
            0, 7, 1,
            7, 4, 3,
            0, 7, 3
        }; // TOOD: REMOVE THIS PARAM SINCE WE USE DEFAULT SAHDER ANYWAY
        public Cube(Shader shader) : base(AVertices, ATextureCoordinates, new float[0], AIndices, shader)
        {
        }
    }
}
