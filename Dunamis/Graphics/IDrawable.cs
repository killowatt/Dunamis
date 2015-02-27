using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dunamis.Graphics
{
    public interface IDrawable
    {
        void Draw(Renderer renderer);
    }
}
