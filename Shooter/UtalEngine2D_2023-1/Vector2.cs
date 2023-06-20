using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDrawing.UtalEngine2D_2023_1
{
    public struct Vector2
    {
        public float x;
        public float y;
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2(a.x*b, a.y*b);
        }
        public override string ToString()
        {
            return "x " + x + " y " + y;
        }
        public static Vector2 Normalize(Vector2 v)
        {
            return new Vector2((float)(v.x * (1 / Math.Sqrt(Math.Pow(v.x, 2) + Math.Pow(v.y, 2)))),
                               (float)(v.y * (1 / Math.Sqrt(Math.Pow(v.x, 2) + Math.Pow(v.y, 2)))));
        }
    }

}
