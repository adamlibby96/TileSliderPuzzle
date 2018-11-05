using System;

namespace TileSliderPuzzle
{
    public enum Moves { Left, Up, Right, Down };

    public struct Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public static bool operator== (Point a, Point b)
        {
            return (a.x == b.x && a.y == b.y);
        }

        public static bool operator!= (Point a, Point b)
        {
            return (a.x != b.x || a.y != b.y);
        }

        public override string ToString()
        {
            return "x: " + x + ", y: " + y;
        }
    }

}