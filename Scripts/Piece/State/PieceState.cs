using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Rules;

namespace Scripts.Piece.State
{
    public class PieceState
    {
        /// <summary>
        /// State of a Piece
        /// </summary>
        public int state;


        /// <summary>
        /// Constructor initialise an instance with a int state s and Cartesian coord
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        public PieceState()
        {
            this.state = Rules.UnityParams.offState;
        }

    }
}

