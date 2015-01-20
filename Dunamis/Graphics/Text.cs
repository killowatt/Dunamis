using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class Text // TODO: make rlly efficient??
    {
        Sprite sprite;
        Texture texture;
        Color color;
        Font font;
        float fontSize;
        string text;
        bool bold;
        bool italic;
        bool underline;
        bool antiAliasingEnabled;
        int x;
        int y;

        bool rendered;

        public Texture Texture
        {
            get { return texture; } // TODO: return COPY, aka new tex
        }
        public bool AntiAliasingEnabled
        {
            get { return antiAliasingEnabled; }
            set
            {
                antiAliasingEnabled = value;
                rendered = false;
            }
        }
        public Color4 Color
        {
            get { return new Color4(color.R, color.G, color.B, color.A); }
            set 
            { 
                color = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
                rendered = false;
            }
        }
        public Font Font
        {
            get { return font; }
            set
            {
                font = value;
                rendered = false;
            }
        }
        public float FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                rendered = false;
            }
        }
        public string String
        {
            get { return text; }
            set
            {
                text = value;
                rendered = false;
            }
        }
        public bool Bold
        {
            get { return bold; }
            set
            {
                bold = value;
                rendered = false;
            }
        }
        public bool Italic
        {
            get { return italic; }
            set
            {
                italic = value;
                rendered = false;
            }
        }
        public bool Underline
        {
            get { return italic; }
            set
            {
                italic = value;
                rendered = false;
            }
        }
        public int X
        {
            get { return sprite.X; }
            set
            {
                x = value;
                rendered = false; // TODO: don't do this, just set the sprite. temp fix.
            }
        }
        public int Y
        {
            get { return sprite.Y; }
            set
            {
                y = value;
                rendered = false;
            }
        }
        public Sprite Sprite // TODO: holy shit for everything like this maybe return copies so people can't fuck shit up?
        {
            get
            {
                if (!rendered)
                {
                    // Create the drawn font from our given settings.
                    FontStyle style = FontStyle.Regular;
                    if (bold)
                    {
                        style |= FontStyle.Bold;
                    }
                    if (italic)
                    {
                        style |= FontStyle.Italic;
                    }
                    if (underline)
                    {
                        style |= FontStyle.Underline;
                    }
                    System.Drawing.Font drawnFont = new System.Drawing.Font(font.Family, fontSize, style);

                    // Measure the font so that we can make room for it in an image.
                    Bitmap bitmap = new Bitmap(1, 1);
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                    SizeF size = graphics.MeasureString(text, drawnFont);

                    if (size.Width <= 0) // TODO: handle this better
                    {
                        size.Width += 1;
                    }
                    if (size.Height <= 0)
                    {
                        size.Height += 1;
                    }

                    // Prepare to render to our image.
                    bitmap = new Bitmap((int)size.Width, (int)size.Height);
                    graphics = System.Drawing.Graphics.FromImage(bitmap);

                    // Set parameters and draw our image.
                    if (antiAliasingEnabled)
                    {
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    }
                    else
                    {
                        graphics.SmoothingMode = SmoothingMode.None;
                    }
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawString(text, drawnFont, new SolidBrush(color), 0, 0);
                    graphics.Flush();

                    // Turn our image into raw data.
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
                    sprite = new Sprite(bitmap.Width, bitmap.Height, x, y, texture);

                    rendered = true;
                }
                return sprite;
            }
        }
        #region old
        //public Sprite Spritess // TOOD: holy shit for everything like this maybe return copies so people can't fuck shit up?
        //{
        //    get
        //    {
        //        if (!rendered)
        //        {
        //            Bitmap bitmap = new Bitmap(1, 1);
        //            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
        //            SizeF size = graphics.MeasureString(text, font);

        //            bitmap = new Bitmap((int)size.Width, (int)size.Height);
        //            graphics = System.Drawing.Graphics.FromImage(bitmap);

        //            if (antiAliasingEnabled)
        //            {
        //                graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //            }
        //            else
        //            {
        //                graphics.SmoothingMode = SmoothingMode.None;
        //            }
        //            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //            graphics.DrawString(text, font, color);

        //            graphics.Flush();
        //            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

        //            int length = data.Stride * data.Height;
        //            byte[] image = new byte[length];

        //            Marshal.Copy(data.Scan0, image, 0, length);
        //            bitmap.UnlockBits(data);

        //            byte[] pixels = new byte[length];
        //            for (int index = 0; index <= image.Length - 4; index += 4)
        //            {
        //                byte R = image[index + 2];
        //                byte G = image[index + 1];
        //                byte B = image[index];
        //                byte A = image[index + 3];

        //                pixels[index] = R;
        //                pixels[index + 1] = G;
        //                pixels[index + 2] = B;
        //                pixels[index + 3] = A;
        //            }

        //            texture = new Texture(pixels, bitmap.Width, bitmap.Height, PixelFormat.Rgba, TextureFilter.Nearest);
        //            sprite = new Sprite(bitmap.Width, bitmap.Height, texture);

        //            rendered = true;
        //        }
        //        return sprite;
        //    }
        //}
        #endregion

        public Text(string text, Font font, float fontSize, bool bold, bool italic, bool underline, Color4 color, bool antiAliasingEnabled, int x, int y) // TODO: add more constructors
        { // TODO: clean up constructor from ugliness
            String = text;
            Font = font;
            FontSize = fontSize;
            Bold = bold;
            Italic = italic;
            Underline = underline;
            Color = color;
            AntiAliasingEnabled = antiAliasingEnabled;
            X = x;
            Y = y;
        }
        public Text(string text) 
            : this(text, Graphics.Font.Default, 8.0f, false, false, false, Color4.Black, true, 0, 0)
        {
        }
        public Text() 
            : this("")
        {
        }
    }
}
