using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dunamis;
using Dunamis.Graphics;
using Dunamis.Input;
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

        static Keyboard k;
        static Mouse m;

        static void Main(string[] args)
        {
            w = new Window(1280, 720);
            r = new Renderer(w, false);
            r.ClearColor = new Dunamis.Color3(12, 12, 12);
    
            r.Camera.Position = new Vector3(0f, 0f, 1.5f);
            r.Camera.Yaw = Angle.CreateDegrees(360).Radians;

            ShaderTest3 testshader = new ShaderTest3();
            ShaderTest3 testshader2 = new ShaderTest3();
            ShaderTest3 tt = new ShaderTest3(1);
            Console.WriteLine(testshader2.GetCompileLog(Dunamis.Graphics.ShaderType.Vertex));
            Console.WriteLine(testshader2.GetCompileLog(Dunamis.Graphics.ShaderType.Fragment));

            mesh = new Mesh(new float[] { -0.75f, 0.25f, 0, -0.25f, 0.25f, 0, -0.5f, 0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader);
            mesh2 = new Mesh(new float[] { 0.75f, -0.25f, 0, 0.25f, -0.25f, 0, 0.5f, -0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader2);
            mesh3 = new Mesh(new float[] { -0.75f, -0.25f, 0, -0.25f, -0.25f, 0, -0.5f, -0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader);
            mesh = new Mesh(new float[] { -0.5f, -0.5f, 0, 0, 0.5f, 0, 0.5f, -0.5f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, tt);

            mesh.Position = new Vector3(0, 0, 0f);
            mesh.Yaw = Angle.CreateDegrees(45).Radians;

            Console.WriteLine(GL.GetError());

            k = new Keyboard(w);
            m = new Mouse(w);

            while (true)
            {
                Update();
                Render();
            }

        }
        static void Update()
        {
            w.Update();
            if (k.IsKeyDown(Key.A))
            {
                mesh.Yaw += 0.0005f;
            }
            if (k.IsKeyDown(Key.D))
            {
                mesh.Yaw -= 0.0005f;
            }
            if (k.IsKeyDown(Key.W))
            {
                mesh.Pitch += 0.0005f;
            }
            if (k.IsKeyDown(Key.S))
            {
                mesh.Pitch -= 0.0005f;
            }

            if (k.IsKeyDown(Key.Right))
            {
                mesh.X += 0.0005f;
            }
            if (k.IsKeyDown(Key.Left))
            {
                mesh.X -= 0.0005f;
            }
            if (k.IsKeyDown(Key.Up))
            {
                mesh.Y += 0.0005f;
            }
            if (k.IsKeyDown(Key.Down))
            {
                mesh.Y -= 0.0005f;
            }
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
