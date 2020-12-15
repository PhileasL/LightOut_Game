using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board;
using Scripts.Coord;
using Scripts.Rules;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts
{
    public class LightOut : MonoBehaviour
    {
        public Text actionRemainingObject;

        public Button clueButton;

        private List<Piece.Piece> board, goal;

        private Cartesian lastCoordHits, lastCoordAbove;

        private bool aboveVoid = false;

        private protected Rules.Rules rules;

        private protected Actions.Actions actions;

        private int actionsRemaining;

        private List<Cartesian> solutionRemaining, hitsDone;

        // Start is called before the first frame update
        void Start()
        {
            PauseMenu.finished = false;
            lastCoordHits = lastCoordAbove = new Cartesian(0, 0);
            rules = new Rules.Rules();
            actions = rules.actions;
            InitGameSession();
        }

        public void InitGameSession(bool retry = false)
        {
            board = rules.board;
            goal = rules.goal;
            actionsRemaining = rules.difficulty;
            solutionRemaining = rules.solution;
            UpdateActionRemainingText();
            SetCamera();
            StartCoroutine(ShowClue());
            if (retry)
            {
                PauseMenu.failed = false;
                foreach (Cartesian hit in hitsDone)
                {
                    List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(hit);
                    board = actions.ChangeNeighourStates(coordsNeighours, board);
                }
                hitsDone.Clear();
            } 
            else
            {
                hitsDone = new List<Cartesian>();
            }
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
                    //Debug.Log(coordHits.String());
                    Piece.Piece pieceHits = actions.GetPieceHits(coordHits, board);
                    List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(pieceHits.coord);
                    board = actions.ChangeNeighourStates(coordsNeighours, board);
                    actionsRemaining--;
                    UpdateActionRemainingText();
                    if (solutionRemaining.Contains(coordHits))
                    {
                        Debug.Log("in solution");
                        solutionRemaining.Remove(coordHits);
                    }
                    hitsDone.Add(coordHits);
                    if (rules.checkForEndGame(board))
                    {
                        Debug.Log("END!!!");
                        PauseMenu.finished = true;
                    }
                    else if (actionsRemaining == 0)
                    {
                        PauseMenu.failed = true;
                    }
                }
            }

            if (coordHits != null && !coordHits.Equals(lastCoordAbove))
            {
                aboveVoid = false;
                //Debug.Log(coordHits.String());
                lastCoordAbove = coordHits;
                Piece.Piece pieceAbove = actions.GetPieceHits(coordHits, board);
                List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(pieceAbove.coord);
                actions.ChangeNeighourHighlight(coordsNeighours, board);
                actions.ChangeNeighourHighlight(coordsNeighours, goal);
            }

            if (coordHits == null && !aboveVoid)
            {
                aboveVoid = true;
                Cartesian nullCoord = new Cartesian(0, 0);
                actions.ChangeNeighourHighlight(new List<Cartesian>() { nullCoord }, board);
                actions.ChangeNeighourHighlight(new List<Cartesian>() { nullCoord }, goal);
                lastCoordAbove = nullCoord;
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

        private void SetCamera()
        {
            GameObject.Find("Main Camera").transform.position = new Vector3((float)(rules.size)/2, (float)(rules.size /1.3), rules.size+1);
        }

        public void ClickButtonClue()
        {
            //Debug.Log(rules.solution[-1].String());
            clueButton.gameObject.SetActive(false);
            actions.ChangeNeighourHighlight(new List<Cartesian>() { solutionRemaining[0] }, board);
            StartCoroutine(ShowClue());
        }

        private void UpdateActionRemainingText()
        {
            actionRemainingObject.text = actionsRemaining.ToString();
        }

        IEnumerator ShowClue()
        {
            yield return new WaitForSeconds(Rules.UnityParams.clueApparitionTemporisation);
            clueButton.gameObject.SetActive(true);
        }
    }
}

