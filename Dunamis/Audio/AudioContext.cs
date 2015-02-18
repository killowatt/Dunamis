using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTK = OpenTK.Audio;

namespace Dunamis.Audio
{
    public class AudioContext
    {
        private static OTK.AudioContext _context;

        public static void Initialize()
        {
            _context = new OTK.AudioContext();
        }

        public static void Destroy()
        {
            _context.Dispose();
        }
    }
}
