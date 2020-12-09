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

        private Cartesian lastCoordAbove;

        private bool aboveVoid = false;

        // Start is called before the first frame update
        void Start()
        {
            board = new Board.Board(false);
            goalBoard = new Board.Board(true);
            lastCoordHits = lastCoordAbove = new Cartesian(0, 0);
            Rules.Rules test = new Rules.Rules(goalBoard.pieces, board.pieces);
            board.pieces = test.board;
            setupTheGame();
        }

        // Update is called once per frame
        void Update()
        {
            Cartesian coordHits = GetCoordHits();
            
            if (Input.GetMouseButton(0))
            {
                if (coordHits != null && !coordHits.Equals(lastCoordHits))
                {
                    aboveVoid = false;
                    lastCoordHits = coordHits;
                    lastCoordAbove = new Cartesian(0, 0);
                    Debug.Log(coordHits.String());
                    Piece.Piece pieceHits = GetPieceHits(coordHits);
                    List<Cartesian> coordsNeighours = GetNeighbourCoords(pieceHits.coord);
                    ChangeNeighourStates(coordsNeighours);
                }
            }

            if (coordHits != null && !coordHits.Equals(lastCoordAbove))
            {
                aboveVoid = false;
                Debug.Log(coordHits.String());
                lastCoordAbove = coordHits;
                Piece.Piece pieceAbove = GetPieceHits(coordHits);
                List<Cartesian> coordsNeighours = GetNeighbourCoords(pieceAbove.coord);
                ChangeNeighourHighlight(coordsNeighours);
            }

            if (coordHits == null && !aboveVoid)
            {
                aboveVoid = true;
                ChangeNeighourHighlight(new List<Cartesian>() { new Cartesian(0, 0) });
            }

        }

        private void setupTheGame()
        {
            List<Cartesian> alreadyDone = new List<Cartesian>();
            System.Random rnd = new System.Random();
            for (int i = 0; i < Rules.Rules.difficulty; i++)
            {
                Cartesian randomStep = new Cartesian(rnd.Next(1, Rules.Rules.size + 1), rnd.Next(1, Rules.Rules.size + 1));
                while (alreadyDone.Contains(randomStep))
                {
                    Debug.Log("doublons " + randomStep.String());
                    randomStep = new Cartesian(rnd.Next(1, Rules.Rules.size + 1), rnd.Next(1, Rules.Rules.size + 1));
                }
                alreadyDone.Add(randomStep);
                Debug.Log(randomStep.String());
                ChangeNeighourStates(GetNeighbourCoords(randomStep));
            }
        }

        private void ChangeNeighourStates(List<Cartesian> coords)
        {
            foreach (Piece.Piece piece in board.pieces)
            {
                if (coords.Contains(piece.coord))
                {
                    //Debug.Log("yes: " + coords.Count.ToString());
                    piece.ChangeState();
                }
            }
        }


        private void ChangeNeighourHighlight(List<Cartesian> coords)
        {
            foreach (Piece.Piece piece in board.pieces)
            {
                if (coords.Contains(piece.coord)) piece.ApplyHighlight(true);
                else piece.ApplyHighlight(false);
            }
        }

        private List<Cartesian> GetNeighbourCoords(Cartesian coords)
        {
            List<Cartesian> neighbour = new List<Cartesian>();
            float distFromHit;
            if (Rules.Rules.neighbour % 2 != 0) distFromHit = Rules.Rules.neighbour/2+1;
            else distFromHit = (Rules.Rules.neighbour/2 + 1) * (float)System.Math.Sqrt(2);
            //Debug.Log("distFromHit: " + distFromHit.ToString());
            for (int i = (int)(coords.Coord(0) - (Rules.Rules.neighbour +1)/2); i <= (int)(coords.Coord(0) + (Rules.Rules.neighbour + 1) / 2); i++)
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

        private Cartesian GetCoordHits()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            if (hit.point.x != 0.0 && hit.point.z != 0.0 && hit.point.z <= Rules.Rules.size && hit.point.x <= Rules.Rules.size)
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
                    return piece;
                }
            }
            return null;
        }
    }
}

