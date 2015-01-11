using Dunamis.Common.Shaders;
using Dunamis.Graphics;

namespace Dunamis.Common.Meshes
{
    public class Cube : Mesh
    { // TODO: readonly these
        public new static float[] Vertices =
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
        public new static uint[] Indices =
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
        public Cube(Shader shader) : base(Vertices, indices: Indices, type: MeshType.Static, shader: shader)
        {
        }
    }
}
