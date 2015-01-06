using Dunamis.Graphics;

namespace Dunamis.Common.Shaders
{
    public class RenderTextureShader : Shader
    {
        public Texture Texture;

        public override void Initialize()
        {
            AddTexture("Texture", Texture);
        }
        public override void Update()
        {
            UpdateTexture("Texture", Texture);
        }

        public RenderTextureShader()
            : base(Utility.GetSource("Dunamis.Common.Shaders.Sources.RenderTextureVertex.txt"), Utility.GetSource("Dunamis.Common.Shaders.Sources.RenderTextureFragment.txt"), ShaderState.Dynamic)
        {
            Texture = null;
        }
    }
}
