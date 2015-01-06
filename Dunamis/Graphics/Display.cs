using OpenTK;

namespace Dunamis.Graphics
{
    public static class Display
    {
        public static int DefaultDisplay = -1;

        public static int GetWidth(int display)
        {
            return DisplayDevice.GetDisplay((DisplayIndex)display).Width;
        }
        public static int GetHeight(int display)
        {
            return DisplayDevice.GetDisplay((DisplayIndex)display).Height;
        }
        public static float GetRefreshRate(int display)
        {
            return DisplayDevice.GetDisplay((DisplayIndex)display).RefreshRate;
        }
    }
}
