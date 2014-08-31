using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Texture
    {
        internal int TextureID;

        int width;
        int height;

        public int Width
        {
            get
            {
                return width;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
        }

        public Texture(byte[] pixels, int width, int height) // TODO: add desc; format RGBA in UNSIGNED BYTES
        {
            GL.GenTextures(1, out TextureID);
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            this.width = width;
            this.height = height;
            // TODO: if length of dimension % 4 != 0 then error wait maybe not

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixels); 

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest); // def linear
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest); // def linear
            //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D); // TODO: see how this works

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }
}
