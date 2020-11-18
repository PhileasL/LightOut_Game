using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Coord;

namespace Scripts.Piece
{
    public class Piece : MonoBehaviour
    {

        public GameObject pieceObject;

        void Start()
        {
            GameObject piecePrefab = (GameObject)Resources.Load("Prefabs/Piece");
            pieceObject = (GameObject)Instantiate(piecePrefab);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

