using System;
using System.Collections.Generic;
using System.Text;

namespace TileSliderPuzzle
{
    class Game
    {
        Board currentBoard;
        Board goal;

        

        public Game(char[] startBoard, char[] goalBoard)
        {
            currentBoard = new Board(startBoard);
            currentBoard.setGoal(goalBoard);
            goal = new Board(goalBoard);

        }

        public void displayStart()
        {
            Console.WriteLine("\n    START\n" + currentBoard.ToString());
        }

        public void displayGoal()
        {
            Console.WriteLine("\n    GOAL\n" + goal.ToString());
        }
    }
}
