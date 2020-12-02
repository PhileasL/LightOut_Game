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

        public string String()
        {
            return "x: " + x.ToString() + " y: " + y.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Cartesian objAsPart = obj as Cartesian;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Cartesian other)
        {
            return (other.x == x && other.y == y);
        }

        public override int GetHashCode()
        {
            return (int)(x + Rules.Rules.size*y);
        }

        public static float DistanceBetween2Coords(Cartesian coord1, Cartesian coord2)
        {
            return (float)System.Math.Sqrt((coord2.Coord(0) - coord1.Coord(0)) * (coord2.Coord(0) - coord1.Coord(0))
                + (coord2.Coord(1) - coord1.Coord(1)) * (coord2.Coord(1) - coord1.Coord(1)));
        }
    }
}

