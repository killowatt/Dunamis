using Dunamis.Graphics;

namespace Dunamis.Common.Shaders
{
    public class DefaultRenderTextureShader : RenderTextureShader
    {
        public DefaultRenderTextureShader()
            : base(Utility.GetSource("Dunamis.Common.Shaders.Sources.RenderTextureFragment.txt"))
        {
        }
    }
}
