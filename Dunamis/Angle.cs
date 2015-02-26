using System;

namespace Dunamis
{
    public struct Angle
    {
        /// <summary>
        /// The value of this angle in radians.
        /// </summary>
        public float Radians;

        /// <summary>
        /// The value of this angle in degrees.
        /// </summary>
        public float Degrees
        {
            get
            {
                return Radians * 180 / (float)Math.PI;
            }
            set
            {
                Radians = value * (float)Math.PI / 180;
            }
        }

        #region Methods
        /// <summary>
        /// Creates a new angle from the specified number of degrees.
        /// </summary>
        /// <param name="degrees">The value of the angle to be created, in degrees.</param>
        /// <returns>A new angle with the specified value.</returns>
        public static Angle CreateDegrees(float degrees)
        {
            return new Angle(degrees * (float)Math.PI / 180);
        }
        /// <summary>
        /// Creates a new angle from the specified number of radians.
        /// </summary>
        /// <param name="radians">The value of the angle to be created, in radians.</param>
        /// <returns>A new angle with the specified value.</returns>
        public static Angle CreateRadians(float radians)
        {
            return new Angle(radians);
        }
        /// <summary>
        /// Creates a new angle representing one full circle (2π radians or 360 degrees).
        /// </summary>
        /// <returns></returns>
        public static Angle Full()
        {
            return new Angle((float)Math.PI * 2);
        }
        /// <summary>
        /// Creates a new angle representing a half circle (π radians or 180 degrees).
        /// </summary>
        /// <returns></returns>
        public static Angle Half()
        {
            return new Angle((float)Math.PI);
        }
        //public static Angle Third()
        //{
        //    return new Angle(Full
        //}
        /// <summary>
        /// Creates a new angle representing a quarter of a circle (0.5π radians or 90 degrees).
        /// </summary>
        /// <returns></returns>
        public static Angle Quarter()
        {
            return new Angle((float)Math.PI / 2);
        }
        #endregion

        #region Operators
        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left.Radians + right.Radians);
        }
        public static Angle operator -(Angle left, Angle right)
        {
            return new Angle(left.Radians - right.Radians);
        }
        public static Angle operator *(Angle left, Angle right)
        {
            return new Angle(left.Radians * right.Radians);
        }
        public static Angle operator /(Angle left, Angle right)
        {
            return new Angle(left.Radians / right.Radians);
        }
        public static Angle operator *(Angle angle, float multiplier)
        {
            return new Angle(angle.Radians * multiplier);
        }
        public static Angle operator /(Angle angle, float divisor)
        {
            return new Angle(angle.Radians / divisor);
        }
        public static implicit operator float(Angle angle)
        {
            return angle.Radians;
        }
        public static implicit operator Angle(float radians)
        {
            return new Angle(radians);
        }
        #endregion

        private Angle(float radians)
        {
            Radians = radians;
        }
    }
}
