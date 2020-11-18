using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Coord;

namespace Scripts.Piece.State
{
    public class PieceState
    {
        /// <summary>
        /// State of a Piece
        /// </summary>
        public int state;


        /// <summary>
        /// Cartesian coordinates of the piece
        /// </summary>
        public Cartesian coord;


        /// <summary>
        /// Constructor initialise an instance with a int state s and Cartesian coord
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        public PieceState(int state, Cartesian coord)
        {
            this.state = state;
            this.coord = coord;
        }

    }
}

