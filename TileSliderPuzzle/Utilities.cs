using System;

namespace TileSliderPuzzle
{
    public enum Moves { Left, Up, Right, Down };

    public struct Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public override string ToString()
        {
            return "x: " + x + ", y: " + y;
        }
    }

}