using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL;
using SharpFont;

namespace Dunamis.Graphics
{
    public class Text : IDrawable // TODO: Fix er up. 
    {
        Sprite sprite;
        Texture texture;

        bool rendered;

        string text;
        Font font;
        Color4 color;
        uint size;

        public Sprite Sprite
        {
            get
            {
                if (!rendered)
                {
                    font.Face.SetCharSize(0, 62, 0, size);
                    float penX = 0, penY = 0, width = 0, height = 0;

                    for (int currentCharacter = 0; currentCharacter < text.Length; currentCharacter++)
                    {
                        char character = text[currentCharacter];

                        uint glyphIndex = font.Face.GetCharIndex(character);
                        font.Face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);

                        width += (float)font.Face.Glyph.Advance.X;

                        if (font.Face.HasKerning && currentCharacter < text.Length - 1)
                        {
                            char nextCharacter = text[currentCharacter + 1];
                            width += (float)font.Face.GetKerning(glyphIndex, font.Face.GetCharIndex(nextCharacter), KerningMode.Default).X;
                        }

                        if ((float)font.Face.Glyph.Metrics.Height > height)
                            height = (float)font.Face.Glyph.Metrics.Height;
                    }

                    Bitmap bitmap = new Bitmap((int)Math.Ceiling(width), (int)Math.Ceiling(height));
                    System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
                    graphics.Clear(System.Drawing.Color.Transparent);

                    //draw the string
                    for (int currentCharacter = 0; currentCharacter < text.Length; currentCharacter++)
                    {
                        char character = text[currentCharacter];

                        uint glyphIndex = font.Face.GetCharIndex(character);
                        font.Face.LoadGlyph(glyphIndex, LoadFlags.Default, LoadTarget.Normal);
                        font.Face.Glyph.RenderGlyph(RenderMode.Normal);

                        if (character == ' ')
                        {
                            penX += (float)font.Face.Glyph.Advance.X;

                            if (font.Face.HasKerning && currentCharacter < text.Length - 1)
                            {
                                char cNext = text[currentCharacter + 1];
                                width += (float)font.Face.GetKerning(glyphIndex, font.Face.GetCharIndex(cNext), KerningMode.Default).X;
                            }

                            penY += (float)font.Face.Glyph.Advance.Y;
                            continue;
                        }

                        Bitmap cBmp = font.Face.Glyph.Bitmap.ToGdipBitmap(System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));

                        //Not using g.DrawImage because some characters come out blurry/clipped.
                        //g.DrawImage(cBmp, penX + font.Face.Glyph.BitmapLeft, penY + (bmp.Height - font.Face.Glyph.Bitmap.Rows));
                        graphics.DrawImageUnscaled(cBmp, (int)Math.Round(penX + font.Face.Glyph.BitmapLeft), (int)Math.Round(penY + (bitmap.Height - font.Face.Glyph.BitmapTop)));

                        penX += (float)font.Face.Glyph.Metrics.HorizontalAdvance;
                        penY += (float)font.Face.Glyph.Advance.Y;

                        if (font.Face.HasKerning && currentCharacter < text.Length - 1)
                        {
                            char cNext = text[currentCharacter + 1];
                            var kern = font.Face.GetKerning(glyphIndex, font.Face.GetCharIndex(cNext), KerningMode.Default);
                            penX += (float)kern.X;
                        }
                    }

                    graphics.Dispose();
                    texture = new Texture(bitmap, TextureFilter.Nearest, false);
                    sprite = new Sprite(texture.Width, texture.Height, texture); // TODO: make a constructor that looks at the textures width/height automatically
                    rendered = true;
                }
                return sprite;
            }
        }
        public string String
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;
                    rendered = false;
                }
            }
        }
        public Font Font
        {
            get
            {
                return font;
            }
            set
            {
                if (font != value)
                {
                    font = value;
                    rendered = false;
                }
            }
        }
        public uint Size
        {
            get
            {
                return size;
            }
            set
            {
                if (size != value)
                {
                    size = value;
                    rendered = false;
                }
            }
        }
        public Color4 Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color != value)
                {
                    color = value;
                    rendered = false;
                }
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.Draw(Sprite);
        }

        public Text(string text, Font font)
        {
            rendered = false;
            String = text;
            Font = font;
        }
    }
}
