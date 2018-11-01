using System;
using System.Collections.Generic;
using System.Text;

namespace TileSliderPuzzle
{
    class Board
    {
        char[,] currentBoard;
        Board pred;
        int gValue;
        int hValue;
        int fValue;

        private int rowSize = 3;
        private int colSize = 3;

        public Board(char[] board)
        {
            currentBoard = new char[3, 3];
            int index = 0;
            // fill the 2d array with the input
            for (int row = 0; row < rowSize; row++)
            {
                for (int col = 0; col < colSize; col++)
                {
                    currentBoard[row, col] = board[index];
                    index++;
                }
            }
            
            pred = null;
            gValue = 0;
            hValue = 0;
            fValue = 0;
        }

        public override string ToString()
        {
            string result = " _____________\n";
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    result += " | " + currentBoard[i, j];
                }
                result += " |\n _____________\n";
            }
            return result;
        }
    }
}
