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


        public Board()
        {
            CreatePlane();
            CreatePieces();
        }

        private void CreatePlane()
        {
            GameObject boardObject = new GameObject("Board");
            boardObject.transform.parent = GameObject.Find("LightOut").transform;
            plane = boardObject.AddComponent<Plane.Plane>();
        }

        private void CreatePieces()
        {

        }
    }
}

