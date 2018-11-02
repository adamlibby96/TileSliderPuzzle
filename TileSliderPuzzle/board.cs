using System;
using System.Collections.Generic;
using System.Text;

namespace TileSliderPuzzle
{
    class Board
    {
        //char[,] currentBoard;
        List<Node> currentBoard;
        Board pred;
        int gValue;
        int hValue;
        int fValue;

        private int rowSize = 3;
        private int colSize = 3;

        public Board(char[] board)
        {
            // init currentBoard;
            currentBoard = new List<Node>();
            int index = 0;
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    Node temp = new Node();
                    temp.setPosition(row, col);
                    temp.setValue((int) char.GetNumericValue(board[index]));
                    index++;
                    currentBoard.Add(temp);
                }
            }


            pred = null;
            gValue = 0;
            hValue = 0;
            fValue = 0;
        }       

        public void setGoal(char[] goal)
        {
            int index = 0;
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    int val = (int)char.GetNumericValue(goal[index]);
                    foreach (Node node in currentBoard)
                    {
                        if (node.getValue() == val)
                        {
                            node.setGoalPosition(row, col);
                            Console.WriteLine("found " + node.getValue() + " at: " + node.getGoalPosition());
                        }
                    }
                    index++;
                }
            }
        }

        public override string ToString()
        {
            int index = 0;
            string result = " _____________\n";
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    result += " | " + currentBoard[index].getValue() + "// goal: " + currentBoard[index].getGoalPosition();
                    index++;
                }
                result += " |\n _____________\n";
            }
            return result;
        }
    }
}
