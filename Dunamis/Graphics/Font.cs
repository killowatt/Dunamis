using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using SharpFont;

namespace Dunamis.Graphics
{
    public class Font
    {
        private static Library library = new Library();
        internal Face Face;

        public Font(string fileName)
        {
            Face = new Face(library, fileName  );
        }
    }
}
