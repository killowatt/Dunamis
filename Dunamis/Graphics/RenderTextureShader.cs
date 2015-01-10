namespace Dunamis.Graphics
{
    public abstract class RenderTextureShader : Shader
    {
        public Texture Texture; // TODO: implement solid black/white textures as static textures in tex class

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
                Common.Shaders.Utility.GetSource("Dunamis.Common.Shaders.Sources.RenderTextureVertex.txt"),
                fragmentSource, ShaderState.Dynamic)
        {
            Texture = null;
        }
    }
}
