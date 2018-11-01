using System;
using System.Collections.Generic;
using System.Text;

namespace TileSliderPuzzle
{
    class Game
    {
        Board start;
        Board goal;

        public Game(char[] startBoard, char[] goalBoard)
        {
            start = new Board(startBoard);
            goal = new Board(goalBoard);
        }

        public void displayStart()
        {
            Console.WriteLine("\n    START\n" + start.ToString());
        }

        public void displayGoal()
        {
            Console.WriteLine("\n    GOAL\n" + goal.ToString());
        }
    }
}
