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

        protected Rules.Rules rules;

        protected Actions.Actions actions;

        // Start is called before the first frame update
        void Start()
        {
            board = new Board.Board(false);
            goalBoard = new Board.Board(true);
            lastCoordHits = lastCoordAbove = new Cartesian(0, 0);
            rules = new Rules.Rules(goalBoard.pieces, board.pieces);
            board.pieces = rules.board;
            actions = new Actions.Actions(rules);
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
                    Piece.Piece pieceHits = actions.GetPieceHits(coordHits, board.pieces);
                    List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(pieceHits.coord);
                    board.pieces = actions.ChangeNeighourStates(coordsNeighours, board.pieces);
                    if (rules.checkForEndGame(board.pieces)) Debug.Log("END!!!");
                }
            }

            if (coordHits != null && !coordHits.Equals(lastCoordAbove))
            {
                aboveVoid = false;
                Debug.Log(coordHits.String());
                lastCoordAbove = coordHits;
                Piece.Piece pieceAbove = actions.GetPieceHits(coordHits, board.pieces);
                List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(pieceAbove.coord);
                actions.ChangeNeighourHighlight(coordsNeighours, board.pieces);
            }

            if (coordHits == null && !aboveVoid)
            {
                aboveVoid = true;
                actions.ChangeNeighourHighlight(new List<Cartesian>() { new Cartesian(0, 0) }, board.pieces);
            }

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
    }
}

