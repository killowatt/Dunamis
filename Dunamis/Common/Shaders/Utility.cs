using System;
using System.IO;
using System.Reflection;

namespace Dunamis.Common.Shaders
{
    public static class Utility
    {
        static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        public static string GetSource(string file)
        {
            using (var resourceStream = Assembly.GetManifestResourceStream(file))
            {
                if (resourceStream != null)
                {
                    StreamReader streamReader = new StreamReader(resourceStream);
                    return streamReader.ReadToEnd();
                }
                throw new ArgumentException("File could not be loaded.");
            }
        }
    }
}
