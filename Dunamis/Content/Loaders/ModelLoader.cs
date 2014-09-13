using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Graphics.Bone> bones = new List<Graphics.Bone>();
            List<uint> indices = new List<uint>();

            Mesh currentMesh = scene.Meshes[1]; // TODO: the index hhh
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
            if (currentMesh.HasBones)
            {
                foreach (Bone bone in currentMesh.Bones)
                {
                    Assimp.Matrix4x4 m = bone.OffsetMatrix;
                    Matrix4 offset = new Matrix4(m.A1, m.A2, m.A3, m.A4, m.B1, m.B2, m.B3, m.B4, m.C1, m.C2, m.C3, m.C4, m.D1, m.D2, m.D3, m.D4);
                    List<uint> vertexIndices = new List<uint>();
                    List<float> weights = new List<float>();
                    foreach (VertexWeight weight in bone.VertexWeights)
                    {
                        vertexIndices.Add((uint)weight.VertexID);
                        weights.Add(weight.Weight);
                    }
                    bones.Add(new Graphics.Bone(bone.Name, offset, vertexIndices.ToArray(), weights.ToArray()));
                }
            }

            for (int i = 0; i < bones.Count; i++)
            {
                
            }

            //List<int> boneIndices = new List<int>();
            //List<float> boneWeights = new List<float>();
            //uint[] boneIndices = new uint[8192];
            //float[] boneWeights = new float[8192];

            //float[] boneStuff = new float[8192];
            //foreach (Graphics.Bone bone in bones)
            //{
            //    for (uint i = 0; i < bone.Vertices.Length; i++)
            //    {
            //        boneStuff[bone.Vertices[i]] = bone.Weights[i];
            //    }
            //}

            //uint[,] boneIndices = new uint[8192, 4];
            //uint[] currentIndexIndex = new uint[8192];
            //float[,] boneWeights = new float[8192, 4];
            //uint[] currentWeightIndex = new uint[8192];
            //for (int bone = 0; bone < bones.Count; bone++)
            //{
            //    for (int index = 0; index < bones[bone].Vertices.Length; index++)
            //    {
            //        boneIndices[bones[bone].Vertices[index], currentIndexIndex[bone]] = (uint)bone;
            //        if (currentIndexIndex[bone] >= 3)
            //        {
            //            currentIndexIndex[bone] = 0;
            //        }
            //        //currentIndexIndex[bone]++;

            //        boneWeights[bones[bone].Vertices[index], currentWeightIndex[bone]] = bones[bone].Weights[index];
            //        if (currentWeightIndex[bone] >= 3)
            //        {
            //            currentWeightIndex[bone] = 0;
            //        }
            //        //currentIndexIndex[bone]++;
            //    }
            //}



            uint[,] boneInd = new uint[vertices.Count / 3, 4];
            int[] currentVectorIndexForBoneIndex = new int[8192];

            float[,] boneWei = new float[vertices.Count / 3, 4];
            int[] currentBoneWeightIndex = new int[8192];

            for (int i = 0; i < bones.Count; i++)
            {
                for (int x = 0; x < bones[i].Vertices.Length; x++)
                {
                    boneInd[bones[i].Vertices[x], currentVectorIndexForBoneIndex[bones[i].Vertices[x]]] = (uint)i;
                    boneWei[bones[i].Vertices[x], currentBoneWeightIndex[bones[i].Vertices[x]]] = bones[i].Weights[x];

                    if (currentVectorIndexForBoneIndex[bones[i].Vertices[x]] >= 3)
                    {
                        currentVectorIndexForBoneIndex[bones[i].Vertices[x]] = 0;
                    }
                    currentVectorIndexForBoneIndex[bones[i].Vertices[x]]++;

                    if (currentBoneWeightIndex[bones[i].Vertices[x]] >= 3)
                    {
                        currentBoneWeightIndex[bones[i].Vertices[x]] = 0;
                    }
                    currentBoneWeightIndex[bones[i].Vertices[x]]++;

                }
            }

            List<uint> ind = boneInd.Cast<uint>().ToList();
            List<float> wei = boneWei.Cast<float>().ToList();

            Graphics.Mesh mesh = new Graphics.Mesh();
            mesh.Vertices = vertices.ToArray();
            mesh.TextureCoordinates = textureCoordinates.ToArray();
            mesh.Normals = normals.ToArray();
            mesh.Bones = bones.ToArray();
            mesh.Indices = indices.ToArray();
            mesh.BoneIndices = ind.ToArray();
            mesh.BoneWeights = wei.ToArray();
            //mesh.BoneIndices = ind.ToArray();
            //mesh.BoneWeights = wei.ToArray();

            return mesh;
        }
    }
}
