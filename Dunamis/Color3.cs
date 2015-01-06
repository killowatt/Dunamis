namespace Dunamis
{
    public struct Color3
    {
        public byte R;
        public byte G;
        public byte B;

        #region Methods
        public bool Equals(Color3 other)
        {
            return R == other.R && G == other.G && B == other.B;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Color3 && Equals((Color3)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                return hashCode;
            }
        }
        #endregion

        #region Operators
        // Math
        // TODO: these

        // Equality
        public static bool operator ==(Color3 left, Color3 right)
        {
            return left.R == right.R && left.G == right.G && left.B == right.B;
        }

        public static bool operator !=(Color3 left, Color3 right)
        {
            return !(left == right);
        }

        // Conversion
        public static explicit operator Color3(Color4 color)
        {
            return new Color3(color.R, color.G, color.B);
        }
        public static implicit operator Color3(Vector3 vector)
        {
            return new Color3(vector.X, vector.Y, vector.Z);
        }
        #endregion

        #region Colors
        public static Color3 Black { get { return new Color3(0, 0, 0); } }
        public static Color3 Gray { get { return new Color3(128, 128, 128); } }
        public static Color3 Red { get { return new Color3(255, 0, 0); } }
        public static Color3 Orange { get { return new Color3(255, 106, 0); } }
        public static Color3 Yellow { get { return new Color3(255, 216, 0); } }
        public static Color3 Green { get { return new Color3(0, 255, 33); } }
        public static Color3 Cyan { get { return new Color3(0, 255, 255); } }
        public static Color3 Blue { get { return new Color3(0, 38, 255); } }
        public static Color3 Purple { get { return new Color3(178, 0, 255); } }
        public static Color3 Pink { get { return new Color3(255, 0, 110); } }
        public static Color3 White { get { return new Color3(255, 255, 255); } }
        #endregion

        #region Constructors
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
        #endregion
    }
}
