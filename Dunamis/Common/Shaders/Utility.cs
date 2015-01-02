using System;
using System.IO;
using System.Reflection;
using Dunamis.Graphics;

namespace Dunamis.Common.Shaders
{
    public static class Utility
    {
        static Assembly assembly = Assembly.GetExecutingAssembly();
        public static string GetSource(string file)
        {
            StreamReader streamReader = new StreamReader(assembly.GetManifestResourceStream(file));
            return streamReader.ReadToEnd();
        }
    }
}
