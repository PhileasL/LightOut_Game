using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board.Plane;
using Scripts.Rules;
using Scripts.Piece;
using Scripts.Coord;
using System;

namespace Scripts.Board
{
    public class Board
    {
        /// <summary>
        /// plane is an instance of Plane, representing the board under the pieces
        /// </summary>
        public Plane.Plane plane;

        /// <summary>
        /// size represents the number of piece on one line of the board
        /// </summary>
        protected int size = Rules.Rules.size;

        /// <summary>
        /// List of Piece contains all the pieces of the game
        /// </summary>
        public List<Piece.Piece> pieces = new List<Piece.Piece>();

        /// <summary>
        /// Constructor of the Board
        /// </summary>
        public Board()
        {
            CreatePlane();
            CreatePieces();
        }

        /// <summary>
        /// CreatePlane create the Board object
        /// puts LightOut to its parent
        /// and create the plane object
        /// </summary>
        private void CreatePlane()
        {
            GameObject boardObject = new GameObject("Board");
            boardObject.transform.parent = GameObject.Find("LightOut").transform;
            plane = boardObject.AddComponent<Plane.Plane>();
        }

        /// <summary>
        /// CreatePieces creates all the pieces at right coordinates
        /// </summary>
        private void CreatePieces()
        {
            for (int i = 1; i <= size; i++)
            {
                for (int j = 1; j <= size; j++)
                {
                    GameObject pieceObject = new GameObject("Piece_" + i.ToString() + "_" + j.ToString());
                    pieceObject.transform.parent = GameObject.Find("LightOut/Board").transform;
                    Piece.Piece piece = pieceObject.AddComponent<Piece.Piece>();
                    piece.CreatePieceAt(new Cartesian(i, j));
                    pieces.Add(piece);
                }
            }
        }
    }
}

