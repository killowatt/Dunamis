using System.IO;
using Dunamis.Graphics;

namespace ConsoleApplication2
{
    public class ShaderTest3 : Shader
    {

        public override void Initialize()
        {
            AddParameter("Model", Model, false);
            AddParameter("View", View, false);
            AddParameter("Projection", Projection, false);
        }
        public override void Update()
        {
            UpdateParameter("Model", Model, false);
            UpdateParameter("View", View, false);
            UpdateParameter("Projection", Projection, false);
        }

        public ShaderTest3()
            : base(File.ReadAllText("newvert.txt"), File.ReadAllText("frag.txt"), ShaderState.Dynamic)
        {
        }
        public ShaderTest3(int yes)
            : base(File.ReadAllText("newvert.txt"), File.ReadAllText("fragf.txt"), ShaderState.Dynamic)
        {
        }
    }
}
