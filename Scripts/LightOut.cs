using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board;
using Scripts.Coord;
using Scripts.Rules;

namespace Scripts
{
    public class LightOut : MonoBehaviour
    {
        private Board.Board board, goalBoard;

        private Cartesian lastCoordHits;
        // Start is called before the first frame update
        void Start()
        {
            board = new Board.Board(false);
            goalBoard = new Board.Board(true);
            lastCoordHits = new Cartesian(0, 0);
            Rules.Rules test = new Rules.Rules(goalBoard.pieces);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Cartesian coordHits = GetCoordHits();
                if (coordHits != null && !coordHits.Equals(lastCoordHits))
                {
                    lastCoordHits = coordHits;
                    Debug.Log(coordHits.String());
                    Piece.Piece pieceHits = GetPieceHits(coordHits);
                    pieceHits.ChangeState();
                    GetNeighbourCoords(pieceHits.coord);
                }
            }
        }
        
        private List<Cartesian> GetNeighbourCoords(Cartesian coords)
        {
            List<Cartesian> neighbour = new List<Cartesian>();
            neighbour.Add(coords);
            float distFromHit;
            if (Rules.Rules.neighbour % 2 != 0) distFromHit = Rules.Rules.neighbour/2+1;
            else distFromHit = (Rules.Rules.neighbour/2 + 1) * (float)System.Math.Sqrt(2);
            Debug.Log("distFromHit: " + distFromHit.ToString());
            for (int i = (int)(coords.Coord(0) - (Rules.Rules.neighbour +1)/2); i <= (int)(coords.Coord(0) + (Rules.Rules.neighbour + 1) / 2); i++)
            {
                for (int j = (int)(coords.Coord(1) - (Rules.Rules.neighbour + 1) / 2); j <= (int)(coords.Coord(1) + (Rules.Rules.neighbour + 1) / 2); j++)
                {
                    if (Cartesian.DistanceBetween2Coords(coords, new Cartesian(i,j)) <= distFromHit)
                    {
                        Debug.Log("x: " + i.ToString() + " y: " + j.ToString() + " dist: " + Cartesian.DistanceBetween2Coords(coords, new Cartesian(i, j)).ToString());
                        neighbour.Add(new Cartesian((float)i, (float)j));
                    }
                }
            }
            return neighbour;

        }

        private Cartesian GetCoordHits()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            if (hit.point.x != 0.0 && hit.point.z != 0.0)
            {
                return new Cartesian((int)hit.point.x + 1, (int)hit.point.z + 1);
            }
            return null;
        }

        private Piece.Piece GetPieceHits(Cartesian coord)
        {
            foreach (Piece.Piece piece in board.pieces)
            {
                if (piece.coord.Equals(coord))
                {
                    Debug.Log(piece.name);
                    piece.ApplyNewMaterial(Rules.UnityParams.onMaterialPath);
                    return piece;
                }
            }
            return null;
        }
    }
}

