using System;
using System.Collections.Generic;
using Assimp;
using Dunamis;

namespace Dunamis.Content.Loaders
{
    public class ModelLoader : ILoader<Graphics.Mesh>
    {
        public Graphics.Mesh Load(string filename)
        {
            AssimpContext context = new AssimpContext();
            Scene scene = context.ImportFile(filename);

            List<float> vertices = new List<float>();
            List<float> textureCoordinates = new List<float>();
            List<float> normals = new List<float>();
            List<uint> indices = new List<uint>();

            Mesh currentMesh = scene.Meshes[1];
            if (currentMesh.HasVertices)
            {
                foreach (Vector3D vector in currentMesh.Vertices)
                {
                    vertices.Add(vector.X);
                    vertices.Add(vector.Y);
                    vertices.Add(vector.Z);
                }
            }
            if (currentMesh.HasTextureCoords(0))
            {
                foreach (Vector3D textureCoordinate in currentMesh.TextureCoordinateChannels[0])
                {
                    textureCoordinates.Add(textureCoordinate.X);
                    textureCoordinates.Add(textureCoordinate.Y);
                }
            }
            if (currentMesh.HasNormals)
            {
                foreach (Vector3D normal in currentMesh.Normals)
                {
                    normals.Add(normal.X);
                    normals.Add(normal.Y);
                    normals.Add(normal.Z);
                }
            }
            if (currentMesh.HasFaces)
            {
                foreach (Face face in currentMesh.Faces)
                {
                    indices.Add((uint)face.Indices[0]);
                    indices.Add((uint)face.Indices[1]);
                    indices.Add((uint)face.Indices[2]);
                }
            }

            Graphics.Mesh mesh = new Graphics.Mesh();
            mesh.Vertices = vertices.ToArray();
            mesh.TextureCoordinates = textureCoordinates.ToArray();
            mesh.Normals = normals.ToArray();
            mesh.Indices = indices.ToArray();

            return mesh;
        }
    }
}
