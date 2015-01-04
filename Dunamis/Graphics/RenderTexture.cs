using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class RenderTexture
    {
        internal int FrameBuffer; 

        int width; // TODO: changable width/height
        int height;
        Texture color;
        Texture depth;

        #region Properties
        public int Width
        {
            get
            {
                return width;
            }
        }
        public int Height
        {
            get
            {
                return height;
            }
        }
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
        #endregion

        #region Constructors
        public RenderTexture()
        {
            GL.GenFramebuffers(1, out FrameBuffer);
            color = new Texture();
            depth = new Texture();
        }
        public RenderTexture(int width, int height)
            : this()
        {
            this.width = width;
            this.height = height;
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FrameBuffer);

            color.Width = width;
            color.Height = height;
            GL.BindTexture(TextureTarget.Texture2D, color.TextureID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero); // TODO: transparent rendertextures?
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, color.TextureID, 0);

            depth.Width = width;
            depth.Height = height;
            GL.BindTexture(TextureTarget.Texture2D, depth.TextureID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent24, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.DepthComponent, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depth.TextureID, 0);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
        #endregion

    }
}
