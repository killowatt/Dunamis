using System.IO.IsolatedStorage;
using Dunamis.Common.Shaders; // TODO: still unsatisfied with premade asset sorting, fix!!
using Dunamis.Graphics;

namespace Dunamis.Common.Meshes
{
    public class Cube : Mesh
    {
        public static float[] CubeVertices // TODO: SHOULD WE RETURN A COPY?
        {
            get
            {
                return new float[]
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
            }
        }
        public static float[] CubeTextureCoordinates
        {
            get
            {
                return new float[]
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
            }
        }
        public static uint[] CubeIndices
        {
            get
            {
                return new uint[]
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
                };
            }
        } // TODO: cube normals
        public Cube(Shader shader) : base(CubeVertices, CubeTextureCoordinates, new float[0], CubeIndices, shader)
        {
        }
        public Cube() : base(CubeVertices, CubeTextureCoordinates, new float[0], CubeIndices, new BasicTexturedShader())
        {
        }
    }
}
