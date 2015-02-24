using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunamis.Input
{
    public class MouseState
    {
        private int _x, _y, _xDelta, _yDelta;
        private float _wheelPosition;
        private HashSet<Button> _downButtons;

        internal MouseState(Mouse m)
        {
            _x = m.X;
            _y = m.Y;
            _xDelta = m.XDelta;
            _yDelta = m.YDelta;
            _wheelPosition = m.WheelPosition;
            foreach(Button button in Enum.GetValues(typeof(Button)))
                if(m.IsButtonDown(button))
                    _downButtons.Add(button);
        }

        public bool IsButtonDown(Button b)
        {
            return _downButtons.Contains(b);
        }

        public bool IsButtonUp(Button b)
        {
            return !IsButtonDown(b);
        }
    }
}
