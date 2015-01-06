using System;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class RenderTexture
    {
        internal int FrameBuffer; 

        int _width; // TODO: changable width/height
        int _height;
        Texture _color;
        Texture _depth;

        #region Properties
        public int Width
        {
            get
            {
                return _width;
            }
        }
        public int Height
        {
            get
            {
                return _height;
            }
        }
        public Texture Color
        {
            get
            {
                return _color;
            }
        }
        public Texture Depth
        {
            get
            {
                return _depth;
            }
        }
        #endregion

        #region Constructors
        public RenderTexture()
        {
            GL.GenFramebuffers(1, out FrameBuffer);
            _color = new Texture();
            _depth = new Texture();
        }
        public RenderTexture(int width, int height)
            : this()
        {
            _width = width;
            _height = height;
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FrameBuffer);

            _color.Width = width;
            _color.Height = height;
            GL.BindTexture(TextureTarget.Texture2D, _color.TextureId);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgb, PixelType.UnsignedByte, IntPtr.Zero); // TODO: transparent rendertextures?
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, _color.TextureId, 0);

            _depth.Width = width;
            _depth.Height = height;
            GL.BindTexture(TextureTarget.Texture2D, _depth.TextureId);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent24, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.DepthComponent, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, _depth.TextureId, 0);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }
        #endregion

    }
}
