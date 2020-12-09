using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board.Plane;
using Scripts.Rules;
using Scripts.Piece;
using Scripts.Coord;
using System;
//InstanciedOnce.Parameters.LightDetected ? "La lumière a été détectée aujourdhui." : "La lumière n'a  pas encore été détectée aujourdhui."
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
        protected int size;

        /// <summary>
        /// List of Pieces contain all the pieces of the game
        /// </summary>
        public List<Piece.Piece> pieces = new List<Piece.Piece>();

        private bool isGoal;

        /// <summary>
        /// Constructor of the Board
        /// </summary>
        public Board(bool isGoal, int size)
        {
            this.isGoal = isGoal;
            this.size = size;
            CreatePlanes();
            CreatePieces();
        }

        /// <summary>
        /// CreatePlane create the Board object
        /// puts LightOut to its parent
        /// and create the plane object
        /// </summary>
        private void CreatePlanes()
        {
            GameObject boardObject = new GameObject(isGoal ? Rules.UnityParams.goal.boardName : Rules.UnityParams.game.boardName);
            boardObject.transform.parent = GameObject.Find(Rules.UnityParams.gameName).transform;
            plane = boardObject.AddComponent<Plane.Plane>();
            plane.SetPosition(isGoal, size);
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
                    GameObject pieceObject = new GameObject(isGoal ? Rules.UnityParams.goal.CreatePieceName(i, j) 
                        : Rules.UnityParams.game.CreatePieceName(i,j));
                    pieceObject.transform.parent = GameObject.Find(isGoal ? Rules.UnityParams.goal.boardHierarchy 
                        : Rules.UnityParams.game.boardHierarchy).transform;
                    Piece.Piece piece = pieceObject.AddComponent<Piece.Piece>();
                    piece.CreatePieceAt(new Cartesian(i, j), isGoal, size);
                    pieces.Add(piece);
                }
            }
        }
    }
}

