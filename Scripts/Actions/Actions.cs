using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board;
using Scripts.Coord;
using Scripts.Rules;

namespace Scripts.Actions
{
    public class Actions
    {
        private protected Rules.Rules rules;

        public Actions(Rules.Rules rules) 
        {
            this.rules = rules;
        }
        
        public Piece.Piece GetPieceHits(Cartesian coord, List<Piece.Piece> pieces)
        {
            foreach (Piece.Piece piece in pieces)
            {
                if (piece.coord.Equals(coord))
                {
                    Debug.Log(piece.name);
                    return piece;
                }
            }
            return null;
        }

        public List<Cartesian> GetNeighbourCoords(Cartesian coords)
        {
            List<Cartesian> neighbour = new List<Cartesian>();
            float distFromHit;
            if (Rules.Rules.neighbour % 2 != 0) distFromHit = Rules.Rules.neighbour / 2 + 1;
            else distFromHit = (Rules.Rules.neighbour / 2 + 1) * (float)System.Math.Sqrt(2);
            //Debug.Log("distFromHit: " + distFromHit.ToString());
            for (int i = (int)(coords.Coord(0) - (Rules.Rules.neighbour + 1) / 2); i <= (int)(coords.Coord(0) + (Rules.Rules.neighbour + 1) / 2); i++)
            {
                for (int j = (int)(coords.Coord(1) - (Rules.Rules.neighbour + 1) / 2); j <= (int)(coords.Coord(1) + (Rules.Rules.neighbour + 1) / 2); j++)
                {
                    Cartesian tmp = new Cartesian((float)i, (float)j);
                    if (Cartesian.DistanceBetween2Coords(coords, tmp) <= distFromHit)
                    {
                        //Debug.Log("x: " + i.ToString() + " y: " + j.ToString() + " dist: " + Cartesian.DistanceBetween2Coords(coords, new Cartesian(i, j)).ToString());
                        neighbour.Add(tmp);
                    }
                }
            }
            return neighbour;
        }

        public List<Piece.Piece> ChangeNeighourStates(List<Cartesian> coords, List<Piece.Piece> pieces)
        {
            foreach (Piece.Piece piece in pieces)
            {
                if (coords.Contains(piece.coord))
                {
                    //Debug.Log("yes: " + coords.Count.ToString());
                    piece.ChangeState();
                }
            }
            return pieces;
        }

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

