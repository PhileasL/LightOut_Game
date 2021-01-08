using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Coord;
using Scripts.Board;
using UnityEngine.SceneManagement;

namespace Scripts.Rules
{
    public class Rules
    {
        /// <summary>
        /// size is the size of the board
        /// </summary>
        public int size = PlayerPrefs.GetInt("size");

        /// <summary>
        /// neighbour is the number of neighbour near the piece hits to take into consideration while clicing on the board
        /// </summary>
        public int neighbour = PlayerPrefs.GetInt("neighbour");

        /// <summary>
        /// difficulty is the number of action to do to complete the game
        /// </summary>
        public int difficulty = PlayerPrefs.GetInt("difficulty");

        /// <summary>
        /// goal, board are lists of Piece representing a state of a board
        /// </summary>
        public List<Piece.Piece> goal, board;

        /// <summary>
        /// actions is an instance of Actions
        /// </summary>
        public Actions.Actions actions;

        /// <summary>
        /// solution is the List of Cartesian to achieve a completion
        /// </summary>
        public List<Cartesian> solution;

        /// <summary>
        /// Rules constructor for the game, compute a goal and scramble another board
        /// </summary>
        public Rules()
        {
            this.goal = (new Board.Board(true, size)).pieces;
            this.board = (new Board.Board(false, size)).pieces;
            actions = new Actions.Actions(this);
            ComputeAGoal();
            ScrambleBoard();
        }

        /// <summary>
        /// Rules constructor for the preview
        /// </summary>
        /// <param name="size"> int size of the board </param>
        /// <param name="neighbour"> int neighbour </param>
        public Rules(int size, int neighbour)
        {
            this.size = size;
            this.neighbour = neighbour;
            this.board = (new Board.Board(false, size)).pieces;
            actions = new Actions.Actions(this);
            SetPreviewBoard();
            SetNeighbourPreview();
        }

        /// <summary>
        /// SetPreviewBoard function sets off state for all Piece of the board
        /// </summary>
        private void SetPreviewBoard()
        {
            for (int i = 0; i < board.Count; i++)
            {
                board[i].ApplyNewMaterial(UnityParams.stateToMaterial[UnityParams.offState]);
                board[i].state = UnityParams.offState;
            }
        }

        /// <summary>
        /// SetNeighbourPreview function sets on state for neighbour of the center of the board
        /// </summary>
        private void SetNeighbourPreview()
        {
            int mid = size/2;
            if (size % 2 != 0) mid++;
            Cartesian middle = new Cartesian(mid, mid);
            actions.ChangeNeighourStates(actions.GetNeighbourCoords(middle), board);
        }

        /// <summary>
        /// ComputeAGoal function randomly sets a state on each Piece
        /// </summary>
        private void ComputeAGoal()
        {
            System.Random rnd = new System.Random();
            for (int i = 0; i < goal.Count; i++)
            {
                int state = rnd.Next(0, 2);

                goal[i].ApplyNewMaterial(UnityParams.stateToMaterial[state]);
                goal[i].state = state;

                board[i].ApplyNewMaterial(UnityParams.stateToMaterial[state]);
                board[i].state = state;
            }
        }

        /// <summary>
        /// ScrambleBoard function modifies the goal board to scramble it
        /// </summary>
        private void ScrambleBoard()
        {
            solution = new List<Cartesian>();
            System.Random rnd = new System.Random();
            for (int i = 0; i < difficulty; i++)
            {
                Cartesian randomStep = new Cartesian(rnd.Next(1, size + 1), rnd.Next(1, size + 1));
                while (solution.Contains(randomStep))
                {
                    Debug.Log("doublons " + randomStep.String());
                    randomStep = new Cartesian(rnd.Next(1, size + 1), rnd.Next(1, size + 1));
                }
                solution.Add(randomStep);
                board = actions.ChangeNeighourStates(actions.GetNeighbourCoords(randomStep), board);
            }
        }

        /// <summary>
        /// CheckForEndGame function checks if the given game state is the goal on not
        /// </summary>
        /// <param name="gameState">List of Piece</param>
        /// <returns>bool game finished</returns>
        public bool CheckForEndGame(List<Piece.Piece> gameState)
        {
            for (int i = 0; i<gameState.Count; i++)
            {
                if (!gameState[i].Equals(goal[i])) return false;
            }
            return true;
        }
    }
}

