namespace Dunamis
{
    public struct Color4
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        #region Methods
        public bool Equals(Color4 other)
        {
            return A == other.A && B == other.B && G == other.G && R == other.R;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Color4 && Equals((Color4) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = A.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ R.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        #region Operators
        // Math
        // TODO: these

        // Equality
        public static bool operator ==(Color4 left, Color4 right)
        {
            return left.R == right.R && left.G == right.G && left.B == right.B && left.A == right.A;
        }
        public static bool operator !=(Color4 left, Color4 right)
        {
            return !(left == right);
        }

        public static implicit operator Color4(Color3 color)
        {
            return new Color4(color.R, color.G, color.B, 255);
        }
        public static implicit operator Color4(OpenTK.Graphics.Color4 color)
        {
            return new Color4(color.R, color.G, color.B, color.A);
        }
        public static implicit operator OpenTK.Graphics.Color4(Color4 color)
        {
            return new OpenTK.Graphics.Color4(color.R, color.G, color.B, color.A);
        }
        #endregion

        #region Colors
        public static Color4 Black { get { return new Color3(0, 0, 0); } }
        public static Color4 Gray { get { return new Color3(128, 128, 128); } }
        public static Color4 Red { get { return new Color3(255, 0, 0); } }
        public static Color4 Orange { get { return new Color3(255, 106, 0); } }
        public static Color4 Yellow { get { return new Color3(255, 216, 0); } }
        public static Color4 Green { get { return new Color3(0, 255, 33); } }
        public static Color4 Cyan { get { return new Color3(0, 255, 255); } }
        public static Color4 Blue { get { return new Color3(0, 38, 255); } }
        public static Color4 Purple { get { return new Color3(178, 0, 255); } }
        public static Color4 Pink { get { return new Color3(255, 0, 110); } }
        public static Color4 White { get { return new Color3(255, 255, 255); } }
        #endregion

        #region Constructors
        public Color4(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        public Color4(float r, float g, float b, float a)
        {
            R = (byte)(r * 255);
            G = (byte)(g * 255);
            B = (byte)(b * 255);
            A = (byte)(a * 255);
        }
        public Color4(byte shade)
            : this(shade, shade, shade, 255)
        {
        }
        #endregion
    }
}
