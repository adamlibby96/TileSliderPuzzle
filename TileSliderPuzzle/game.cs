using System;
using System.Collections.Generic;
using System.Text;

namespace TileSliderPuzzle
{
    class Game
    {
        Board currentBoard;
        Board goal;
        List<Board> children;
        

        public Game(char[] startBoard, char[] goalBoard)
        {
            currentBoard = new Board(startBoard);
            currentBoard.setGoal(goalBoard);
            goal = new Board(goalBoard);
            children = new List<Board>();
        }

        public void Solve()
        {
            generateChildren(currentBoard);
            int childCount = children.Count;
            for (int i = 0; i < childCount; i++)
            {
                Console.WriteLine("Children "+ i + " children: \n");
                generateChildren(children[i]);
            }
            
        }

        private void generateChildren(Board curBoard)
        {
            Dictionary<Moves, Node> moves = curBoard.getPossibleMoves();
            foreach (KeyValuePair<Moves, Node> pair in moves)
            {
                Console.WriteLine(pair.Value.getValue() + ": " + pair.Key.ToString() + ", " + pair.Value.getCurrentPosition());

                //List<Node> tempChild = currentBoard.getBoard();// not deep copy
                List<Node> tempChild = curBoard.getBoard().ConvertAll(n => new Node(n));
                Node blank = tempChild.Find(n => n.getValue() == -1); // get the blank node
                Node tempNode = tempChild.Find(n => n.getValue() == pair.Value.getValue()); // get the node we are swapping
                Point blankPos = blank.getCurrentPosition();

                tempChild.Remove(blank);
                tempChild.Remove(tempNode);

                blank.setPosition(tempNode.getCurrentPosition());
                tempNode.setPosition(blankPos);

                tempChild.Add(blank);
                tempChild.Add(tempNode);

                Board tempBoard = new Board(tempChild, curBoard);
                
                children.Add(tempBoard);
                
                switch (pair.Key)
                {
                    case Moves.Left:
                        Console.WriteLine("\n\n Left Child: \n" + tempBoard.ToString());
                        break;
                    case Moves.Up:
                        Console.WriteLine("\n\n Up Child: \n" + tempBoard.ToString());
                        break;
                    case Moves.Right:
                        Console.WriteLine("\n\n Right Child: \n" + tempBoard.ToString());
                        break;
                    case Moves.Down:
                        Console.WriteLine("\n\n Down Child: \n" + tempBoard.ToString());
                        break;
                    default:
                        Console.WriteLine("\n\n Could Not Generate Children \n");
                        break;
                }
            }
        }

        public void displayStart()
        {
            //currentBoard.displayNeighborStuff();

            Console.WriteLine("\n    START\n" + currentBoard.ToString());
        }

        public void displayGoal()
        {
            Console.WriteLine("\n    GOAL\n" + goal.ToString());
        }
    }
}
