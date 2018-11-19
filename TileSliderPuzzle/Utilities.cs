/**
 * Written by: Adam Libby
 * Written For: AI assignment 
 *          (Tile Sliding Puzzle using A* Algorithm)
 * Date completed: November 18th 2018
**/

using System;

namespace TileSliderPuzzle
{
    /* Utilities: 
     *  Description:
     *      This holds data structures used in the program      
    */

    /* Enum: Moves 
     *      Use: data structure for representing move directions
    */
    public enum Moves { Nothing, Left, Up, Right, Down };

    /* Struct: Point 
     *      Use: data structure for representing x,y coordinates 
     *          also contains overrides for comparison and a string 
     *          representation of the point
    */
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