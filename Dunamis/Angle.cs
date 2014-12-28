using System;

namespace Dunamis
{
    public struct Angle
    {
        public float Radians;
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
        //public void Clamp(Angle first, Angle second)
        //{
        //    float first
        //}
        public static Angle CreateDegrees(float degrees)
        {
            return new Angle(degrees * (float)Math.PI / 180);
        }
        public static Angle CreateRadians(float radians)
        {
            return new Angle(radians);
        }
        public static Angle Full()
        {
            return new Angle((float)Math.PI * 2);
        }
        public static Angle Half()
        {
            return new Angle((float)Math.PI);
        }
        //public static Angle Third()
        //{
        //    return new Angle(Full
        //}
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
