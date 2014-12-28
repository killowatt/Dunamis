using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dunamis;
using Dunamis.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ConsoleApplication2
{
    class Program
    {
        static Window w;
        static Renderer r;

        static Vector3 vec;
        static Vector2 test;

        static Angle a;

        static void Main(string[] args)
        {
            w = new Window(1280, 720);
            r = new Renderer(w);
            r.ClearColor = new Dunamis.Color3(255, 255, 0);

            vec = new Vector3(1.0f, 1.0f, 1.0f);
            test = new Vector2(1.0f, 1.0f);

            a = Angle.CreateDegrees(180);
            Console.WriteLine((TextureFilter)2);

            float maxAnisotropy;
            GL.GetFloat((GetPName)ExtTextureFilterAnisotropic.MaxTextureMaxAnisotropyExt, out maxAnisotropy);

            while (true)
            {
                Update();
                Render();
            }

        }
        static void Update()
        {
            w.Update();
        }
        static void Render()
        {
            r.Clear();
            r.Display();
        }
    }
}
