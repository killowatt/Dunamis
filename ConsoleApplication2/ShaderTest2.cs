using System;
using System.IO;
using Dunamis.Graphics;

namespace DunamisExamples
{
    public class ShaderTest2 : Shader
    {
        public override void Initialize()
        {
            Console.WriteLine("sup!");
        }
        public override void Update()
        {
        }

        public ShaderTest2()
            : base(File.ReadAllText("Resources/vert.txt"), File.ReadAllText("Resources/frag2.txt"), ShaderState.Static)
        {
        }
    }
}
