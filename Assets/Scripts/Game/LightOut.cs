using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board;
using Scripts.Coord;
using Scripts.Rules;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scripts.Game
{
    /// <summary>
    /// LightOut class is the supervisor class of the game
    /// </summary>
    public class LightOut : MonoBehaviour
    {
        /// <summary>
        /// actionRemainingObject Text is binded to its object in editor hierarchy
        /// </summary>
        public Text actionRemainingObject;

        /// <summary>
        /// clueButton Button is binded to its object in editor hierarchy
        /// </summary>
        public Button clueButton;

        /// <summary>
        /// board List of Piece is the representation of the current game state
        /// goal List of Piece is the representation of the goal state
        /// </summary>
        private List<Piece.Piece> board, goal;

        /// <summary>
        /// lastCoordHits Cartesian represents the last Cartesian coordinates hit by the player
        /// lastCoordAbove Cartesian represents the last Cartesian coordinates the cursor of the player was above
        /// </summary>
        private Cartesian lastCoordHits, lastCoordAbove;

        /// <summary>
        /// aboveVoid bool represents the position of the cursor regarding to the board
        /// </summary>
        private bool aboveVoid = false;

        /// <summary>
        /// rules is the instance of this game's Rules
        /// </summary>
        private protected Rules.Rules rules;

        /// <summary>
        /// actions is an instance of Actions
        /// </summary>
        private protected Actions.Actions actions;

        /// <summary>
        /// actionsRemaining int represents the actions remaining by the player
        /// </summary>
        private int actionsRemaining;

        /// <summary>
        /// solutionRemaining List of Cartesian represents the actions remaining in the solution
        /// hitsDone List of Cartesian store the actions done by the player
        /// </summary>
        private List<Cartesian> solutionRemaining, hitsDone;

        /// <summary>
        /// Start is called before the first frame update
        /// Its role is to initialize the whole game scene
        /// It's binded to LightOut gameObject inthe editor hierarchy
        /// </summary>
        public void Start()
        {
            PauseMenu.finished = false;
            rules = new Rules.Rules();
            actions = rules.actions;
            InitGameSession();
        }

        /// <summary>
        /// InitGameSession function initialize class variables, visual elements and launch the game
        /// </summary>
        /// <param name="retry">bool</param>
        public void InitGameSession(bool retry = false)
        {
            lastCoordHits = lastCoordAbove = new Cartesian(0, 0);
            board = rules.board;
            goal = rules.goal;
            actionsRemaining = rules.difficulty;
            solutionRemaining = rules.solution;
            UpdateActionRemainingText();
            SetCamera();
            StartCoroutine(ShowClue());
            PauseMenu.failed = false;
            if (retry)
            {
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

        /// <summary>
        /// Update is called once per frame
        /// Checks the actions done by the player at each frame
        /// and calls functions depending on the player action on the game
        /// </summary>
        void Update()
        {
            Cartesian coordHits = GetCoordHits();
            
            if (Input.GetMouseButton(0))
            {
                if (coordHits != null && !coordHits.Equals(lastCoordHits))
                {
                    BoardHit(coordHits);
                }
            }

            if (coordHits != null && !coordHits.Equals(lastCoordAbove))
            {
                AboveBoard(coordHits);
            }

            if (coordHits == null && !aboveVoid)
            {
                AboveVoid();
            }

        }

        /// <summary>
        /// BoardHit function is called when the board gets hit
        /// changes the state of the Piece hit and its neighbour
        /// checks for end or fail
        /// </summary>
        /// <param name="coordHits">Cartesian hit</param>
        private void BoardHit(Cartesian coordHits)
        {
            aboveVoid = false;
            lastCoordHits = coordHits;
            lastCoordAbove = new Cartesian(0, 0);
            Piece.Piece pieceHits = actions.GetPieceHits(coordHits, board);
            List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(pieceHits.coord);
            board = actions.ChangeNeighourStates(coordsNeighours, board);
            actionsRemaining--;
            UpdateActionRemainingText();
            if (solutionRemaining.Contains(coordHits))
            {
                solutionRemaining.Remove(coordHits);
            }
            hitsDone.Add(coordHits);
            if (rules.CheckForEndGame(board))
            {
                Debug.Log("END!!!");
                PauseMenu.finished = true;
            }
            else if (actionsRemaining == 0)
            {
                PauseMenu.failed = true;
            }
        }

        /// <summary>
        /// AboveBoard hightlight the Piece and neighbour below the player cursor
        /// </summary>
        /// <param name="coordHits">Cartesian hit</param>
        private void AboveBoard(Cartesian coordHits)
        {
            aboveVoid = false;
            lastCoordAbove = coordHits;
            Piece.Piece pieceAbove = actions.GetPieceHits(coordHits, board);
            List<Cartesian> coordsNeighours = actions.GetNeighbourCoords(pieceAbove.coord);
            actions.ChangeNeighourHighlight(coordsNeighours, board);
            actions.ChangeNeighourHighlight(coordsNeighours, goal);
        }

        /// <summary>
        /// AboveVoid function unhighlight all the Piece of the board
        /// </summary>
        private void AboveVoid()
        {
            aboveVoid = true;
            Cartesian nullCoord = new Cartesian(0, 0);
            actions.ChangeNeighourHighlight(new List<Cartesian>() { nullCoord }, board);
            actions.ChangeNeighourHighlight(new List<Cartesian>() { nullCoord }, goal);
            lastCoordAbove = nullCoord;
        }

        /// <summary>
        /// GetCoordHits cast the ray of the cursor and extracts the coordinates hit by the ray
        /// </summary>
        /// <returns>Cartesian hit or null if not</returns>
        private Cartesian GetCoordHits()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.point.x != 0.0 && hit.point.z != 0.0 && hit.point.z <= rules.size && hit.point.x <= rules.size)
            {
                return new Cartesian((int)hit.point.x + 1, (int)hit.point.z + 1);
            }
            return null;
        }

        /// <summary>
        /// SetCamera sets the position of the camera in order to be able to see everything in the game
        /// </summary>
        private void SetCamera()
        {
            GameObject.Find("Main Camera").transform.position = new Vector3((float)(rules.size)/2, (float)(rules.size /1.3), rules.size+1);
        }

        /// <summary>
        /// ClickButtonClue called when clue button is hit
        /// highlights a Piece part of the solution and hid clue button
        /// </summary>
        public void ClickButtonClue()
        {
            clueButton.gameObject.SetActive(false);
            actions.ChangeNeighourHighlight(new List<Cartesian>() { solutionRemaining[0] }, board);
            StartCoroutine(ShowClue());
        }

        /// <summary>
        /// UpdateActionRemainingText update the text after an action done by the player
        /// </summary>
        private void UpdateActionRemainingText()
        {
            actionRemainingObject.text = actionsRemaining.ToString();
        }

        /// <summary>
        /// ShowClue is a coroutine waiting a certain amount of time before shows the clue button
        /// </summary>
        /// <returns>None</returns>
        IEnumerator ShowClue()
        {
            yield return new WaitForSeconds(Rules.UnityParams.clueApparitionTemporisation);
            clueButton.gameObject.SetActive(true);
        }
    }
}

