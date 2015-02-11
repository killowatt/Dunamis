using System;
using System.IO;
using Dunamis;
using Dunamis.Common.Meshes;
using Dunamis.Graphics;
using Dunamis.Input;

namespace DunamisExamples.Examples
{
    public class CubeExample : BaseExample
    {
        Renderer renderer;
        Window window;

        Mesh cube;
        ShaderTest4 ourShader;
        ShaderTest4 xxxx;

        Keyboard k;
        Sprite sprite;

        Mesh xxxxddddd;

        public CubeExample()
        {
        }

        public override void Run()
        {
            window = new Window(1280, 720); // Create a window with the resolution 1280x720.
            renderer = new Renderer(window, false); // Create our renderer using our window, enabling vsync.
            renderer.ClearColor = new Color3(12, 12, 12); // Set our clear color to an almost black color.

            k = new Keyboard(window);

            Texture t = new Texture("Untitled.png", TextureFilter.Nearest, true);

            ourShader = new ShaderTest4(); // Create our shader.
            ourShader.Texture = t;
            xxxx = new ShaderTest4();
            ourShader.Texture = t;
            //cube = new Cube(ourShader); // Create our cube using our shader.
            cube = new Mesh(RenderTextureMesh.AVertices, RenderTextureMesh.ATextureCoordinates, new float[0],
                RenderTextureMesh.AIndices, ourShader);
            xxxxddddd = new Mesh(RenderTextureMesh.AVertices, RenderTextureMesh.ATextureCoordinates, new float[0],
                RenderTextureMesh.AIndices, ourShader);
            sprite = new Sprite(200, 200, t);
            sprite.X = 0;
            sprite.Y = 200;
            sprite.Width = 400;

            renderer.Camera.Position = new Vector3(2, 2, 2); // Set our camera position to 2, 2, 2 (XYZ)
            renderer.Camera.Pitch = Angle.CreateDegrees(35); // Set our camera's pitch.
            renderer.Camera.Yaw = Angle.CreateDegrees(315); // Set our camera's yaw.

            //Text x = new Text("DUNAMIS", new Font("DINRg.ttf"), 24, false, false, false, Color4.White, true, 0, 0);
            //  x.String = "HELLO";

            NewText.Test();

            while (true)
            {
                cube.Yaw += 0.0005f;
                renderer.Clear(); // Clear the screen.

                renderer.Draw(cube); // Draw our cube.
                renderer.Draw(xxxxddddd); // TODO: fix texture not switching to empty
                renderer.Draw(sprite);
                // renderer.Draw(x);
                renderer.Draw(NewText.xxx);

                renderer.Display(); // Display the result.
                window.Update(); // Update window events.

                if (k.IsKeyDown(Key.Z))
                {
                    renderer.Camera.FieldOfView -= 0.05f;
                }
                if (k.IsKeyDown(Key.X))
                {
                    renderer.Camera.FieldOfView += 0.05f;
                }
                if (k.IsKeyDown(Key.A))
                {
                    renderer.Camera.Yaw -= 0.0005f;
                }
                if (k.IsKeyDown(Key.D))
                {
                    renderer.Camera.Yaw += 0.0005f;
                }
                if (k.IsKeyDown(Key.W))
                {
                    renderer.Camera.Pitch -= 0.0005f;
                }
                if (k.IsKeyDown(Key.S))
                {
                    renderer.Camera.Pitch += 0.0005f;
                }
            }
        }
    }
}
