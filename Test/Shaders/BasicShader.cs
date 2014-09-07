using System;
using System.IO;
using OpenTK;
using Dunamis;
using Dunamis.Graphics;

namespace Test.Shaders
{
    public class BasicShader : Shader
    {
        public Matrix4 Model;
        public Matrix4 Projection;
        public Matrix4 View;

        Matrix4 cust;

        public Texture diffuse;

        float updaterate = 0;
        public float angle = 0;

        public override void Initialize()
        {
            //Projection = Matrix4.CreateOrthographic(4, 2.25f, 0f, 10.0f); // ORTHO: NO PERSPECTIVE
            addUniform("model", Model, false);
            addUniform("projection", Projection, false);
            addUniform("view", View, false);
            addTexture("diffuse", diffuse);
        }
        public override void Update()
        {
            cust = Model * OpenTK.Matrix4.CreateRotationZ(angle);
            //angle += updaterate;
            updateUniform("model", cust, false);
            updateUniform("projection", Projection, false);
            updateUniform("view", View, false);
            updateTexture("diffuse", diffuse);
        }
        public BasicShader(Matrix4 model, Camera camera, float updaterate, Texture tex)
            : base(File.ReadAllText("Shaders/BasicVertex.txt"), File.ReadAllText("Shaders/BasicFragment.txt"), ShaderState.Dynamic)
        {
            Model = model;
            Projection = camera.Projection;
            View = camera.View;
            diffuse = tex;
            this.updaterate = updaterate / 250;
        }
    }
}
