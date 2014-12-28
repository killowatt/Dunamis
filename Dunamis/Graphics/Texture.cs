using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Texture
    {
        internal int TextureID;

        byte[] pixels;
        int width;
        int height;
        PixelFormat pixelFormat;
        TextureFilter textureFilter;
        bool mipmappingEnabled;

        #region Properties
        public byte[] Pixels
        {
            get
            {
                return pixels;
            }
        }
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
        public PixelFormat PixelFormat
        {
            get
            {
                return pixelFormat;
            }
        }
        public TextureFilter TextureFilter
        {
            get
            {
                return textureFilter;
            }
        }
        public bool MipmappingEnabled
        {
            get
            {
                return mipmappingEnabled;
            }
        }
        #endregion

        #region Methods
        public void SetTexture(byte[] pixels, int width, int height, PixelFormat format)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            PixelInternalFormat internalPixelFormat = new PixelInternalFormat();
            OpenTK.Graphics.OpenGL.PixelFormat pixelFormat = new OpenTK.Graphics.OpenGL.PixelFormat();
            if (format == PixelFormat.RGB)
            {
                internalPixelFormat = PixelInternalFormat.Rgb;
                pixelFormat = OpenTK.Graphics.OpenGL.PixelFormat.Rgb;
            }
            else if (format == PixelFormat.RGBA)
            {
                internalPixelFormat = PixelInternalFormat.Rgba;
                pixelFormat = OpenTK.Graphics.OpenGL.PixelFormat.Rgba;
            }

            GL.TexImage2D(TextureTarget.Texture2D, 0, internalPixelFormat, width, height, 0, pixelFormat, PixelType.UnsignedByte, pixels);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            this.width = width;
            this.height = height;
        }
        public void SetParameters(TextureFilter textureFilter, bool mipmappingEnabled)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            TextureMagFilter MagFilter = new TextureMagFilter();
            TextureMinFilter MinFilter = new TextureMinFilter();
            if (textureFilter == TextureFilter.Nearest)
            {
                MagFilter = TextureMagFilter.Nearest;
                if (mipmappingEnabled)
                {
                    MinFilter = TextureMinFilter.NearestMipmapLinear;
                }
                else
                {
                    MinFilter = TextureMinFilter.Nearest;
                }
            }
            else if (textureFilter >= TextureFilter.Linear)
            {
                MagFilter = TextureMagFilter.Linear;
                if (mipmappingEnabled)
                {
                    MinFilter = TextureMinFilter.LinearMipmapLinear;
                }
                else
                {
                    MinFilter = TextureMinFilter.Linear;
                }
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)MagFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)MinFilter);

            if (textureFilter >= TextureFilter.Anisotropic2x)
            {
                float maxAnisotropy;
                GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAnisotropy);
                GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, 4.0f);
            }
            if (mipmappingEnabled)
            {
                GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
            }

            GL.BindTexture(TextureTarget.Texture2D, 0);

            this.textureFilter = textureFilter;
            this.mipmappingEnabled = mipmappingEnabled;
        }
        #endregion

        public Texture()
        {
            GL.GenTextures(1, out TextureID);
        }
        public Texture(byte[] pixels, int width, int height, TextureFilter textureFilter, bool mipmappingEnabled, PixelFormat format)
            : this()
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, pixels);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
        }
    }
}
