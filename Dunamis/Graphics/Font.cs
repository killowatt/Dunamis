using System;
using System.IO;
using System.Drawing;
using System.Drawing.Text;

namespace Dunamis.Graphics
{
    public class Font
    {
        internal FontFamily Family;

        public static Font Default
        {
            get
            {
                Font font = new Font();
                font.Family = FontFamily.GenericMonospace;
                return font;
            }
        }

        internal Font()
        {
        }
        public Font(string fileName)
        {
            PrivateFontCollection x = new PrivateFontCollection();

            x.AddFontFile(fileName);
            Family = x.Families[0];
            
            //string fullPath = Path.GetFullPath(fileName);
            //if (File.Exists(fullPath))
            //{
            //    Family = new FontFamily(fullPath);
            //}
            //// TODO: else throw fucking FILE NOT FOUD
        }
    }
}
