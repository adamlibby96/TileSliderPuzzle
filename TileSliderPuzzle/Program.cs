/**
 * Written by: Adam Libby
 * Written For: AI assignment 
 *          (Tile Sliding Puzzle using A* Algorithm)
 * Date completed: November 18th 2018
**/

using System;
using System.Diagnostics;
using System.IO;

namespace TileSliderPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            // change the color of the console window and text 
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            
            // get input to use the test boards or not
            Console.WriteLine("Use my test boards? (y/n)");
            char[] useTest = Console.ReadLine().ToCharArray();
            if (useTest[0] == 'y' || useTest[0] == 'Y')
            {
                // test function
                testingFunction();
            }
            else
            {
                // bool to see if user wants to watch the board being solved
                bool customWatch = false;

                // waitTime will be how long the board will pause for between moves
                int waitTime = 1000; // default 1 second

                // get user input to test if they want to watch it being solved or not
                Console.WriteLine("Do you want to watch it being solved? (y/n)");
                char[] watchIt = Console.ReadLine().ToCharArray();
                if (watchIt[0] == 'y' || watchIt[0] == 'Y')
                {
                    // get the time between moves from the user
                    Console.WriteLine("Enter the time between moves. (in milliseconds)");
                    waitTime = int.Parse(Console.ReadLine());
                    customWatch = true;
                }

                // get the input from the user for the starting state
                string currentBoardInput = getInput();
                // get the input from the user for the goal state
                string goalInput = getGoalBoard();
                // convert input strings to char arrays
                char[] startBoard = currentBoardInput.ToCharArray();
                char[] goalBoard = goalInput.ToCharArray();

                // initialize the game with the start and goal board
                Game game = new Game(startBoard, goalBoard);

                // display the start board
                game.displayStart();

                // display the goal board
                game.displayGoal();

                // stop watch to determain how fast the algorithm solved the board
                Stopwatch stopwatch = new Stopwatch();

                // if user selected to watch it being solved, call the function to watch 
                if (customWatch)
                {
                    System.Threading.Thread.Sleep(waitTime);
                    Console.Clear();
                    game.autoSolveWithWatching(waitTime);
                }
                // else start the timmer, solve it, and stop the timer
                else
                {
                    stopwatch.Start();
                    game.autoSolveNoBoardDisplay();
                    stopwatch.Stop();
                }
                // display the time it took to solve it
                Console.WriteLine("\nCompleted in " + stopwatch.ElapsedMilliseconds + " milliseconds!");
                
                // function to solve the board using manual input 
                //game.ManualSolve();
            }
            // pause the program to make sure it doesnt close
            Console.ReadKey();
        }

        /* Function: getInput()
         *      Params: none
         *      Use: get the input from the user for the starting board
         *      Return: string representation of the start board
        */
        static string getInput()
        {
            string input = "";

            Console.WriteLine("Enter current board: ");
            Console.Write("Top Row: ");
            input += Console.ReadLine();
            Console.Write("Middle Row: ");
            input += Console.ReadLine();
            Console.Write("Bottom Row: ");
            input += Console.ReadLine();

            return input;
        }


        /* Function: getGoalBoard
         *      Params: none
         *      Use: get the input from the user for the goal board
         *      Return: string representation of the goal board
        */
        static string getGoalBoard()
        {
            string input = "";

            Console.WriteLine("\nEnter goal board: ");
            Console.Write("Top Row: ");
            input += Console.ReadLine();
            Console.Write("Middle Row: ");
            input += Console.ReadLine();
            Console.Write("Bottom Row: ");
            input += Console.ReadLine();

            return input;
        }


        /* Function: getM
         *      Params: none
         *      Use: get the M value in (MxN) for the board size. 
         *           I was going to make it dynamic, but decided to not do this
         *      Return: integer
        */
        static int getM()
        {
            int m;
            Console.WriteLine("Enter board size (MxN): ");
            Console.Write("M = ");
            m = int.Parse(Console.ReadLine());

            while (m <= 1 || m >= 7)
            {
                Console.WriteLine("Error: M must be between 2 and 6. Try again...");
                Console.Write("M = ");
                m = int.Parse(Console.ReadLine());
            }

            return m;
        }

        /* Function: getN
        *      Params: none
        *      Use: get the N value in (MxN) for the board size. 
        *           I was going to make it dynamic, but decided to not do this
        *      Return: integer
        */
        static int getN()
        {
            int n;

            Console.Write("N = ");
            n = int.Parse(Console.ReadLine());

            while (n <= 1 || n >= 7)
            {
                Console.WriteLine("Error: N must be between 2 and 6. Try again...");
                Console.Write("n = ");
                n = int.Parse(Console.ReadLine());
            }
            return n;
        }

        /* Function: testingFunction 
         *      Params: none
         *      Use: test a bunch of preset board states and display their results
         *      Return: none
        */
        static void testingFunction()
        {
            string testingInput = "5*2143786";
            string goal = "12345678*";
            char[] startBoard = testingInput.ToCharArray();
            char[] goalBoard = goal.ToCharArray();

            Game game = new Game(startBoard, goalBoard);
            Console.WriteLine("Test 1" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "1234*5786";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 2" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "123745*86";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 3" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "12348*765";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 4" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "41372658*";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 5" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "16253*478";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 6" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "51263*478";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 7" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "12635*478";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 8" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "356148*72";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 9" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "436871*52";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 10" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "3*2651478";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 11" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "*12345678";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 12" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "5*3284671";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 13" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "87432*651";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 14" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "876543*21";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 15" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

            testingInput = "87654321*";
            startBoard = testingInput.ToCharArray();
            goalBoard = goal.ToCharArray();
            game = new Game(startBoard, goalBoard);
            Console.WriteLine("\nTest 16" + "\nStartString: " + testingInput + "\nGoalString: " + goal);
            game.autoSolveNoBoardDisplay();

        }

    }
}
