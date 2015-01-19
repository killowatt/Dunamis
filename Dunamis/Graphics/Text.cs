using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Text
    {
        internal Sprite Sprite;
        Texture texture;
        Font font;
        string text;
        bool antiAlias;

        public string String
        {
            get { return text; }
            set
            {
                text = value;

                Bitmap bitmap = new Bitmap(1, 1);
                System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                SizeF size = graphics.MeasureString(text, font);

                bitmap = new Bitmap((int)size.Width, (int)size.Height);
                graphics = System.Drawing.Graphics.FromImage(bitmap);

                if (antiAlias)
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                }
                else
                {
                    graphics.SmoothingMode = SmoothingMode.None;
                }
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawString(text, font, Brushes.White, 0, 0);

                graphics.Flush();
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

                texture = new Texture(pixels, bitmap.Width, bitmap.Height, PixelFormat.Rgba, TextureFilter.Nearest);
                Sprite = new Sprite(bitmap.Width, bitmap.Height, texture);
            }
        }

        public Text()
        {
            antiAlias = true;
            font = SystemFonts.DefaultFont;
            text = "HELLO WORLD";
        }
    }
}
