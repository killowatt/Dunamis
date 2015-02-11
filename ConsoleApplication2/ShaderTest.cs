﻿using System;
using System.IO;
using Dunamis.Graphics;

namespace DunamisExamples
{
    public class ShaderTest : Shader
    {
        public override void Initialize()
        {
            Console.WriteLine("hey!");
        }
        public override void Update()
        {
        }

        public ShaderTest()
            : base(File.ReadAllText("Resources/vert.txt"), File.ReadAllText("Resources/frag.txt"), ShaderState.Static)
        {
        }
    }
}
