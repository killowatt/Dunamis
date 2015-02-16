using Dunamis.Graphics;

namespace Dunamis.Common.Shaders
{
    public class BasicColorShader : Shader
    {
        public Color4 Color;

        public override void Initialize()
        {
            AddParameter("Model", Model, false);
            AddParameter("View", View, false);
            AddParameter("Projection", Projection, false);
            AddParameter("Color", Color);
        }
        public override void Update()
        {
            UpdateParameter("Model", Model, false);
            UpdateParameter("View", View, false);
            UpdateParameter("Projection", Projection, false);
            UpdateParameter("Color", Color);
        }

        public BasicColorShader(Color4 color)
            : base(Utility.GetSource("Dunamis.Common.Shaders.Sources.BasicColorVertex.txt"), Utility.GetSource("Dunamis.Common.Shaders.Sources.BasicColorFragment.txt"), ShaderState.Dynamic)
        {
            Color = color;
        }
        public BasicColorShader()
            : this(new Color4(224, 99, 71, 255))
        {
        }
    }
}
