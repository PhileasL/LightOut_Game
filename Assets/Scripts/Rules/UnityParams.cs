using System.Collections;
using System.Collections.Generic;

namespace Scripts.Rules
{
    /// <summary>
    /// UnityParams class contains all the readonly definitions of the game
    /// </summary>
    public static class UnityParams
    {
        public static readonly int defaultSize = 6;

        public static readonly int defaultNeighbour = 2;

        public static readonly int defaultDifficulty = 1;

        public static readonly float clueApparitionTemporisation = 30.0f;

        public static readonly string planeObjectPath = "Prefabs/Plane";

        public static readonly string pieceObjectPath = "Prefabs/Piece";

        public static readonly string defaultMaterialPath = "Textures/Default";

        public static readonly string planeMaterialPath = "Textures/Plane";

        public static readonly string onMaterialPath = "Textures/OnMaterial";

        public static readonly string offMaterialPath = "Textures/OffMaterial";

        public static readonly string onMaterialHighligthedPath = "Textures/OnMaterialHighlighted";

        public static readonly string offMaterialHighligthedPath = "Textures/OffMaterialHighligthed";

        public static readonly string gameName = "LightOut";

        public static readonly int spaceBetweenBoards = 1;

        public static readonly int onState = 1;

        public static readonly int offState = 0;

        public static readonly Dictionary<int, string> stateToMaterial = new Dictionary<int, string>(){{onState, onMaterialPath},
                                                                                              {offState, offMaterialPath}};

        public static readonly Dictionary<int, string> stateToHighlightMaterial = new Dictionary<int, string>(){{onState, onMaterialHighligthedPath},
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