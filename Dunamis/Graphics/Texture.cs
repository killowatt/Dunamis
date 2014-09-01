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
            internal set
            {
                width = value;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
            internal set
            {
                height = value;
            }
        }

        // TODO: get pixels

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
        public Texture(int width, int height)
        {
            GL.GenTextures(1, out TextureID);
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            this.width = width;
            this.height = height;

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear); // TODO: wouldn't this be nearest? (for framebuffer)
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
        internal Texture()
        {
            GL.GenTextures(1, out TextureID);
        }
    }
}
