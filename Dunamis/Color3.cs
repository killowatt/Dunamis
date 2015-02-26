namespace Dunamis
{
    public struct Color3
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
        /// <summary>
        /// A black color (0, 0, 0).
        /// </summary>
        public static Color3 Black { get { return new Color3(0, 0, 0); } }
        /// <summary>
        /// A gray color (128, 128, 128).
        /// </summary>
        public static Color3 Gray { get { return new Color3(128, 128, 128); } }
        /// <summary>
        /// A grey color (128, 128, 128).
        /// </summary>
        public static Color3 Grey { get { return Gray; } }
        /// <summary>
        /// A red color (255, 0, 0).
        /// </summary>
        public static Color3 Red { get { return new Color3(255, 0, 0); } }
        /// <summary>
        /// An orange color (255, 106, 0).
        /// </summary>
        public static Color3 Orange { get { return new Color3(255, 106, 0); } }
        /// <summary>
        /// A yellow color (255, 216, 0).
        /// </summary>
        public static Color3 Yellow { get { return new Color3(255, 216, 0); } }
        /// <summary>
        /// A green color (0, 255, 33).
        /// </summary>
        public static Color3 Green { get { return new Color3(0, 255, 33); } }
        /// <summary>
        /// A cyan color (0, 255, 255).
        /// </summary>
        public static Color3 Cyan { get { return new Color3(0, 255, 255); } }
        /// <summary>
        /// A blue color (0, 38, 255).
        /// </summary>
        public static Color3 Blue { get { return new Color3(0, 38, 255); } }
        /// <summary>
        /// A purple color (178, 0, 255).
        /// </summary>
        public static Color3 Purple { get { return new Color3(178, 0, 255); } }
        /// <summary>
        /// A pink color (255, 0, 110).
        /// </summary>
        public static Color3 Pink { get { return new Color3(255, 0, 110); } }
        /// <summary>
        /// A white color (255, 255, 255).
        /// </summary>
        public static Color3 White { get { return new Color3(255, 255, 255); } }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new color object with the specified values.
        /// </summary>
        /// <param name="r">The red component of this color.</param>
        /// <param name="g">The green component of this color.</param>
        /// <param name="b">The blue component of this color.</param>
        public Color3(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
        /// <summary>
        /// Creates a new color object with the specified float values.
        /// </summary>
        /// <param name="r">The red component of this color.</param>
        /// <param name="g">The green component of this color.</param>
        /// <param name="b">The blue component of this color.</param>
        public Color3(float r, float g, float b)
        {
            R = (byte)(r * 255);
            G = (byte)(g * 255);
            B = (byte)(b * 255);
        }
        /// <summary>
        /// Creates a new gray color of the specified shade.
        /// </summary>
        /// <param name="shade">The shade of gray to create.</param>
        public Color3(byte shade)
            : this(shade, shade, shade)
        {
        }
        #endregion
    }
}
