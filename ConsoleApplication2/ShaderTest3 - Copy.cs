using System.IO;
using Dunamis.Graphics;

namespace ConsoleApplication2
{
    public class ShaderTest4 : Shader
    {
        public Texture Texture;

        public override void Initialize()
        {
            AddParameter("Model", Model, false);
            AddParameter("View", View, false);
            AddParameter("Projection", Projection, false);
            AddTexture("tex", Texture);
        }
        public override void Update()
        {
            UpdateParameter("Model", Model, false);
            UpdateParameter("View", View, false);
            UpdateParameter("Projection", Projection, false);
            UpdateTexture("tex", Texture);
        }

        public ShaderTest4()
            : base(File.ReadAllText("newvert.txt"), File.ReadAllText("fragf2.txt"), ShaderState.Dynamic)
        {
        }
    }
}
