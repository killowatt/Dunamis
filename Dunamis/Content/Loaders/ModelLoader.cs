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
            List<Graphics.Animation> animations = new List<Graphics.Animation>();
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
                    List<Graphics.VertexWeight> weights = new List<Graphics.VertexWeight>();
                    foreach (VertexWeight weight in bone.VertexWeights)
                    {
                        weights.Add(new Graphics.VertexWeight((uint)weight.VertexID, weight.Weight));
                    }
                    Assimp.Matrix4x4 m = bone.OffsetMatrix;
                    Matrix4 offset = new Matrix4(m.A1, m.A2, m.A3, m.A4, m.B1, m.B2, m.B3, m.B4, m.C1, m.C2, m.C3, m.C4, m.D1, m.D2, m.D3, m.D4);
                    bones.Add(new Graphics.Bone(bone.Name, offset, weights.ToArray()));
                }
                for (int i = 0; i < bones.Count; i++)
                {
                }
            }
            if (scene.HasAnimations)
            {
                foreach(Animation animation in scene.Animations)
                {
                    Graphics.Animation anim;
                    List<Graphics.AnimationChannel> channels = new List<Graphics.AnimationChannel>();
                    foreach (NodeAnimationChannel channel in animation.NodeAnimationChannels)
                    {
                        Graphics.AnimationChannel chan;
                        List<Graphics.VectorKey> positionKeys = new List<Graphics.VectorKey>();
                        List<Graphics.QuaternionKey> rotationKeys = new List<Graphics.QuaternionKey>();
                        List<Graphics.VectorKey> scalingKeys = new List<Graphics.VectorKey>();
                        foreach (VectorKey key in channel.PositionKeys)
                        {
                            Vector3 vec = new Vector3(key.Value.X, key.Value.Y, key.Value.Z);
                            Graphics.VectorKey positionKey = new Graphics.VectorKey(key.Time, vec);
                            positionKeys.Add(positionKey);
                        }
                        foreach (QuaternionKey key in channel.RotationKeys)
                        {
                            Vector4 vec = new Vector4(key.Value.X, key.Value.Y, key.Value.Z, key.Value.W);
                            Graphics.QuaternionKey rotationKey = new Graphics.QuaternionKey(key.Time, vec);
                            rotationKeys.Add(rotationKey);
                        }
                        foreach (VectorKey key in channel.ScalingKeys)
                        {
                            Vector3 vec = new Vector3(key.Value.X, key.Value.Y, key.Value.Z);
                            Graphics.VectorKey scalingKey = new Graphics.VectorKey(key.Time, vec);
                            scalingKeys.Add(scalingKey);
                        }
                        chan = new Graphics.AnimationChannel(channel.NodeName, positionKeys.ToArray(), rotationKeys.ToArray(), scalingKeys.ToArray());
                        channels.Add(chan);
                    }
                    anim = new Graphics.Animation(channels.ToArray(), animation.TicksPerSecond, animation.DurationInTicks);
                    animations.Add(anim);
                }
            }

            uint[,] boneVertices = new uint[vertices.Count / 3, 4];
            float[,] boneWeights = new float[vertices.Count / 3, 4];
            uint[] VectorIndex = new uint[vertices.Count / 3];

            for (int i = 0; i < bones.Count; i++)
            {
                for (int j = 0; j < bones[i].VertexWeights.Length; j++)
                {
                    int vertex = (int)bones[i].VertexWeights[j].Vertex;
                    boneVertices[vertex, VectorIndex[vertex]] = (uint)i;
                    boneWeights[vertex, VectorIndex[vertex]] = bones[i].VertexWeights[j].Weight;

                    if (VectorIndex[bones[i].VertexWeights[j].Vertex] >= 3)
                    {
                        VectorIndex[bones[i].VertexWeights[j].Vertex] = 0;
                    }
                    else
                    {
                        VectorIndex[bones[i].VertexWeights[j].Vertex]++;
                    }
                }
            }

            #region rek
            //if (currentMesh.HasBones)
            //{
            //    foreach (Bone bone in currentMesh.Bones)
            //    {
            //        Assimp.Matrix4x4 m = bone.OffsetMatrix;
            //        Matrix4 offset = new Matrix4(m.A1, m.A2, m.A3, m.A4, m.B1, m.B2, m.B3, m.B4, m.C1, m.C2, m.C3, m.C4, m.D1, m.D2, m.D3, m.D4);
            //        List<uint> vertexIndices = new List<uint>();
            //        List<float> weights = new List<float>();
            //        foreach (VertexWeight weight in bone.VertexWeights)
            //        {
            //            vertexIndices.Add((uint)weight.VertexID);
            //            weights.Add(weight.Weight);
            //        }
            //        bones.Add(new Graphics.Bone(bone.Name, offset, vertexIndices.ToArray(), weights.ToArray()));
            //    }
            //}

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



            //uint[,] boneInd = new uint[vertices.Count / 3, 4];
            //int[] currentVectorIndexForBoneIndex = new int[8192];

            //float[,] boneWei = new float[vertices.Count / 3, 4];
            //int[] currentBoneWeightIndex = new int[8192];

            //for (int i = 0; i < bones.Count; i++)
            //{
            //    for (int x = 0; x < bones[i].Vertices.Length; x++)
            //    {
            //        boneInd[bones[i].Vertices[x], currentVectorIndexForBoneIndex[bones[i].Vertices[x]]] = (uint)i;
            //        boneWei[bones[i].Vertices[x], currentBoneWeightIndex[bones[i].Vertices[x]]] = bones[i].Weights[x];

            //        if (currentVectorIndexForBoneIndex[bones[i].Vertices[x]] >= 3)
            //        {
            //            currentVectorIndexForBoneIndex[bones[i].Vertices[x]] = 0;
            //        }
            //        currentVectorIndexForBoneIndex[bones[i].Vertices[x]]++;

            //        if (currentBoneWeightIndex[bones[i].Vertices[x]] >= 3)
            //        {
            //            currentBoneWeightIndex[bones[i].Vertices[x]] = 0;
            //        }
            //        currentBoneWeightIndex[bones[i].Vertices[x]]++;

            //    }
            //}


            //List<uint> ind = boneInd.Cast<uint>().ToList();
            //List<float> wei = boneWei.Cast<float>().ToList();
            #endregion

            Dictionary<string, Graphics.Bone> bonedict = new Dictionary<string, Graphics.Bone>();
            foreach (Graphics.Bone bone in bones)
            {
                bonedict.Add(bone.Name, bone);
            }

             // BLOC
            Node n = scene.RootNode.Children[0].Children[0];
            bonedict[n.Name].Parent = null;
            List<Graphics.Bone> c = new List<Graphics.Bone>();
            foreach (Node child in n.Children)
            {
                c.Add(bonedict[child.Name]);
            }
            bonedict[n.Name].Children = c.ToArray();
            //foreach (Node node in scene.RootNode.Children[0].Children[0].Children)
            //{
            //    bonedict[node.Name].Parent = bonedict[node.Parent.Name];
            //    List<Graphics.Bone> children = new List<Graphics.Bone>();
            //    foreach (Node child in node.Children)
            //    {
            //        children.Add(bonedict[child.Name]);
            //    }
            //    bonedict[node.Name].Children = children.ToArray();
            //}
            bonedict = trav(bonedict, scene.RootNode.Children[0].Children[0].Children[0]);

            // BLOCK

            Graphics.Mesh mesh = new Graphics.Mesh();
            mesh.Vertices = vertices.ToArray();
            mesh.TextureCoordinates = textureCoordinates.ToArray();
            mesh.Normals = normals.ToArray();
            mesh.Bones = bonedict;
            mesh.Indices = indices.ToArray();
            mesh.BoneIndices = boneVertices.Cast<uint>().ToArray();
            mesh.BoneWeights = boneWeights.Cast<float>().ToArray();

            mesh.Animations = animations.ToArray();

            //mesh.BoneIndices = ind.ToArray();
            //mesh.BoneWeights = wei.ToArray();
            //mesh.BoneIndices = ind.ToArray();
            //mesh.BoneWeights = wei.ToArray();

            return mesh;
        }
        //public Dictionary<string, Graphics.Bone> traverse(Dictionary<string, Graphics.Bone> bonedict, Scene scene)
        //{
        //    foreach (Node node in scene.RootNode.Children[0].Children[0].Children)
        //    {
        //        bonedict[node.Name].Parent = bonedict[node.Parent.Name];
        //        List<Graphics.Bone> children = new List<Graphics.Bone>();
        //        foreach (Node child in node.Children)
        //        {
        //            children.Add(bonedict[child.Name]);
        //        }
        //        bonedict[node.Name].Children = children.ToArray();
        //        foreach (Node n in node.Children)
        //        {
        //            traverse(
        //        }
        //    }
        //    return bonedict;
        //}
        public Dictionary<string, Graphics.Bone> trav(Dictionary<string, Graphics.Bone> bonedict, Node node)
        {
            if (bonedict.ContainsKey(node.Name) && bonedict.ContainsKey(node.Parent.Name))
            {
                bonedict[node.Name].Parent = bonedict[node.Parent.Name];
            }
            List<Graphics.Bone> children = new List<Graphics.Bone>();
            foreach (Node child in node.Children)
            {
                if (bonedict.ContainsKey(child.Name))
                {
                    children.Add(bonedict[child.Name]);
                }
            }
            if (bonedict.ContainsKey(node.Name))
            {
                bonedict[node.Name].Children = children.ToArray();
            }

            foreach (Node n in node.Children)
            {
                trav(bonedict, n);
            }
            return bonedict;
        }
    }
}
