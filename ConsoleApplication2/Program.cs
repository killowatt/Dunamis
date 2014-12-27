using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dunamis.Graphics;

namespace ConsoleApplication2
{
    class Program
    {
        static Window w;
        static Renderer r;

        static void Main(string[] args)
        {
            w = new Window(1280, 720);
            r = new Renderer(w);
            r.ClearColor = new Dunamis.Color3(255, 255, 0);

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
