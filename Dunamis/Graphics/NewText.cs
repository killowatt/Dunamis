using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;
using SharpFont;

namespace Dunamis.Graphics
{
    public class NewText
    {
        Sprite sprite;
        Texture texture;

        public Texture Texture
        {
            get { return texture; } // TODO: RETURN COPY
        }

        public static void Test()
        {
            Library library = new Library();
            Face face = new Face(library, "DINRg.ttf");
            face.SetCharSize(0, 62, 0, 96);

            string text = "DUNAMIS";

            float penX = 0, penY = 0;
            float width = 0;
            float height = 0;

            //measure the size of the string before rendering it, requirement of Bitmap.
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                uint glyphIndex = face.GetCharIndex(c);
                face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

                width += (float)face.Glyph.Advance.X;

                if (face.HasKerning && i < text.Length - 1)
                {
                    char cNext = text[i + 1];
                    width += (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
                }

                if ((float)face.Glyph.Metrics.Height > height)
                    height = (float)face.Glyph.Metrics.Height;
            }

            //create a new bitmap that fits the string.
            Bitmap bmp = new Bitmap((int)Math.Ceiling(width), (int)Math.Ceiling(height));
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            g.Clear(Color.Transparent);

            //draw the string
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                uint glyphIndex = face.GetCharIndex(c);
                face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
                face.Glyph.RenderGlyph(RenderMode.Normal);

                if (c == ' ')
                {
                    penX += (float)face.Glyph.Advance.X;

                    if (face.HasKerning && i < text.Length - 1)
                    {
                        char cNext = text[i + 1];
                        width += (float)face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default).X;
                    }

                    penY += (float)face.Glyph.Advance.Y;
                    continue;
                }

                Bitmap cBmp = face.Glyph.Bitmap.ToGdipBitmap(Color.White);

                //Not using g.DrawImage because some characters come out blurry/clipped.
                //g.DrawImage(cBmp, penX + face.Glyph.BitmapLeft, penY + (bmp.Height - face.Glyph.Bitmap.Rows));
                g.DrawImageUnscaled(cBmp, (int)Math.Round(penX + face.Glyph.BitmapLeft), (int)Math.Round(penY + (bmp.Height - face.Glyph.BitmapTop)));

                penX += (float)face.Glyph.Metrics.HorizontalAdvance;
                penY += (float)face.Glyph.Advance.Y;

                if (face.HasKerning && i < text.Length - 1)
                {
                    char cNext = text[i + 1];
                    var kern = face.GetKerning(glyphIndex, face.GetCharIndex(cNext), KerningMode.Default);
                    penX += (float)kern.X;
                }
            }

            g.Dispose();
            
            //xxx = new Sprite((int)Math.Ceiling(width), (int)Math.Ceiling(height), 10, 10, new Texture(bmp, TextureFilter.Nearest, false));
        }
    }
}
