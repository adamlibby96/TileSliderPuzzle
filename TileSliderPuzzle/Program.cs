using System;
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


            char[,] board = new char[m, n];

            //string currentBoardInput = getInput(); dont delete!!!!!!!!!!! this is the real input method
            //string goalInput = getGoalBoard(); 
            string testingInput = "5*2143786";
            string goal = "12345678*";
            char[] startBoard = testingInput.ToCharArray();
            char[] goalBoard = goal.ToCharArray();
            Game game = new Game(startBoard, goalBoard);
            game.displayStart();
            game.displayGoal();
            
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
            Console.Write("\nMiddle Row: ");
            input += Console.ReadLine();
            Console.Write("\nBottom Row: ");
            input += Console.ReadLine();

            return input;
        }

        static string getGoalBoard()
        {
            string input = "\n";

            Console.WriteLine("Enter goal board: ");
            Console.Write("Top Row: ");
            input += Console.ReadLine();
            Console.Write("\nMiddle Row: ");
            input += Console.ReadLine();
            Console.Write("\nBottom Row: ");
            input += Console.ReadLine();

            return input;
        }
    }
}
