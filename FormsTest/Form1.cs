using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dunamis;
using Dunamis.Graphics;

namespace FormsTest
{
    public partial class Form1 : Form
    {
        Renderer renderer;

        public void Update()
        {
        }
        public void Render()
        {
            renderer.Clear();

            renderer.Display();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Render();

            base.OnPaint(e);
        }
        public Form1()
        {
            InitializeComponent();

            renderer = new Renderer(dunamisControl1, false);
            renderer.ClearColor = Color3.Pink;
    
           // while (true)
           // {
           //     Render();
           // }
        }
    }
}
