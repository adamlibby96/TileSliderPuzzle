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
        public Moves moveToGetHere { get; set; }

        private int rowSize = 3;
        private int colSize = 3;

        public Board()
        {
            currentBoard = new List<Node>();
            pred = new Board();
            gValue = 0;
            hValue = 0;
            fValue = 0;
        }

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
                    temp.setPosition(col, row);
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
        
        public Board(List<Node> board, Board parent)
        {
            currentBoard = board;
            pred = parent;
            gValue = parent.gValue + 1;
        }

        public bool isComplete()
        {
            foreach (Node cur in currentBoard)
            {
                
                if (!cur.isAtGoalPosition())
                {
                    return false;
                }
            }
            return true;
        }

        public int manHattenDist()
        {
            int result = 0;
            foreach (Node n in currentBoard)
            {
                // h = distance + # of tiles in the way -- maybe eventually
                if (n.getValue() != -1 && !n.isAtGoalPosition()) // ignore the * node and nodes that are completed
                {
                    result += n.getDistanceToGoal();
                }
            }
            return result;
        }

        private void evaluate()
        {
            foreach (Node n in currentBoard)
            {
                // h = distance + # of tiles in the way -- maybe eventually
                if (n.getValue() != -1 && !n.isAtGoalPosition()) // ignore the * node and nodes that are completed
                {
                    fValue += n.getDistanceToGoal();
                    Node goalPos = currentBoard.Find(temp => temp.getCurrentPosition() == n.getGoalPosition());
                    if (goalPos.getValue() != -1)
                    {
                        fValue++;
                    }
                }
            }

            hValue = gValue + fValue;
        }

        //private int getTilesInNodePath(Node cur)
        //{
        //    int count = 0;
        //    foreach (Node n in currentBoard)
        //    {

        //    }
        //    return count;
        //}

        public List<Node> getBoard()
        {
            List<Node> temp = new List<Node>();
            for (int i = 0; i < currentBoard.Count; i++)
            {
                temp.Add(currentBoard[i]);
            }
            return temp;
        }

        public int getHeuristic()
        {
            //evaluate();
            return manHattenDist() + gValue;
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
                            node.setGoalPosition(col, row);
                            //Console.WriteLine("found " + node.getValue() + " at: " + row + "," + col);
                        }
                    }
                    index++;
                }
            }
        }

        public Dictionary<Moves, Node> getPossibleMoves()
        {
            Dictionary<Moves, Node> neighbors = new Dictionary<Moves, Node>();

            foreach (Node n in currentBoard)
            {
                if (n.getValue() == -1) // if it is the star
                {
                    neighbors = n.GetNeighbors(currentBoard);
                    break;
                }
            }

            return neighbors;
        }

        public void displayNeighborStuff()
        {
            // testing of getNeighbors
            foreach (Node n in currentBoard)
            {
                Dictionary<Moves, Node> neighbors = new Dictionary<Moves, Node>();
                neighbors = n.GetNeighbors(currentBoard);
                foreach (KeyValuePair<Moves, Node> pair in neighbors)
                {
                    Console.Write(n.getValue() + ": " + pair.Key + "- " + pair.Value.getValue() + ",  ");
                }
                Console.WriteLine();
            }
        }

        public bool areTheseBoardsEqual(List<Node> a, List<Node> b)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Point curPoint = new Point();
                    curPoint.x = col;
                    curPoint.y = row;

                    Node tempA = a.Find(x => x.getCurrentPosition() == curPoint);
                    Node tempB = b.Find(x => x.getCurrentPosition() == curPoint);

                    if (tempA.getValue() != tempB.getValue())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override string ToString()
        {
            string result = " _____________\n";

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Point curPoint = new Point();
                    curPoint.x = x;
                    curPoint.y = y;
                    Node temp = currentBoard.Find(n => n.getCurrentPosition() == curPoint);
                    if (temp.getValue() < 0)
                    {
                        result += " | *";
                    }
                    else
                    {
                        result += " | " + temp.getValue();
                    }
                }
                result += " |\n _____________\n";
            }
            
            return result;
        }
    }
}
