using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PuzzleGameLibrary
{
    [Serializable]
    public class Vector2
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector2(int _x, int _y)
        {
            X = _x;
            Y = _y;
        }

        public static Vector2 operator + (Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, int b)
        {
            return new Vector2(a.X * b, a.Y * b);
        }

        public static Vector2 operator *(int a, Vector2 b)
        {
            return new Vector2(a * b.X, a * b.Y);
        }

        public static Vector2 operator *(Vector2 a, float b)
        {
            return new Vector2((int)(a.X * b), (int)(a.Y * b));
        }

        public static Vector2 operator *(float a, Vector2 b)
        {
            return new Vector2((int)(a * b.X), (int)(a * b.Y));
        }

        public static Vector2 operator /(Vector2 a, int b)
        {
            return new Vector2(a.X / b, a.Y / b);
        }

        public static Vector2 operator /(int a, Vector2 b)
        {
            return new Vector2(a / b.X, a / b.Y);
        }

        public static Vector2 operator /(Vector2 a, float b)
        {
            return new Vector2((int)(a.X / b), (int)(a.Y / b));
        }

        public static Vector2 operator /(float a, Vector2 b)
        {
            return new Vector2((int)(a / b.X), (int)(a / b.Y));
        }
    }
}
