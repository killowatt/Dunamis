using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class RenderTexture
    {
        internal int FrameBuffer;
        internal bool Complete;

        Texture color;
        Texture depth;

        #region Properties
        // TODO: set for giggles
        public Texture Color
        {
            get
            {
                return color;
            }
        }
        public Texture Depth
        {
            get
            {
                return depth;
            }
        }
        public int Width
        {
            get
            {
                if (color.Width == depth.Width)
                {
                    return color.Width;
                }
                else
                {
                    return 0; // TODO: error
                }
            }
        }
        public int Height
        {
            get
            {
                if (color.Height == depth.Height)
                {
                    return color.Height;
                }
                else
                {
                    return 0; // TODO: error
                }
            }
        }
        #endregion

        #region Methods
        public void Clear()
        {
            GL.Viewport(0, 0, Width, Height);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FrameBuffer);
            GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
        }
        public void Finish()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
        #endregion

        public RenderTexture(int width, int height)    // TODO: provide basic 2d shader for this     
        {
            GL.GenFramebuffers(1, out FrameBuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FrameBuffer);

            color = new Texture();
            color.Width = width;
            color.Height = height;

            GL.BindTexture(TextureTarget.Texture2D, color.TextureID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear); // TODO: make it so people can switch between linear and nearest
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, color.TextureID, 0);

            depth = new Texture();
            depth.Width = width;
            depth.Height = height;

            GL.BindTexture(TextureTarget.Texture2D, depth.TextureID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent24, width, height, 0, PixelFormat.DepthComponent, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depth.TextureID, 0);

            FramebufferErrorCode e = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) == FramebufferErrorCode.FramebufferComplete)
            {
                Complete = true;
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
    }
}
