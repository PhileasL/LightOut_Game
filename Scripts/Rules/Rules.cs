using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Rules
{
    public class Rules
    {
        public static int size = 5;

        public static int neighbour = 1;

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

        public static string boardName = "Board";

        public static string pieceName = "Piece";

        public static string boardHierarchy = gameName + "/" + boardName;

        public static int onState = 1;

        public static int offState = 0;

        public static string CreatePieceName(int x, int y)
        {
            return pieceName + "_" + x.ToString() + "_" + y.ToString();
        }

        public static Dictionary<int, string> stateToMaterial = new Dictionary<int, string>(){{onState, onMaterialPath},
                                                                                              {offState, offMaterialPath}};
    }
}

