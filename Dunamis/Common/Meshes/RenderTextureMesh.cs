﻿using System;
using Dunamis.Common.Shaders;
using Dunamis.Graphics;

namespace Dunamis.Common.Meshes
{
    public class RenderTextureMesh : Mesh
    {
        public static new float[] Vertices = new float[]
            {
                -1, -1, 0,
                1, -1, 0,
                1, 1, 0,
                -1, 1, 0,
            };
        public static new float[] TextureCoordinates = new float[]
            {
                0, 0,
                1, 0,
                1, 1,
                0, 1
            };
        public static new uint[] Indices = new uint[]
            {
                0, 1, 2,
                2, 3, 0
            };
        public RenderTextureMesh(RenderTextureShader shader)
            : base(Vertices, TextureCoordinates, new float[] { }, Indices, MeshType.Static, shader)
        {
        }
    }
}