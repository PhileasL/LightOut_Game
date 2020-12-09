using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Coord;

namespace Scripts.Rules
{
    public class Rules
    {
        public static int size = 5;

        public static int neighbour = 2;

        public static int difficulty = 1;

        private List<Piece.Piece> goal;

        public List<Piece.Piece> board;

        public Actions.Actions actions;

        public Rules(List<Piece.Piece> goal, List<Piece.Piece> board)
        {
            this.goal = goal;
            this.board = board;
            actions = new Actions.Actions(this);
            ComputeAGoal();
            ScrambleBoard();
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
            List<Cartesian> alreadyDone = new List<Cartesian>();
            System.Random rnd = new System.Random();
            for (int i = 0; i < difficulty; i++)
            {
                Cartesian randomStep = new Cartesian(rnd.Next(1, size + 1), rnd.Next(1, size + 1));
                while (alreadyDone.Contains(randomStep))
                {
                    Debug.Log("doublons " + randomStep.String());
                    randomStep = new Cartesian(rnd.Next(1, size + 1), rnd.Next(1, size + 1));
                }
                alreadyDone.Add(randomStep);
                Debug.Log(randomStep.String());
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

    public static class UnityParams
    {
        public static string planeObjectPath = "Prefabs/Plane";

        public static string pieceObjectPath = "Prefabs/Piece";

        public static string defaultMaterialPath = "Textures/Default";

        public static string planeMaterialPath = "Textures/Plane";

        public static string onMaterialPath = "Textures/OnMaterial";

        public static string offMaterialPath = "Textures/OffMaterial";

        public static string gameName = "LightOut";

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

