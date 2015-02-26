namespace Dunamis
{
    public struct Color4
    {
        /// <summary>
        /// The red component of this color.
        /// </summary>
        public byte R;
        /// <summary>
        /// The green component of this color.
        /// </summary>
        public byte G;
        /// <summary>
        /// The blue component of this color.
        /// </summary>
        public byte B;
        /// <summary>
        /// The alpha component of this color.
        /// </summary>
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
        /// <summary>
        /// A black color (0, 0, 0).
        /// </summary>
        public static Color4 Black { get { return Color3.Black; } }
        /// <summary>
        /// A gray color (128, 128, 128).
        /// </summary>
        public static Color4 Gray { get { return Color3.Gray; } }
        /// <summary>
        /// A grey color (128, 128, 128).
        /// </summary>
        public static Color4 Grey { get { return Color3.Grey; } }
        /// <summary>
        /// A red color (255, 0, 0).
        /// </summary>
        public static Color4 Red { get { return Color3.Red; } }
        /// <summary>
        /// An orange color (255, 106, 0).
        /// </summary>
        public static Color4 Orange { get { return Color3.Orange; } }
        /// <summary>
        /// A yellow color (255, 216, 0).
        /// </summary>
        public static Color4 Yellow { get { return Color3.Yellow; } }
        /// <summary>
        /// A green color (0, 255, 33).
        /// </summary>
        public static Color4 Green { get { return Color3.Green; } }
        /// <summary>
        /// A cyan color (0, 255, 255).
        /// </summary>
        public static Color4 Cyan { get { return Color3.Cyan; } }
        /// <summary>
        /// A blue color (0, 38, 255).
        /// </summary>
        public static Color4 Blue { get { return Color3.Blue; } }
        /// <summary>
        /// A purple color (178, 0, 255).
        /// </summary>
        public static Color4 Purple { get { return Color3.Purple; } }
        /// <summary>
        /// A pink color (255, 0, 110).
        /// </summary>
        public static Color4 Pink { get { return Color3.Pink; } }
        /// <summary>
        /// A white color (255, 255, 255).
        /// </summary>
        public static Color4 White { get { return Color3.White; } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new color object with the specified values.
        /// </summary>
        /// <param name="r">The red component of this color.</param>
        /// <param name="g">The green component of this color.</param>
        /// <param name="b">The blue component of this color.</param>
        /// <param name="a">The alpha component of this color.></param>
        public Color4(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        /// <summary>
        /// Creates a new color object with the specified float values.
        /// </summary>
        /// <param name="r">The red component of this color.</param>
        /// <param name="g">The green component of this color.</param>
        /// <param name="b">The blue component of this color.</param>
        /// <param name="a">The alpha component of this color.></param>
        public Color4(float r, float g, float b, float a)
        {
            R = (byte)(r * 255);
            G = (byte)(g * 255);
            B = (byte)(b * 255);
            A = (byte)(a * 255);
        }
        /// <summary>
        /// Creates a new gray color of the specified shade.
        /// </summary>
        /// <param name="shade">The shade of gray to create.</param>
        public Color4(byte shade)
            : this(shade, shade, shade, 255)
        {
        }
        #endregion
    }
}
