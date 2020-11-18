using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Coord
{
    public class Cartesian
    {

        /// <summary>
        /// Coordinates of a Cartesian instance
        /// </summary>
        public float x, y;

        /// <summary>
        /// Cartesian is a constructor of a cartesian system coordinate
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        public Cartesian(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Coord is a function that returns x or y depending on the given argument
        /// </summary>
        /// <param name="c"></param>
        /// <returns>x or y coordinate, -1 if c isn't valid</returns>
        public float Coord(int c)
        {
            if (c == 0) { return x; }
            else if (c == 1) { return y; }
            else { return -1; }
        }
    }
}

