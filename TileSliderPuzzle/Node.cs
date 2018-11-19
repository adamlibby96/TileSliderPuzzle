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
    public class Node
    {
        // current position of the node
        Point currentPosition;
        // the goal position of the node 
        Point goalPosition;
        // distance to the goal position 
        int distance;
        // value in the node (i.e. 1 or 2 or 3...)
        int value;
        // the cost to move for this node (i.e. it would be higher if the node is in the goal position 
        public int costToMove { get; set; }
        // if the node is in the goal position
        bool isFinished;
        // the surrounding nodes of this current node 
        Dictionary<Moves, Node> neighbors;

        /* Function: Node constructor (default)
         *      Params: none
         *      Use: init node to default values
         *      Return: none
        */
        public Node()
        {
            isFinished = false;
            currentPosition = new Point();
            goalPosition = new Point();
            distance = -999;
            value = -999;
            neighbors = new Dictionary<Moves, Node>();
        }

        /* Function: Node constructor
         *      Params: Node
         *      Use: copy the passed node information into this node
         *      Return: none
        */
        public Node(Node node)
        {
            isFinished = node.isFinished;
            currentPosition = node.currentPosition;
            goalPosition = node.goalPosition;
            distance = node.distance;
            value = node.value;
            neighbors = node.neighbors;
        }
        // getters and setters, no need for explanation 
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

        public int getCostToMove()
        {
            getDistanceToGoal();
            return costToMove;
        }
        #endregion

        /* Function: GetNeighbors
         *      Params: board
         *      Use: get the left, up, down, right neighbors of this node
         *      Return: Dictonary 
        */
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

        /* Function: getDistanceToGoal
         *      Params: none
         *      Use: calculate the distance to the goal from current position
         *      Return: int
        */
        public int getDistanceToGoal()
        {
            int dist = Math.Abs((currentPosition.x - goalPosition.x)) + Math.Abs((currentPosition.y - goalPosition.y));
            if (dist == 0)
            {
                // if we are finished, the cost is increased 
                costToMove = 10;
                isFinished = true;
            } else
            {
                costToMove = 1;
                isFinished = false;
            }
            distance = dist;
            return dist;
        }

        /* Function: isAtGoalPosition
         *      Params: none
         *      Use: get the bool if we are at the current position or not
         *      Return: bool
        */
        public bool isAtGoalPosition()
        {
            return currentPosition == goalPosition;
        }

        /* Function: ToString override
         *      Params: none
         *      Use: string representation of the node
         *              value: currentPosition, GoalPosition
         *      Return: string
        */
        public override string ToString()
        {
            return this.value + ": at " + currentPosition.ToString() + ";\nGoal: " + goalPosition.ToString() + "\n";
        }
    }
}
