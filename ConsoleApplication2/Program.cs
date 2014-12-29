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

        static Mesh mesh;

        static void Main(string[] args)
        {
            w = new Window(1280, 720);
            r = new Renderer(w);
            r.ClearColor = new Dunamis.Color3(12, 12, 12);

            ShaderTest testshader = new ShaderTest();
            mesh = new Mesh(new float[] { -0.5f, -0.5f, 0, 0.5f, -0.5f, 0, 0, 0.5f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader);

            Console.WriteLine(GL.GetError());
            Console.WriteLine(testshader.GetCompileLog(Dunamis.Graphics.ShaderType.Vertex));
            Console.WriteLine(testshader.GetCompileLog(Dunamis.Graphics.ShaderType.Fragment));
            Console.WriteLine(testshader.GetCompileStatus(Dunamis.Graphics.ShaderType.Vertex));
            Console.WriteLine(testshader.GetCompileStatus(Dunamis.Graphics.ShaderType.Fragment));

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

            r.Draw(mesh);

            r.Display();
        }
    }
}
