using System;
using System.Collections.Generic;
using System.Text;

namespace TileSliderPuzzle
{
    public class Node
    {
        Point currentPosition;
        Point goalPosition;
        int distance;
        int value;
        bool isFinished;
        Dictionary<Moves, Node> neighbors;

        public Node()
        {
            isFinished = false;
            currentPosition = new Point();
            goalPosition = new Point();
            distance = -999;
            value = -999;
            neighbors = new Dictionary<Moves, Node>();
        }

        public Node(Node node)
        {
            isFinished = node.isFinished;
            currentPosition = node.currentPosition;
            goalPosition = node.goalPosition;
            distance = node.distance;
            value = node.value;
            neighbors = node.neighbors;
        }
#region gettersAndSetters
        public void setPosition(Point pos)
        {
            currentPosition = pos;
        }

        public void setPosition(int x, int y)
        {
            currentPosition.x = x;
            currentPosition.y = y;
        }

        public void setGoalPosition(Point pos)
        {
            currentPosition = pos;
        }

        public void setGoalPosition(int x, int y)
        {
            goalPosition.x = x;
            goalPosition.y = y;
        }

        public Point getCurrentPosition()
        {
            return currentPosition;
        }

        public Point getGoalPosition()
        {
            return goalPosition;
        }

        public void setValue(int val)
        {
            value = val;
        }

        public int getValue()
        {
            return value;
        }
        #endregion

        public Dictionary<Moves, Node> GetNeighbors(List<Node> board)
        {
            neighbors = new Dictionary<Moves, Node>();

            Point right = new Point();
            Point up = new Point();
            Point down = new Point();
            Point left = new Point();

            right.x = currentPosition.x + 1;
            right.y = currentPosition.y;

            up.x = currentPosition.x;
            up.y = currentPosition.y - 1;

            down.x = currentPosition.x;
            down.y = currentPosition.y + 1;

            left.x = currentPosition.x - 1;
            left.y = currentPosition.y;

            //Console.WriteLine("Value: " + value);
            //Console.WriteLine("Left: " + left.x + ", " + left.y);
            //Console.WriteLine("Up: " + up.x + ", " + up.y);
            //Console.WriteLine("Right: " + right.x + ", " + right.y);
            //Console.WriteLine("Down: " + down.x + ", " + down.y + "\n");


            foreach (Node n in board)
            {
                if (n.currentPosition == right)
                {
                    neighbors.Add(Moves.Right, n);
                } else if (n.currentPosition == left)
                {
                    neighbors.Add(Moves.Left, n);
                } else if (n.currentPosition == up)
                {
                    neighbors.Add(Moves.Up, n);
                } else if (n.currentPosition == down)
                {
                    neighbors.Add(Moves.Down, n);
                }
            }

            return neighbors;
        }

        /******************************************* 
        * Function Name: getDistanceToGoal         *
        * Parameters: Point goal                   *
        * Description:                             *
        *      this calculates the distance of     *
        *      the current node's point to the     *
        *      goal's point. If it is 0, it also   *
        *      sets the bool isFinished to true    *
        *      because it is in the correct        *
        *      position                            *
        ********************************************/
        public int getDistanceToGoal()
        {
            int dist = (currentPosition.x - goalPosition.x) + (currentPosition.y - goalPosition.y);
            if (dist == 0)
            {
                isFinished = true;
            } else
            {
                isFinished = false;
            }
            distance = dist;
            return dist;
        }

        public bool isAtGoalPosition()
        {
            return isFinished;
        }
    }
}
