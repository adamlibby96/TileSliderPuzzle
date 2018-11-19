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
    /* Class: Game
    *      Use: main game class that keeps track of the current board state and goal state,
    *           it also holds a list of children for the current board (children represent possible next boards)
    *           and Game keeps track of the closed list, which holds all the boards we moved to
    */
    class Game
    {
        // the current board we are on
        Board currentBoard;
        // the goal board
        Board goal;
        // the possible boards we can move to from the current board
        List<Board> children;
        // the stack of moves we have done to get to where we are
        Stack<Moves> history;
        // list to hold all the boards we have been too
        List<Board> closedList;

        /* Function: Game Constructor
         *      Params: char array Starting board, and char array goal board
         *      Use: initialize the game with the parameters 
         *      Return: none
        */
        public Game(char[] startBoard, char[] goalBoard)
        {
            // init closed list to a new list
            closedList = new List<Board>();
            // init history to a new stack
            history = new Stack<Moves>();
            // assign current board to a new board with the passed start board
            currentBoard = new Board(startBoard);
            // set the goal of the current board to the passed goal board
            currentBoard.setGoal(goalBoard);
            // save the goal board 
            goal = new Board(goalBoard);
            // init the chidren to a new list
            children = new List<Board>();
            // add the start board to the closed list since this is where we are starting 
            closedList.Add(currentBoard);
        }
         /* Function: ManualSolve
         *      Params: none
         *      Use: contains functionallity to manually solve the puzzle 
         *      Return: none
        */
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

        /* Function: isFinished
         *      Params: none
         *      Use: check if the current board is the goal board
         *      Return: bool: true if complete, false otherwise 
        */
        public bool isFinished()
        {
            return currentBoard.isComplete();
        }

        /* Function: autoSolveNoBoardDisplay
         *      Params: none
         *      Use: solves the board without displaying feedback for the user
         *              still displays the moves after completion
         *      Return: none
        */
        public void autoSolveNoBoardDisplay()
        {
            // generate first set of children
            generateChildren(currentBoard);

            // bool to check if the board is solved
            bool solved = false;
            // loop till we find a solution or fail
            while (!solved)
            {
                // test children for lowest h value
                Board lowest = null;
                int lowestH = 1000;
                for (int i = 0; i < children.Count; i++)
                {
                    int h = children[i].getHeuristic();
                    if (h < lowestH)
                    {
                        lowestH = h; // save the lowest h value
                        lowest = children[i]; // save lowest board
                    }
                }

                // if lowest is null, we failed to find a board
                if (lowest != null)
                {
                    // add lowest to the close list
                    closedList.Add(lowest);
                    // save the move
                    history.Push(lowest.moveToGetHere);
                }
                else
                {
                    Console.WriteLine("Error: could not find lowest board");
                    return;
                }
                // check if the lowest is the goal board
                solved = lowest.isComplete();
                
                // if we are not solved, generate the next set of children based on the lowest board
                if (!solved)
                {
                    generateChildren(lowest);
                }
            } // end loop

            // display how many moves we solved it in
            Console.WriteLine("Solved in: " + history.Count + " moves!");
            // display the moves it took to solve it
            displayMoves();
        }

        /* Function: autoSolveWithWatching
         *      Params: miliseconds 
         *      Use: solve the board (same logic as above) but with feedback to 
         *              the user, with a pause between moves denoted in milliseconds 
         *      Return: none
        */
        public void autoSolveWithWatching(int miliseconds)
        {
            // generate first set of children
            generateChildren(currentBoard);

            // bool to check if we solved the puzzle or not
            bool solved = false;
            // loop till we find a solution or fail
            while (!solved)
            {
                // test children for lowest h value
                Board lowest = null;
                int lowestH = 1000;
                for (int i = 0; i < children.Count; i++)
                {
                    int h = children[i].getHeuristic();
                    if (h < lowestH)
                    {
                        lowestH = h; // save the h value
                        lowest = children[i]; // save lowest board
                    }
                }

                // if lowest is null, we have failed to find a solution
                if (lowest != null)
                {
                    // add lowest to the close list
                    closedList.Add(lowest);
                    // save the move
                    history.Push(lowest.moveToGetHere);
                }
                else
                {
                    Console.WriteLine("Error: could not find lowest board");
                    return;
                }

                // check to see if we solved it
                solved = lowest.isComplete();

                // display the move we just did, and the board for feedback to the user 
                Console.WriteLine("Moved: " + lowest.moveToGetHere + ";\n" + lowest.ToString());
                Console.WriteLine(lowest.getHeuristic() + " == heuristic\n");

                // if we still haven't found a solution, generate next set of children, 
                //      and wait for amount of time (from user input) and then clear the console
                if (!solved)
                {
                    generateChildren(lowest);
                    System.Threading.Thread.Sleep(miliseconds);
                    Console.Clear();
                }
            } // end loop 

            // display the move count and the moves it took to solve the puzzle 
            Console.WriteLine("\nSolved in: " + history.Count + " moves!\n");
            displayMoves();
        }

        /* Function: displayMoves
         *      Params: none
         *      Use: display the moves from the stack history in the proper order
         *      Return: none
        */
        private void displayMoves()
        {
            // string to keep track of the moves
            string output = "";
            // loop through the stack, reverse the move it took and add it to the front of the 
            //      output so it is in the proper order 
            while (history.Count > 0)
            {
                output = flipMove(history.Pop()).ToString()[0] + " " + output;
            }
            // display the moves to the user
            Console.WriteLine("Moves: " + output);
        }

        /* Function: move
         *      Params: Moves move
         *      Use: this is for manual solving and it takes in a direction, and 
         *              adjusts the current board based on that move
         *      Return: none
        */
        private void move(Moves move)
        {
            // get the possible moves from the current state
            Dictionary<Moves, Node> moves = currentBoard.getPossibleMoves();
            // if we can move in the direction of the passed Moves variable, move
            //      otherwise, output an error message
            if (moves.ContainsKey(move))
            {
                // generate a copy of the current board state
                List<Node> tempBoard = currentBoard.getBoard().ConvertAll(n => new Node(n));
                // find the blank tile and store it in a temp variable
                Node blankTile = tempBoard.Find(n => n.getValue() == -1);
                // find the neighbor we want to move to
                Node neighbor = tempBoard.Find(n => n.getValue() == moves[move].getValue());
                
                // save the blank spot position
                Point blanksOriginalPosition = blankTile.getCurrentPosition();

                // remove the 2 nodes from the board
                tempBoard.Remove(blankTile);
                tempBoard.Remove(neighbor);
                
                // set the position of the blank to the neighbors position
                blankTile.setPosition(neighbor.getCurrentPosition());
                // set the neighbors position to the previously saved blank position
                neighbor.setPosition(blanksOriginalPosition);

                // add the 2 nodes back to the board
                tempBoard.Add(blankTile);
                tempBoard.Add(neighbor);

                // update the current board to the new board
                currentBoard = new Board(tempBoard, currentBoard);
                // display the board to the screen
                Console.WriteLine(currentBoard.ToString());
            }
            else
            { 
                // error message for when the player tries to move in an invalid direction
                Console.WriteLine(currentBoard.ToString());
                Console.WriteLine("\nCannot move in that direction");
            }
        }

        /* Function: flipMove
         *      Params: Moves move
         *      Use: takes the passed move and reverses its direction
         *      Return: Moves (reversed) 
        */
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
        }

        /* Function: generateChildren
         *      Params: Board current board
         *      Use: assign the children list to the next possible board we can move to
         *      Return: none
        */
        private void generateChildren(Board curBoard)
        {
            // re-init the children list to a new list
            children = new List<Board>();
            // get the boards possible moves
            Dictionary<Moves, Node> moves = curBoard.getPossibleMoves();
            // foreach possible move, generate a new board with that move preformed 
            foreach (KeyValuePair<Moves, Node> pair in moves)
            {
                // generate a copy of the passed board
                List<Node> tempChild = curBoard.getBoard().ConvertAll(n => new Node(n));
                // get the blank node
                Node blank = tempChild.Find(n => n.getValue() == -1); 
                // get the node we are swapping
                Node tempNode = tempChild.Find(n => n.getValue() == pair.Value.getValue());
                // save the blank spot's position
                Point blankPos = blank.getCurrentPosition();

                // remove the 2 nodes from the board
                tempChild.Remove(blank);
                tempChild.Remove(tempNode);

                // swap the positions
                blank.setPosition(tempNode.getCurrentPosition());
                tempNode.setPosition(blankPos);

                // add the updated nodes back to the board
                tempChild.Add(blank);
                tempChild.Add(tempNode);

                // create a new board with the updated move
                Board tempBoard = new Board(tempChild, curBoard);
                // assign what move it took to get here
                tempBoard.moveToGetHere = pair.Key;
                
                // bool to check if the closed list contains the new board we just generated 
                bool isfound = false;
                if (closedList.Count > 0)
                {
                    // loop through the closed list
                    foreach(Board board in closedList)
                    {
                        if (tempBoard.areTheseBoardsEqual(tempBoard.getBoard(), board.getBoard()))
                        {
                            // if we are in here, we have been to the board state before, so we dont add it to the list
                            isfound = true;
                        }
                    }
                }
                // if the new board is not in the closed list, we can add it to the children
                if (!isfound)
                {
                    children.Add(tempBoard);
                }
                
            }
        }

        /* Function: displayStart
         *      Params: none
         *      Use: display the starting board to the console window for user feedback
         *      Return: none
        */
        public void displayStart()
        {
            Console.WriteLine("\n    START\n" + currentBoard.ToString());
        }

        /* Function: displayGoal
         *      Params: none
         *      Use: display the goal board to the window for user feedback 
         *      Return: none
        */
        public void displayGoal()
        {
            Console.WriteLine("\n    GOAL\n" + goal.ToString());
        }
    }
}
