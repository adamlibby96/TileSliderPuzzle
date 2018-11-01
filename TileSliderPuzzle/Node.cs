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

        public Node()
        {
            isFinished = false;
            currentPosition = new Point();
            goalPosition = new Point();
            distance = -999;
            value = -999;
        }

        

        public void updatePosition(int x, int y)
        {
            currentPosition.x = x;
            currentPosition.y = y;
        }

        public void updatePosition(Point position)
        {
            this.currentPosition = position;
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
