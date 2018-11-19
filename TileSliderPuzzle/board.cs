/**
 * Written by: Adam Libby
 * Written For: AI assignment 
 *          (Tile Sliding Puzzle using A* Algorithm)
 * Date completed: November 18th 2018
**/

using System;
using System.Collections.Generic;
using System.Text;

namespace TileSliderPuzzle
{
    class Board
    {
        // The current Game board
        List<Node> currentBoard;
        // the parent game board
        Board pred;
        // the g value (how far down the tree we are)
        int gValue;
        // the cost to move to this board
        public int hValue { get; set; }
        // f = h + g
        int fValue;
        // what move did we take to get to this board
        public Moves moveToGetHere { get; set; }

        // the size of the board
        private int rowSize = 3;
        private int colSize = 3;

        // default constructor
        public Board()
        {
            currentBoard = new List<Node>();
            pred = new Board();
            gValue = 0;
            hValue = 0;
            fValue = 0;
        }

        /* Function: Board constructor 
         *      Params: char array 
         *      Use: convert char array to a list of nodes and init the board with default values
         *      Return: none
        */
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

        /* Function: Board constructor
         *      Params: list of nodes: board, and the boards parent board
         *      Use: create a new board and assign the pred to the parent 
         *      Return: none
        */
        public Board(List<Node> board, Board parent)
        {
            currentBoard = board;
            pred = parent;
            gValue = parent.gValue + 1;
        }

        /* Function: isComplete
         *      Params: none
         *      Use: check and see if every node is in their goal spot
         *      Return: bool: true if the board is the goal; false otherwise
        */
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

        /* Function: manHattenDist
         *      Params: none
         *      Use: calculate the manHatten distance of each node, and 
         *              sum them all up for the heuristic value
         *      Return: int: heuristic value of the current board
        */
        public int manHattenDist()
        {
            int result = 0;
            foreach (Node n in currentBoard)
            {
                if (n.getValue() != -1 && !n.isAtGoalPosition()) // ignore the * node and nodes that are completed
                {
                    result += n.getDistanceToGoal();
                }
            }
            return result;
        }

        /* Function: calcCost
         *      Params: none
         *      Use: improved heuristic of the manhatten distance
         *              uses sum of the manhattenDistance + # of tiles in the nodes path
         *      Return: int: heuristic value of the current board
        */
        private int calcCost()
        {
            int cost = 0;

            // for each node in the board, calculate the cost it would take to get 
            //      to its final position
            foreach (Node cur in currentBoard)
            {
                // if the node is the blank, skip the rest of the loop and continue calculation
                if (cur.getValue() == -1)
                {
                    continue;
                }
                Point curPos = cur.getCurrentPosition(); // starting position
                Point goalPos = cur.getGoalPosition();

                // find path to goal
                bool isFound = false;
                while (!isFound)
                {
                    // if we are at the goal, then we have found a path
                    if (curPos == goalPos)
                    {
                        isFound = true;
                    }
                    // if currentPosition is to the left of the goalPosition
                    else if (curPos.x < goalPos.x)
                    {
                        curPos.x++; // move right
                                    // if node to right is blank, cost increases by only 1
                        if (getNodeAtPosition(curPos).getValue() == -1)
                        {
                            cost++;
                        }
                        else // else cost increases by 2 because there was another node there
                        {
                            cost += 2;
                        }
                    }
                    // if we are on the same column as the goal, but we are above the goal
                    else if (curPos.x == goalPos.x && curPos.y < goalPos.y)
                    {
                        curPos.y++;
                        if (getNodeAtPosition(curPos).getValue() == -1)
                        {
                            cost++;
                        }
                        else
                        {
                            cost += 2;
                        }
                    }
                    // if we are to the right of the goalPosition
                    else if (curPos.x > goalPos.x)
                    {
                        curPos.x--;
                        if (getNodeAtPosition(curPos).getValue() == -1)
                        {
                            cost++;
                        }
                        else
                        {
                            cost += 2;
                        }
                    }
                    // if we are below the goal and in the same column
                    else if (curPos.y > goalPos.y && curPos.x == goalPos.x)
                    {
                        curPos.y--;
                        if (getNodeAtPosition(curPos).getValue() == -1)
                        {
                            cost++;
                        }
                        else
                        {
                            cost += 2;
                        }
                    }
                }

            }
            return cost + hValue;
        }

        /* Function: getNodeAtPosition
         *      Params: Point position
         *      Use: find the node at the passed position
         *      Return: Node at the position
        */
        private Node getNodeAtPosition(Point p)
        {
            return currentBoard.Find(x => x.getCurrentPosition() == p);
        }

        /* Function: getBoard
         *      Params: none
         *      Use: getter function for the current board
         *      Return: List of Nodes that represent the current board
        */
        public List<Node> getBoard()
        {
            List<Node> temp = new List<Node>();
            for (int i = 0; i < currentBoard.Count; i++)
            {
                temp.Add(currentBoard[i]);
            }
            return temp;
        }

        /* Function: getHeuristic
         *      Params: none
         *      Use: public function to call calcCost
         *      Return: int
        */
        public int getHeuristic()
        {
            //return manHattenDist();
            return calcCost();
        }

        /* Function: setGoal
         *      Params: char array goal
         *      Use: set the goal position of each node in the current board
         *      Return: none
        */
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
                        }
                    }
                    index++;
                }
            }
        }

        /* Function: getPossibleMoves
         *      Params: none
         *      Use: get the neighbors of the blank node
         *      Return: Dictionary
        */
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

        /* Function: areTheseBoardsEqual
         *      Params: 2 lists of nodes, a and b
         *      Use: compare the 2 boards to see if they are equal
         *      Return: bool: true if they are; false otherwise
        */
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

        /* Function: ToString override
         *      Params: none
         *      Use: set up the desired string representation of the board
         *      Return: string
        */
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
