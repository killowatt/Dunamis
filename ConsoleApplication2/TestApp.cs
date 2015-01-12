using Dunamis;
using Dunamis.Common.Meshes;
using Dunamis.Graphics;

namespace ConsoleApplication2
{
    public class TestApp
    {
        Renderer renderer;
        Window window;

        Cube cube;
        ShaderTest4 ourShader;

        public void Do()
        {
            window = new Window(1280, 720); // Create a window with the resolution 1280x720.
            renderer = new Renderer(window, true); // Create our renderer using our window, enabling vsync.
            renderer.ClearColor = new Color3(12, 12, 12); // Set our clear color to an almost black color.

            Texture t = new Texture("Untitled.png", TextureFilter.Anisotropic16X, true);

            ourShader = new ShaderTest4(); // Create our shader.
            ourShader.Texture = t;
            cube = new Cube(ourShader); // Create our cube using our shader.
            cube.SetMesh(RenderTextureMesh.Vertices, RenderTextureMesh.TextureCoordinates, indices: RenderTextureMesh.Indices);
            //cube.SetShader(ourShader);

            renderer.Camera.Position = new Vector3(2, 2, 2); // Set our camera position to 2, 2, 2 (XYZ)
            renderer.Camera.Pitch = Angle.CreateDegrees(35); // Set our camera's pitch.
            renderer.Camera.Yaw = Angle.CreateDegrees(315); // Set our camera's yaw.

            while (true)
            {
                cube.Yaw += 0.01f;
                renderer.Clear(); // Clear the screen.

                renderer.Draw(cube); // Draw our cube.

                renderer.Display(); // Display the result.
                window.Update(); // Update window events.
            }
        }
    }
}
