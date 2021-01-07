using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Coord
{
    /// <summary>
    /// Cartesian class is the representation of a Cartesian space
    /// </summary>
    public class Cartesian
    {
        /// <summary>
        /// Coordinates of a Cartesian instance
        /// </summary>
        public float x, y;

        /// <summary>
        /// Cartesian is a constructor of a cartesian system coordinate
        /// </summary>
        /// <param name="xPos"> float x </param>
        /// <param name="yPos"> float y </param>
        public Cartesian(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Coord is a function that returns x or y depending on the given argument
        /// </summary>
        /// <param name="c"> int 0->x ; 1->y </param>
        /// <returns>x or y coordinate, -1 if c isn't valid</returns>
        public float Coord(int c)
        {
            if (c == 0) { return x; }
            else if (c == 1) { return y; }
            else { return -1; }
        }

        /// <summary>
        /// String is the function that return a definition of the Cartesian object 
        /// </summary>
        /// <returns> "x: value y: value" </returns>
        public string String()
        {
            return "x: " + x.ToString() + " y: " + y.ToString();
        }

        /// <summary>
        /// Equals that override the function is used if we are considering the .Contains method of a List object
        /// </summary>
        /// <param name="obj"> object </param>
        /// <returns> bool </returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Cartesian objAsPart = obj as Cartesian;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        /// <summary>
        /// Equals is a function that compare x and y of two Cartesian object
        /// </summary>
        /// <param name="other"> Cartesian </param>
        /// <returns> bool isEqual </returns>
        public bool Equals(Cartesian other)
        {
            return (other.x == x && other.y == y);
        }

        /// <summary>
        /// GetHashCode that override the function is used if we are considering the .Contains method of a List object
        /// </summary>
        /// <returns> int hash code </returns>
        public override int GetHashCode()
        {
            return (int)(x + 20*y);
        }

        /// <summary>
        /// DistanceBetween2Coords static function returns the cartesian distance between two points
        /// </summary>
        /// <param name="coord1"> Cartesian </param>
        /// <param name="coord2"> Cartesian </param>
        /// <returns> int distance between 2 Cartesian objects </returns>
        public static float DistanceBetween2Coords(Cartesian coord1, Cartesian coord2)
        {
            return (float)System.Math.Sqrt((coord2.Coord(0) - coord1.Coord(0)) * (coord2.Coord(0) - coord1.Coord(0))
                + (coord2.Coord(1) - coord1.Coord(1)) * (coord2.Coord(1) - coord1.Coord(1)));
        }
    }
}

