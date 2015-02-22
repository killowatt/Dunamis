using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Dunamis.Graphics
{
    public class PointField
    {
        private Vector3[] _points;
        private int _vao;
        private int _vbo;

        public Shader Shader = null;

        public Vector3[] Points
        {
            get { return _points; }
            set
            {
                _points = value;
                GL.BindVertexArray(VertexArrayObject);
                GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
                List<float> floats = new List<float>();
                for(int i = 0; i < _points.Length; i++)
                {
                    floats.Add(_points[i].X);
                    floats.Add(_points[i].Y);
                    floats.Add(_points[i].Z);
                }
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(sizeof(float) * floats.Count), floats.ToArray(), BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                GL.EnableVertexAttribArray(0);
                GL.BindVertexArray(0);
            }
        }

        public int VertexArrayObject
        {
            get { return _vao; }
        }

        public PointField(params Vector3[] points)
        {
            _points = points;

            GL.GenVertexArrays(1, out _vao);
            GL.BindVertexArray(_vao);

            GL.GenBuffers(1, out _vbo);
            Points = points;
            GL.BindVertexArray(0);
        }
    }
}
