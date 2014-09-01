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

        static string GetSource(ShaderType type)
        {
            string file = "";
            if (type == ShaderType.Vertex)
            {
                file = "Dunamis.Common.Shaders.Sources.RenderTextureVertex.txt";
            }
            if (type == ShaderType.Fragment)
            {
                file = "Dunamis.Common.Shaders.Sources.RenderTextureFragment.txt";
            }

            Assembly assembly = Assembly.GetExecutingAssembly();
            StreamReader streamReader = new StreamReader(assembly.GetManifestResourceStream(file));

            return streamReader.ReadToEnd();
        }
        public RenderTextureShader()
            : base(GetSource(ShaderType.Vertex), GetSource(ShaderType.Fragment), ShaderState.Dynamic)
        {
            Texture = null;
        }
    }
}
