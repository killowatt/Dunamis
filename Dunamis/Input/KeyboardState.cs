using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunamis.Input
{
    public class KeyboardState
    {
        private HashSet<Key> _downKeys;

        internal KeyboardState(HashSet<Key> downKeys)
        {
            _downKeys = downKeys;
        }

        public bool IsKeyDown(Key k)
        {
            return _downKeys.Contains(k);
        }

        public bool IsKeyUp(Key k)
        {
            return !IsKeyDown(k);
        }
    }
}
