using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board.Plane;
using Scripts.Rules;
using Scripts.Piece;
using Scripts.Coord;

namespace Scripts.Board
{
    public class Board
    {
        /// <summary>
        /// plane is an instance of Plane, representing the board under the pieces
        /// </summary>
        public Plane.Plane plane;


        public Dictionary<Cartesian, Piece.Piece> pieces = new Dictionary<Cartesian, Piece.Piece>();

        /// <summary>
        /// size represents the number of piece on one line of the board
        /// </summary>
        protected int size = Rules.Rules.size;


        public Board()
        {
            CreatePlane();
            CreatePieces();
        }

        private void CreatePlane()
        {
            GameObject boardObject = new GameObject("Board");
            boardObject.transform.parent = GameObject.Find("LightOut").transform;
            plane = boardObject.AddComponent<Plane.Plane>();
        }

        private void CreatePieces()
        {
            for (int i = 1; i <= size; i++)
            {
                GameObject pieceObject = new GameObject("Piece");
                //pieceObject.transform.parent = GameObject.Find("LightOut/Board/Plane(Clone)").transform;
                //plane = boardObject.AddComponent<Plane.Plane>();
            }
        }
    }
}

