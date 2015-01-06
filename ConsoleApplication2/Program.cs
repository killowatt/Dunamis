using System;
using Dunamis;
using Dunamis.Graphics;
using Dunamis.Input;
using OpenTK.Graphics.OpenGL;
using ShaderType = Dunamis.Graphics.ShaderType;

namespace ConsoleApplication2
{
    class Program
    {
        static Window w;
        static Renderer r;

        static Mesh mesh;
        static Mesh mesh2;
        static Mesh mesh3;

        static bool running;

        static Keyboard k;
        static Mouse m;
        

        static void CLOSING(object o, EventArgs e)
        {
            Console.WriteLine("OH SHIT");
            running = false;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("what");
            //Console.ReadKey();

            w = new Window(1280, 720);
            w.Closing += CLOSING;
            r = new Renderer(w, false);
            r.ClearColor = new Color3(12, 12, 12);


            r.Camera.Position = new Vector3(0f, 0f, 1.5f);
            r.Camera.Yaw = Angle.CreateDegrees(360).Radians;

            ShaderTest3 testshader = new ShaderTest3();
            ShaderTest3 testshader2 = new ShaderTest3();
            ShaderTest3 tt = new ShaderTest3(1);
            Console.WriteLine(testshader2.GetCompileLog(ShaderType.Vertex));
            Console.WriteLine(testshader2.GetCompileLog(ShaderType.Fragment));

            mesh = new Mesh(new[] { -0.75f, 0.25f, 0, -0.25f, 0.25f, 0, -0.5f, 0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader);
            mesh2 = new Mesh(new[] { 0.75f, -0.25f, 0, 0.25f, -0.25f, 0, 0.5f, -0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader2);
            mesh3 = new Mesh(new[] { -0.75f, -0.25f, 0, -0.25f, -0.25f, 0, -0.5f, -0.75f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, testshader);
            mesh = new Mesh(new[] { -0.5f, -0.5f, 0, 0, 0.5f, 0, 0.5f, -0.5f, 0 }, new float[] { }, new float[] { }, new uint[] { 0, 1, 2 }, MeshType.Static, tt);

            mesh.Position = new Vector3(0, 0, 0f);
            mesh.Yaw = Angle.CreateDegrees(45).Radians;

            Console.WriteLine(GL.GetError());

            k = new Keyboard(w);
            m = new Mouse(w);

            running = true;
            while (running)
            {
                Update();
                Render();
            }

        }
        static void Update()
        {
            if (k.IsKeyDown(Key.A))
            {
                r.Camera.Yaw -= 0.0005f;
            }
            if (k.IsKeyDown(Key.D))
            {
                r.Camera.Yaw += 0.0005f;
            }
            if (k.IsKeyDown(Key.W))
            {
                r.Camera.Pitch -= 0.0005f;
            }
            if (k.IsKeyDown(Key.S))
            {
                r.Camera.Pitch += 0.0005f;
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
            
            //Console.WriteLine(krkkr.IsKeyDown(Key.RightShift));
            //mesh.Yaw += 0.0005f;
        }
        static void Render()
        {
            r.Clear();

            r.Draw(mesh);
            r.Draw(mesh2);
            r.Draw(mesh3);

            r.Display();
            w.Update();
        }
    }
}
