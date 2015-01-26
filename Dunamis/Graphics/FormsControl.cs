using System;
using System.Drawing;
using OpenTK.Platform;

namespace Dunamis.Graphics
{
    public class FormsControl : System.Windows.Forms.Control
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            
        }
        public FormsControl(Renderer renderer)
        {
            renderer.GraphicsContext.MakeCurrent(new FormsWindowInfo(Handle));
        }
    }
    sealed class FormsWindowInfo : IWindowInfo
    {
        IntPtr _handle;

        public IntPtr Handle
        {
            get { return _handle; }
        }
        public void Dispose()
        {
        }
        public FormsWindowInfo(IntPtr handle)
        {
            _handle = handle;
        }
    }
}
