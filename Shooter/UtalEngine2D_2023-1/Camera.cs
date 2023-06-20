using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public class Camera
    {
        public int xSize = 400;
        public int ySize = 400;
        public Vector2 Position;
        public float scale = 1;

        public Camera(int xSize, int ySize, float scale)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            this.scale = scale;
        }
    }
}
