using System.Collections;
using System.Collections.Generic;

namespace Scripts.Rules
{
    public static class UnityParams
    {
        public static int defaultSize = 6;

        public static int defaultNeighbour = 2;

        public static int defaultDifficulty = 1;

        public static string planeObjectPath = "Prefabs/Plane";

        public static string pieceObjectPath = "Prefabs/Piece";

        public static string defaultMaterialPath = "Textures/Default";

        public static string planeMaterialPath = "Textures/Plane";

        public static string onMaterialPath = "Textures/OnMaterial";

        public static string offMaterialPath = "Textures/OffMaterial";

        public static string onMaterialHighligthedPath = "Textures/OnMaterialHighlighted";

        public static string offMaterialHighligthedPath = "Textures/OffMaterialHighligthed";

        public static string gameName = "LightOut";

        public static int spaceBetweenBoards = 1;

        public static int onState = 1;

        public static int offState = 0;

        public static float clueApparitionTemporisation = 30.0f;

        public static Dictionary<int, string> stateToMaterial = new Dictionary<int, string>(){{onState, onMaterialPath},
                                                                                              {offState, offMaterialPath}};

        public static Dictionary<int, string> stateToHighlightMaterial = new Dictionary<int, string>(){{onState, onMaterialHighligthedPath},
                                                                                                     {offState, offMaterialHighligthedPath}};

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