using System;
using Dunamis;
using Dunamis.Content;
using Dunamis.Graphics;
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
        Texture tex;
        BasicShader shader;

        public void Update()
        {
            window.Update();
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
            renderer.ClearColor = new Color3(37, 37, 56);

            renderTexture = new RenderTexture(2560, 1440);

            resourceManager = new ResourceManager();
            resourceManager.AddLoader(new Dunamis.Content.Loaders.TextureLoader());
            resourceManager.AddLoader(new Dunamis.Content.Loaders.ModelLoader());

            tex = resourceManager.Load<Texture>("Textures/test.png");
            mesh = resourceManager.Load<Mesh>("Models/test.obj");

            Camera camera = new Camera(new Vector3(2, 2, 2), new Vector2(15, 15), 75, new Vector2(1280, 720));
            camera.LookAt(new Vector3(0, 0, 0));
            shader = new BasicShader(mesh.ModelMatrix, camera, 0.05f, tex);
            mesh.Shader = shader;

            Console.WriteLine("Vertex Shader Status: " + shader.GetCompileStatus(ShaderType.Vertex));
            Console.WriteLine("Vertex Shader Log: " + shader.GetCompileLog(ShaderType.Vertex));
            Console.WriteLine();
            Console.WriteLine("Fragment Shader Status: " + shader.GetCompileStatus(ShaderType.Fragment));
            Console.WriteLine("Fragment Shader Log: " + shader.GetCompileLog(ShaderType.Fragment));
            Console.WriteLine("OpenGL Error: " + OpenTK.Graphics.OpenGL.GL.GetError());

            while (true)
            {
                Update();
                Render();
            }
        }
    }
}
