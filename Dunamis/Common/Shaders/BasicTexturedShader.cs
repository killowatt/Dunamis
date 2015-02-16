using Dunamis.Graphics;

namespace Dunamis.Common.Shaders
{
    public class BasicTexturedShader : Shader
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

        public BasicTexturedShader(Texture texture)
            : base(Utility.GetSource("Dunamis.Common.Shaders.Sources.BasicTexturedVertex.txt"), Utility.GetSource("Dunamis.Common.Shaders.Sources.BasicTexturedFragment.txt"), ShaderState.Dynamic)
        {
            this.Texture = texture;
        }
        public BasicTexturedShader()
            : this(new Texture(new byte[] { 255, 255, 255, 255, 0, 0, 0, 255, 0, 0, 0, 255, 255, 255, 255, 255 }, 2, 2, PixelFormat.Rgba, TextureFilter.Nearest, false)) // TODO: USE BUILT IN DEFAULT
        {
        }
    }
}
