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
        public void SetTexture(byte[] pixels, int width, int height, PixelFormat pixelFormat)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureID);

            PixelInternalFormat internalFormat = new PixelInternalFormat();
            OpenTK.Graphics.OpenGL.PixelFormat format = new OpenTK.Graphics.OpenGL.PixelFormat();
            if (pixelFormat == PixelFormat.RGB)
            {
                internalFormat = PixelInternalFormat.Rgb;
                format = OpenTK.Graphics.OpenGL.PixelFormat.Rgb;
            }
            else if (pixelFormat == PixelFormat.RGBA)
            {
                internalFormat = PixelInternalFormat.Rgba;
                format = OpenTK.Graphics.OpenGL.PixelFormat.Rgba;
            }

            GL.TexImage2D(TextureTarget.Texture2D, 0, internalFormat, width, height, 0, format, PixelType.UnsignedByte, pixels);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            this.pixels = pixels;
            this.width = width;
            this.height = height;
            this.pixelFormat = pixelFormat;
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
                //float maxAnisotropy;
                //GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAnisotropy);
                GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)ExtTextureFilterAnisotropic.TextureMaxAnisotropyExt, (float)textureFilter); // ERROR: if max anisotropy is surpassed, throw exception.
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

        #region Constructors
        public Texture()
        {
            GL.GenTextures(1, out TextureID);
        }
        public Texture(byte[] pixels, int width, int height, PixelFormat format, TextureFilter textureFilter, bool mipmappingEnabled)
            : this()
        {
            SetTexture(pixels, width, height, format);
            SetParameters(textureFilter, mipmappingEnabled);
        }
        public Texture(byte[] pixels, int width, int height, PixelFormat format, TextureFilter textureFilter)
            : this(pixels, width, height, format, textureFilter, true)
        {
        }
        public Texture(byte[] pixels, int width, int height, PixelFormat format)
            : this(pixels, width, height, format, TextureFilter.Linear)
        {
        }
        public Texture(int width, int height, PixelFormat pixelFormat)
            : this(null, width, height, pixelFormat)
        {
        }
        public Texture(int width, int height)
            : this(width, height, PixelFormat.RGB)
        {
        }
        #endregion
    }
}
