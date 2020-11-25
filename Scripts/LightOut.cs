using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board;
using Scripts.Coord;

namespace Scripts
{
    public class LightOut : MonoBehaviour
    {
        private Board.Board board;
        // Start is called before the first frame update
        void Start()
        {
            board = new Board.Board();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                // Casts the ray and get the first game object hit
                Physics.Raycast(ray, out hit);
                if (hit.point.x != 0.0 && hit.point.z != 0.0){
                    Cartesian coordHits = new Cartesian((int)hit.point.x + 1, (int)hit.point.z + 1);
                    Debug.Log(coordHits.String());
                    foreach (Piece.Piece piece in board.pieces)
                    {
                        if (piece.coord.Equals(coordHits)) { Debug.Log(piece.name); }
                    }
                }
            }
        }
    }
}

