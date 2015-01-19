using Dunamis.Graphics;

namespace Dunamis.Common.Shaders
{
    public class SpriteShader : Shader
    {
        public Texture Texture;

        public override void Initialize()
        {
            AddTexture("Texture", Texture);
            AddParameter("Projection", Projection, false);
        }
        public override void Update()
        {
            UpdateTexture("Texture", Texture);
            UpdateParameter("Projection", Projection, false);
        }
        public SpriteShader() 
            : base(Utility.GetSource("Dunamis.Common.Shaders.Sources.SpriteVertex.txt"), Utility.GetSource("Dunamis.Common.Shaders.Sources.SpriteFragment.txt"), ShaderState.Dynamic)
        {
        }
    }
}
