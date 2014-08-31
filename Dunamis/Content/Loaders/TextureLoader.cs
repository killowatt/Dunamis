using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Dunamis.Graphics;

namespace Dunamis.Content.Loaders
{
    public class TextureLoader : ILoader<Texture>
    {
        public Texture Load(string filename)
        {
            Bitmap bitmap = new Bitmap(filename);
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

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

            return new Texture(pixels, bitmap.Width, bitmap.Height);
        }
    }
}
