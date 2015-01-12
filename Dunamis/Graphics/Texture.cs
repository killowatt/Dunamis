using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public partial class Texture
    {
        internal int TextureId;

        byte[] _pixels;
        int _width;
        int _height;
        PixelFormat _pixelFormat;
        TextureFilter _textureFilter;
        bool _mipmappingEnabled;

        #region Properties
        public byte[] Pixels
        {
            get
            {
                return _pixels; // TODO: provide a way to get this texture directly from OpenGL instead of managing it ourselves (glGetTexImage)
            }
        }
        public int Width
        {
            get
            {
                return _width;
            }
            internal set
            {
                _width = value;
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }
            internal set
            {
                _height = value;
            }
        }
        public PixelFormat PixelFormat
        {
            get
            {
                return _pixelFormat;
            }
            internal set
            {
                _pixelFormat = value;
            }
        }
        public TextureFilter TextureFilter
        {
            get
            {
                return _textureFilter;
            }
            internal set
            {
                _textureFilter = value;
            }
        }
        public bool MipmappingEnabled
        {
            get
            {
                return _mipmappingEnabled;
            }
            internal set
            {
                _mipmappingEnabled = value;
            }
        }
        #endregion

        #region Methods
        public void SetTexture(byte[] pixels, int width, int height, PixelFormat pixelFormat)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureId);

            PixelInternalFormat internalFormat = new PixelInternalFormat();
            OpenTK.Graphics.OpenGL.PixelFormat format = new OpenTK.Graphics.OpenGL.PixelFormat();
            if (pixelFormat == PixelFormat.Rgb)
            {
                internalFormat = PixelInternalFormat.Rgb;
                format = OpenTK.Graphics.OpenGL.PixelFormat.Rgb;
            }
            else if (pixelFormat == PixelFormat.Rgba)
            {
                internalFormat = PixelInternalFormat.Rgba;
                format = OpenTK.Graphics.OpenGL.PixelFormat.Rgba;
            }

            GL.TexImage2D(TextureTarget.Texture2D, 0, internalFormat, width, height, 0, format, PixelType.UnsignedByte, pixels);
            GL.BindTexture(TextureTarget.Texture2D, 0);

            _pixels = pixels;
            _width = width;
            _height = height;
            _pixelFormat = pixelFormat;
        }
        public void SetParameters(TextureFilter textureFilter, bool mipmappingEnabled)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureId);

            TextureMagFilter magFilter = new TextureMagFilter();
            TextureMinFilter minFilter = new TextureMinFilter();
            if (textureFilter == TextureFilter.Nearest)
            {
                magFilter = TextureMagFilter.Nearest;
                minFilter = mipmappingEnabled ? TextureMinFilter.NearestMipmapLinear : TextureMinFilter.Nearest;
            }
            else if (textureFilter >= TextureFilter.Linear)
            {
                magFilter = TextureMagFilter.Linear;
                minFilter = mipmappingEnabled ? TextureMinFilter.LinearMipmapLinear : TextureMinFilter.Linear;
            }
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minFilter);

            if (textureFilter >= TextureFilter.Anisotropic2X)
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

            _textureFilter = textureFilter;
            _mipmappingEnabled = mipmappingEnabled;
        }
        #endregion

        #region Constructors
        public Texture()
        {
            GL.GenTextures(1, out TextureId);
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
            : this(width, height, PixelFormat.Rgb)
        {
        }
        public Texture(string filename, TextureFilter textureFilter, bool mipmappingEnabled) : this() // TODO: fix textures loading upside down
        {
            Bitmap bitmap = new Bitmap(filename);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            int length = data.Stride * data.Height;
            byte[] image = new byte[length];

            Marshal.Copy(data.Scan0, image, 0, length);
            bitmap.UnlockBits(data);

            byte[] pixels = new byte[length];
            for (int index = 0; index <= image.Length - 4; index += 4)
            {
                byte R = image[index + 2];
                byte G = image[index + 1];
                byte B = image[index];
                byte A = image[index + 3];

                pixels[index] = R;
                pixels[index + 1] = G;
                pixels[index + 2] = B;
                pixels[index + 3] = A;
            }

            SetTexture(pixels, bitmap.Width, bitmap.Height, PixelFormat.Rgba);
            SetParameters(textureFilter, mipmappingEnabled);
        }
        ~Texture()
        {
            GL.DeleteTextures(1, ref TextureId);
        }
        #endregion
    }
}
