using System;
using System.IO;
using System.Reflection;
using Dunamis.Graphics;

namespace Dunamis.Common.Shaders
{
    public class RenderTextureShader : Shader
    {
        public Texture Texture;

        public override void Initialize()
        {
            addTexture("Texture", Texture);
        }
        public override void Update()
        {
            updateTexture("Texture", Texture);
        }

        public RenderTextureShader()
            : base(Utility.GetSource("Dunamis.Common.Shaders.Sources.RenderTextureVertex.txt"), Utility.GetSource("Dunamis.Common.Shaders.Sources.RenderTextureFragment.txt"), ShaderState.Dynamic)
        {
            Texture = null;
        }
    }
}
