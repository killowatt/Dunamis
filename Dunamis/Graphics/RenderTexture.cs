using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class RenderTexture
    {
        internal int FrameBuffer;

        public RenderTexture()
        {
            GL.GenFramebuffers(1, out FrameBuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FrameBuffer);


        }
    }
}
