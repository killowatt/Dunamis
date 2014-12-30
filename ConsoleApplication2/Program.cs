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
        static Mesh mesh2;
        static Mesh mesh3;

        static void Main(string[] args)
        {
            w = new Window(1280, 720);
            r = new Renderer(w, false);
            r.ClearColor = new Dunamis.Color3(12, 12, 12);

            ShaderTest testshader = new ShaderTest();
            ShaderTest2 testshader2 = new ShaderTest2();
            mesh = new Mesh(new float[] { -0.75f, 0.25f, 0, -0.25f, 0.25f, 0, -0.5f, 0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader);
            mesh2 = new Mesh(new float[] { 0.75f, -0.25f, 0, 0.25f, -0.25f, 0, 0.5f, -0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader2);
            mesh3 = new Mesh(new float[] { -0.75f, -0.25f, 0, -0.25f, -0.25f, 0, -0.5f, -0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader);

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
            r.Draw(mesh2);
            r.Draw(mesh3);

            r.Display();
        }
    }
}
