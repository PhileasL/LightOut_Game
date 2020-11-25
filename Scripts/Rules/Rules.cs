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

    public static class unityParams
    {
        public static string planeObjectPath = "Prefabs/Plane";

        public static string pieceObjectPath = "Prefabs/Piece";

        public static string defaultMaterialPath = "Textures/Default";

        public static string planeMaterialPath = "Textures/Plane";

        public static string gameName = "LightOut";

        public static string boardName = "Board";

        public static string pieceName = "Piece";

        public static string boardHierarchy = gameName + "/" + boardName;

        public static string CreatePieceName(int x, int y)
        {
            return pieceName + "_" + x.ToString() + "_" + y.ToString();
        }
    }
}

