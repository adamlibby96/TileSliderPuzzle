using System;
using System.Diagnostics;
using System.IO;

namespace TileSliderPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            // dynamic stuff
            //int m = getM();
            //int n = getN();

            // undergrad stuff
            int m = 3;
            int n = 3;

            bool customWatch = false;
            int waitTime = 1000; // default 1 second
            Console.WriteLine("Do you want to watch it being solved? (y/n)");
            char[] watchIt = Console.ReadLine().ToCharArray();
            if (watchIt[0] == 'y' || watchIt[0] == 'Y')
            {
                Console.WriteLine("Enter the time between moves. (in milliseconds)");
                waitTime = int.Parse(Console.ReadLine());
                customWatch = true;
            }


            char[,] board = new char[m, n];

                ////testing input
                //string testingInput = "5*2143786";
                //string goal = "12345678*";
                //char[] startBoard = testingInput.ToCharArray();
                //char[] goalBoard = goal.ToCharArray();
            

                //user input
                string currentBoardInput = getInput();
                string goalInput = getGoalBoard();
                char[] startBoard = currentBoardInput.ToCharArray();
                char[] goalBoard = goalInput.ToCharArray();
           
            Game game = new Game(startBoard, goalBoard);

            game.displayStart();

            game.displayGoal();

            Stopwatch stopwatch = new Stopwatch();
            if (customWatch)
            {
                System.Threading.Thread.Sleep(waitTime);
                Console.Clear();
                stopwatch.Start();
                game.autoSolveWithWatching(waitTime);
                stopwatch.Stop();
            }
            else
            {
                stopwatch.Start();
                game.autoSolveNoBoardDisplay();
                stopwatch.Stop();
            }
            Console.WriteLine("\nCompleted in " + stopwatch.ElapsedMilliseconds + " milliseconds!");

            //game.AutoSolve();

            //game.ManualSolve();

            // pause the program to make sure it doesnt close
            Console.ReadKey();
        }

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
    }
}
