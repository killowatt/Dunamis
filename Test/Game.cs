using System;
using System.Diagnostics;
using System.Linq;
using Dunamis;
using Dunamis.Content;
using Dunamis.Graphics;
using Dunamis.Input;
using Test.Shaders;

namespace Test
{
    public class Game
    {
        Window window;
        Renderer renderer;
        RenderTexture renderTexture;

        ResourceManager resourceManager;

        Mesh mesh;
        Animation anim;
        Texture tex;
        BasicShader shader;

        Stopwatch timer;
        Keyboard keyboard;

        public void Update()
        {
            window.Update();

            if (keyboard.IsKeyDown(Key.Space))
            {
                shader.angle += 0.05f / 250;
            }

            float runningtime = (float)timer.ElapsedMilliseconds / 1000.0f;
            anim.Update(runningtime, mesh.Bones);

            mesh.TEMP();

            if (timer.ElapsedMilliseconds >= 1500)
            {
                timer.Reset();
                timer.Start();
            }
        }
        public void Render()
        {
            renderTexture.Clear();

            renderer.DrawMesh(mesh);

            renderTexture.Finish();

            // SPLIT

            renderer.Clear();

            renderer.DrawTexture(renderTexture.Color);
            
            renderer.Display();
            //Console.WriteLine("OpenGL Error: " + OpenTK.Graphics.OpenGL.GL.GetError());
        }

        public Game()
        {
            window = new Window(1280, 720, "Test", WindowType.Window, Window.DefaultDisplay, true);
            renderer = new Renderer(window, false);
            renderer.ClearColor = new Color3(59, 79, 114);

            renderTexture = new RenderTexture(2560, 1440); // HIGH RESOLUTION

            resourceManager = new ResourceManager();
            resourceManager.AddLoader(new Dunamis.Content.Loaders.TextureLoader());
            resourceManager.AddLoader(new Dunamis.Content.Loaders.ModelLoader());

            tex = resourceManager.Load<Texture>("Textures/test.png");
            mesh = resourceManager.Load<Mesh>("Models/human.dae");

            Camera camera = new Camera(new Vector3(0, -6, 0.1f), new Vector2(15, 15), 75, new Vector2(1280, 720));
            camera.LookAt(new Vector3(0, 0, 0));
            shader = new BasicShader(mesh.ModelMatrix, camera, 0.05f, tex, mesh.Bones.Values.Cast<Bone>().ToArray());
            mesh.Shader = shader;

            keyboard = new Keyboard(window);

            Console.WriteLine("Vertex Shader Status: " + shader.GetCompileStatus(ShaderType.Vertex));
            Console.WriteLine("Vertex Shader Log: " + shader.GetCompileLog(ShaderType.Vertex));
            Console.WriteLine();
            Console.WriteLine("Fragment Shader Status: " + shader.GetCompileStatus(ShaderType.Fragment));
            Console.WriteLine("Fragment Shader Log: " + shader.GetCompileLog(ShaderType.Fragment));
            Console.WriteLine("OpenGL Error: " + OpenTK.Graphics.OpenGL.GL.GetError());

            anim = mesh.Animations[0];

            timer = new Stopwatch();
            timer.Start();

            while (true)
            {
                Update();
                Render();
            }
        }
    }
}
