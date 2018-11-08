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
        Stack<Moves> history;

        public Game(char[] startBoard, char[] goalBoard)
        {
            history = new Stack<Moves>();
            currentBoard = new Board(startBoard);
            currentBoard.setGoal(goalBoard);
            goal = new Board(goalBoard);
            children = new List<Board>();
        }

        public void ManualSolve()
        {
            bool isSolved = false;
            while (!isSolved)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        move(Moves.Up);
                        break;
                    case ConsoleKey.LeftArrow:
                        move(Moves.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        move(Moves.Right);
                        break;
                    case ConsoleKey.DownArrow:
                        move(Moves.Down);
                        break;
                    default:
                        Console.WriteLine("\nIncorrect Move Key");
                        break;
                }
                isSolved = currentBoard.isComplete();
            }
            Console.WriteLine("Solved! Congrats!");
        }

<<<<<<< HEAD
            foreach(Board b in children)
            {
                b.evaluate();
            }

            /**
            int childCount = children.Count;
            // testing purposes
            for (int i = 0; i < childCount; i++)
=======
        public bool isFinished()
        {
            return currentBoard.isComplete();
        }

        public void AutoSolve()
        {
            generateChildren(currentBoard);  
            while (!currentBoard.isComplete())
>>>>>>> e46d8d507c9ab837e5b689f56aed329ae347c102
            {
                int index = 0;
                int lowestVal = 1000;
                for (int i = 0; i < children.Count; i++)
                {
                    int h = children[i].getHeuristic();
                    if (h < lowestVal)
                    {
                        index = i;
                        lowestVal = h;
                    }
                }

                if (lowestVal > 100)
                {
                    Console.WriteLine("ERROR: something happened when finding lowest heuristic!");
                    return;
                }

                currentBoard = children[index];
                Console.WriteLine("Move: " + currentBoard.moveToGetHere + "\n" + currentBoard.ToString() + "\n");
                history.Push(currentBoard.moveToGetHere);
                generateChildren(currentBoard);

            }
<<<<<<< HEAD
            **/
=======
        }

        private void move(Moves move)
        {
            //Moves correctMove = flipMove(move);
            Dictionary<Moves, Node> moves = currentBoard.getPossibleMoves();
            if (moves.ContainsKey(move))
            {
                List<Node> tempBoard = currentBoard.getBoard().ConvertAll(n => new Node(n));
                Node blankTile = tempBoard.Find(n => n.getValue() == -1);
                Node neighbor = tempBoard.Find(n => n.getValue() == moves[move].getValue());
                
                Point blanksOriginalPosition = blankTile.getCurrentPosition();

                tempBoard.Remove(blankTile);
                tempBoard.Remove(neighbor);
                
                blankTile.setPosition(neighbor.getCurrentPosition());
                neighbor.setPosition(blanksOriginalPosition);

                tempBoard.Add(blankTile);
                tempBoard.Add(neighbor);

                currentBoard = new Board(tempBoard, currentBoard);

                Console.WriteLine(currentBoard.ToString());
            }
            else
            {
                Console.WriteLine(currentBoard.ToString());
                Console.WriteLine("\nCannot move in that direction");
            }
        }

        private Moves flipMove(Moves move)
        {
            Moves m = new Moves();
            switch (move)
            {
                case Moves.Left:
                    m = Moves.Right;
                    break;
                case Moves.Up:
                    m = Moves.Down;
                    break;
                case Moves.Right:
                    m = Moves.Left;
                    break;
                case Moves.Down:
                    m = Moves.Up;
                    break;
                default:
                    break;
            }
            return m;
>>>>>>> e46d8d507c9ab837e5b689f56aed329ae347c102
        }

        private void generateChildren(Board curBoard)
        {
            children = new List<Board>();
            Dictionary<Moves, Node> moves = curBoard.getPossibleMoves();
            foreach (KeyValuePair<Moves, Node> pair in moves)
            {
                //Console.WriteLine(pair.Value.getValue() + ": " + pair.Key.ToString() + ", " + pair.Value.getCurrentPosition());

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
                tempBoard.moveToGetHere = pair.Key;
                children.Add(tempBoard);
                
                //switch (pair.Key)
                //{
                //    case Moves.Left:
                //        Console.WriteLine("\n\n Left Child: \n" + tempBoard.ToString());
                //        break;
                //    case Moves.Up:
                //        Console.WriteLine("\n\n Up Child: \n" + tempBoard.ToString());
                //        break;
                //    case Moves.Right:
                //        Console.WriteLine("\n\n Right Child: \n" + tempBoard.ToString());
                //        break;
                //    case Moves.Down:
                //        Console.WriteLine("\n\n Down Child: \n" + tempBoard.ToString());
                //        break;
                //    default:
                //        Console.WriteLine("\n\n Could Not Generate Children \n");
                //        break;
                //}
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
