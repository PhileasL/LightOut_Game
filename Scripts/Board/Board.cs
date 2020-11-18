using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board.Plane;
using Scripts.Rules;

namespace Scripts.Board
{
    public class Board
    {
        public Plane.Plane plane;
        protected int size = Rules.Rules.size;

        public Board(GameObject parent)
        {
            GameObject boardObject = new GameObject("Board");
            boardObject.transform.parent = parent.transform;
            plane = boardObject.AddComponent<Plane.Plane>();
        }
    }
}

