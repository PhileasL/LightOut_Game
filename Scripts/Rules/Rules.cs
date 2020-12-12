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

        public List<Piece.Piece> goal;

        public List<Piece.Piece> board;

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
            /*solution.Reverse();
            foreach (Cartesian coords in solution)
            {
                Debug.Log(coords.String());
            }*/
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

    public static class UnityParams
    {
        public static string planeObjectPath = "Prefabs/Plane";

        public static string pieceObjectPath = "Prefabs/Piece";

        public static string defaultMaterialPath = "Textures/Default";

        public static string planeMaterialPath = "Textures/Plane";

        public static string onMaterialPath = "Textures/OnMaterial";

        public static string offMaterialPath = "Textures/OffMaterial";

        public static string gameName = "LightOut";

        public static string ui = gameName + "/UI";

        public static string clueButtonName = ui + "/Clue";

        public static string actionRemainingTextName = ui + "/actionRemaining";

        public static int spaceBetweenBoards = 1;

        public static int onState = 1;

        public static int offState = 0;
        
        public static Dictionary<int, string> stateToMaterial = new Dictionary<int, string>(){{onState, onMaterialPath},
                                                                                              {offState, offMaterialPath}};
    
        public readonly ref struct game
        {
            public static string boardName = "GameBoard";

            public static string pieceName = "GamePiece";

            public static string boardHierarchy = gameName + "/" + boardName;

            public static string CreatePieceName(int x, int y)
            {
                return pieceName + "_" + x.ToString() + "_" + y.ToString();
            }
        }

        public readonly ref struct goal
        {
            public static string boardName = "GoalBoard";

            public static string pieceName = "GoalPiece";

            public static string boardHierarchy = gameName + "/" + boardName;

            public static string CreatePieceName(int x, int y)
            {
                return pieceName + "_" + x.ToString() + "_" + y.ToString();
            }
        }
    }
}

