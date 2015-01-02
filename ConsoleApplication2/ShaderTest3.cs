using System;
using System.IO;
using Dunamis.Graphics;

namespace ConsoleApplication2
{
    public class ShaderTest3 : Shader
    {

        public override void Initialize()
        {
            addParameter("Model", Model, false);
        }
        public override void Update()
        {
            updateParameter("Model", Model, false);
        }

        public ShaderTest3()
            : base(File.ReadAllText("newvert.txt"), File.ReadAllText("frag.txt"), ShaderState.Static)
        {
        }
    }
}
