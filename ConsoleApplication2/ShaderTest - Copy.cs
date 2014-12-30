using System;
using System.IO;
using Dunamis.Graphics;

namespace ConsoleApplication2
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
            : base(File.ReadAllText("vert.txt"), File.ReadAllText("frag2.txt"), ShaderState.Static)
        {
        }
    }
}
