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
        private Board.Board board;

        private Cartesian lastCoordHits;
        // Start is called before the first frame update
        void Start()
        {
            board = new Board.Board();
            lastCoordHits = new Cartesian(0, 0);
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
                    GetPieceHits(coordHits);
                }
            }
            /*if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Casts the ray and get the first game object hit
                Physics.Raycast(ray, out hit);
                if (hit.point.x != 0.0 && hit.point.z != 0.0){
                    Cartesian coordHits = new Cartesian((int)hit.point.x + 1, (int)hit.point.z + 1);
                    if (!coordHits.Equals(lastCoordHits))
                    {
                        lastCoordHits = coordHits;
                        Debug.Log(coordHits.String());
                        foreach (Piece.Piece piece in board.pieces)
                        {
                            if (piece.coord.Equals(coordHits))
                            {
                                Debug.Log(piece.name);
                                piece.ApplyNewMaterial(Rules.UnityParams.onMaterialPath);
                            }
                        }
                    }
                }
            }*/
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

