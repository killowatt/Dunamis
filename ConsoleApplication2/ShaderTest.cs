﻿using System;
using System.IO;
using Dunamis.Graphics;

namespace ConsoleApplication2
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
            : base(File.ReadAllText("vert.txt"), File.ReadAllText("frag.txt"), ShaderState.Static)
        {
        }
    }
}