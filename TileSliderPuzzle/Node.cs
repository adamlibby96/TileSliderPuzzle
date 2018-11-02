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
            currentPosition.x = x;
            currentPosition.y = y;
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

        public Node[] getNeighbors()
        {
            // not implemented yet
            return null;
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
