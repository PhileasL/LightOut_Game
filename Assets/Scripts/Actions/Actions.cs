using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board;
using Scripts.Coord;
using Scripts.Rules;

namespace Scripts.Actions
{

    /// <summary>
    /// Actions class contains all the different possible actions in the game
    /// </summary>
    public class Actions
    {
        /// <summary>
        /// rules is an instance of Rules containing the parameters of the game
        /// </summary>
        private protected Rules.Rules rules;

        /// <summary>
        /// Constructor of Actions class
        /// </summary>
        /// <param name="rules">Rule instance</param>
        public Actions(Rules.Rules rules) 
        {
            this.rules = rules;
        }

        /// <summary>
        /// GetPieceHits function send back the Piece hits in the given list of Piece at the given Cartesian coordinates
        /// </summary>
        /// <param name="coord">Cartesian hits</param>
        /// <param name="pieces">List of game's Piece</param>
        /// <returns>Piece hits by the Cartesian coordinates</returns>
        public Piece.Piece GetPieceHits(Cartesian coord, List<Piece.Piece> pieces)
        {
            foreach (Piece.Piece piece in pieces)
            {
                if (piece.coord.Equals(coord))
                {
                    return piece;
                }
            }
            return null;
        }

        /// <summary>
        /// GetNeighbourCoords send back a list of neighbour Piece around the given Cartesian coordinate
        /// uses the neighbour parameter in Rules instance given in the constructor
        /// </summary>
        /// <param name="coords">Cartesian center of neighbours to find</param>
        /// <returns>List of Cartesian representing the neighbours' coordinates</returns>
        public List<Cartesian> GetNeighbourCoords(Cartesian coords)
        {
            List<Cartesian> neighbour = new List<Cartesian>();
            float distFromHit;
            if (rules.neighbour % 2 != 0) distFromHit = rules.neighbour / 2 + 1;
            else distFromHit = (rules.neighbour / 2 + 1) * (float)System.Math.Sqrt(2);
            for (int i = (int)(coords.Coord(0) - (rules.neighbour + 1) / 2); i <= (int)(coords.Coord(0) + (rules.neighbour + 1) / 2); i++)
            {
                for (int j = (int)(coords.Coord(1) - (rules.neighbour + 1) / 2); j <= (int)(coords.Coord(1) + (rules.neighbour + 1) / 2); j++)
                {
                    Cartesian tmp = new Cartesian((float)i, (float)j);
                    if (Cartesian.DistanceBetween2Coords(coords, tmp) <= distFromHit)
                    {
                        neighbour.Add(tmp);
                    }
                }
            }
            return neighbour;
        }

        /// <summary>
        /// ChangeNeighourStates changes the state of the given list of Cartesian in the given List of Piece
        /// </summary>
        /// <param name="coords">List of Cartesian neighbours</param>
        /// <param name="pieces">List of game's Piece</param>
        /// <returns>List of updated game's Piece</returns>
        public List<Piece.Piece> ChangeNeighourStates(List<Cartesian> coords, List<Piece.Piece> pieces)
        {
            foreach (Piece.Piece piece in pieces)
            {
                if (coords.Contains(piece.coord))
                {
                    piece.ChangeState();
                }
            }
            return pieces;
        }

        /// <summary>
        /// ChangeNeighourHighlight changes the highlights of the Pieces at the given Cartesian coordinates
        /// </summary>
        /// <param name="coords">List of Cartesian to highlight</param>
        /// <param name="pieces">List of game's Piece</param>
        public void ChangeNeighourHighlight(List<Cartesian> coords, List<Piece.Piece> pieces)
        {
            foreach (Piece.Piece piece in pieces)
            {
                if (coords.Contains(piece.coord)) piece.ApplyHighlight(true);
                else piece.ApplyHighlight(false);
            }
        }
    }
}

