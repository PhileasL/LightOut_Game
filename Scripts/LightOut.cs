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
        private List<Piece.Piece> board;

        private Cartesian lastCoordHits;

        private Cartesian lastCoordAbove;

        private bool aboveVoid = false;

        private protected Rules.Rules rules;

        private protected Actions.Actions actions;

        // Start is called before the first frame update
        void Start()
        {
            lastCoordHits = lastCoordAbove = new Cartesian(0, 0);
            rules = new Rules.Rules();
            board = rules.board;
            actions = rules.actions;
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
                    Piece.Piece pieceHits = actions.GetPieceHits(coordHits, board);
                    List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(pieceHits.coord);
                    board = actions.ChangeNeighourStates(coordsNeighours, board);
                    if (rules.checkForEndGame(board)) Debug.Log("END!!!");
                }
            }

            if (coordHits != null && !coordHits.Equals(lastCoordAbove))
            {
                aboveVoid = false;
                Debug.Log(coordHits.String());
                lastCoordAbove = coordHits;
                Piece.Piece pieceAbove = actions.GetPieceHits(coordHits, board);
                List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(pieceAbove.coord);
                actions.ChangeNeighourHighlight(coordsNeighours, board);
            }

            if (coordHits == null && !aboveVoid)
            {
                aboveVoid = true;
                actions.ChangeNeighourHighlight(new List<Cartesian>() { new Cartesian(0, 0) }, board);
            }

        }

        private Cartesian GetCoordHits()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            if (hit.point.x != 0.0 && hit.point.z != 0.0 && hit.point.z <= rules.size && hit.point.x <= rules.size)
            {
                return new Cartesian((int)hit.point.x + 1, (int)hit.point.z + 1);
            }
            return null;
        }
    }
}

