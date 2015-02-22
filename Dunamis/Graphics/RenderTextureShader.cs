namespace Dunamis.Graphics
{
    public abstract class RenderTextureShader : Shader
    {
        public Texture Texture; // TODO: TEXTURE CLASS: color paramter; makes texture a single uniform color of 1x1

        public override void Initialize()
        {
            AddTexture("Texture", Texture);
        }
        public override void Update()
        {
            UpdateTexture("Texture", Texture);
        }
        public RenderTextureShader(string fragmentSource)
            : base(
                Common.Shaders.Utility.GetSource("Dunamis.Common.Shaders.Sources.RenderTextureVertex.txt"), // TODO: maybe we shouldn't use constructors for file loading, but rather have their own method (i.e. static load)
                fragmentSource, ShaderState.Dynamic)
        { 
            Texture = null;
        }
    }
}
