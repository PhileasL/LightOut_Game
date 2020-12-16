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
        public int size = PlayerPrefs.GetInt("size");

        public int neighbour = PlayerPrefs.GetInt("neighbour");

        public int difficulty = PlayerPrefs.GetInt("difficulty");

        public List<Piece.Piece> goal, board;

        public Actions.Actions actions;

        public List<Cartesian> solution;

        public Rules()
        {
            this.goal = (new Board.Board(true, size)).pieces;
            this.board = (new Board.Board(false, size)).pieces;
            actions = new Actions.Actions(this);
            ComputeAGoal();
            ScrambleBoard();
        }

        public Rules(int size, int neighbour)
        {
            this.size = size;
            this.neighbour = neighbour;
            this.board = (new Board.Board(false, size)).pieces;
            actions = new Actions.Actions(this);
            SetPreviewBoard();
            SetNeighbourPreview();
        }

        private void SetPreviewBoard()
        {
            for (int i = 0; i < board.Count; i++)
            {
                board[i].ApplyNewMaterial(UnityParams.stateToMaterial[UnityParams.offState]);
                board[i].state = UnityParams.offState;
            }
        }

        private void SetNeighbourPreview()
        {
            int mid = size/2;
            if (size % 2 != 0) mid++;
            Cartesian middle = new Cartesian(mid, mid);
            actions.ChangeNeighourStates(actions.GetNeighbourCoords(middle), board);
        }

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

        public bool checkForEndGame(List<Piece.Piece> gameState)
        {
            for (int i = 0; i<gameState.Count; i++)
            {
                if (!gameState[i].Equals(goal[i])) return false;
            }
            return true;
        }
    }
}

