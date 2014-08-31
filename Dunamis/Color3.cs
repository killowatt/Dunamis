using System;

namespace Dunamis
{
    // TODO: convert to class; include fancy conversions (hue to color, color from image?)
    public struct Color3
    {
        public byte R;
        public byte G;
        public byte B;

        public Color3(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
        public Color3(float r, float g, float b)
        {
            R = (byte)(r * 255);
            G = (byte)(g * 255);
            B = (byte)(b * 255);
        }
        public Color3(byte shade)
            : this(shade, shade, shade)
        {
        }
    }
}
