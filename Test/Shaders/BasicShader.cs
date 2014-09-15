using System;
using System.IO;
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
        public Matrix4[] mbones;

        float updaterate = 0;
        public float angle = 0;

        public override void Initialize()
        {
            //Projection = Matrix4.CreateOrthographic(4, 2.25f, 0f, 10.0f); // ORTHO: NO PERSPECTIVE
            addUniform("model", Model, false);
            addUniform("projection", Projection, false);
            addUniform("view", View, false);
            addTexture("diffuse", diffuse);
            addUniform("bones", mbones, false);
        }
        public override void Update()
        {
            cust = Model * Matrix4.CreateRotationZ(angle);
            //angle += updaterate;
            updateUniform("model", cust, false);
            updateUniform("projection", Projection, false);
            updateUniform("view", View, false);
            updateTexture("diffuse", diffuse);
        }
        public BasicShader(Matrix4 model, Camera camera, float updaterate, Texture tex, Bone[] bones)
            : base(File.ReadAllText("Shaders/BasicVertex.txt"), File.ReadAllText("Shaders/BasicFragment.txt"), ShaderState.Dynamic)
        {
            System.Collections.Generic.List<Matrix4> matrices = new System.Collections.Generic.List<Matrix4>();
            foreach (Bone bone in bones)
            {
                matrices.Add(bone.Transformation);
            }
            mbones = matrices.ToArray();
            Model = model;
            Projection = camera.Projection;
            View = camera.View;
            diffuse = tex;
            this.updaterate = updaterate / 250;
        }
    }
}
