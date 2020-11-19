using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Coord;
using System.Globalization;

namespace Scripts.Piece
{
    public class Piece : MonoBehaviour
    {

        public GameObject pieceObject;

        public Cartesian coord;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreatePieceAt(Cartesian coord)
        {
            GameObject piecePrefab = (GameObject)Resources.Load("Prefabs/Piece");
            pieceObject = (GameObject)Instantiate(piecePrefab);
            pieceObject.transform.parent = this.transform;
            this.coord = coord;
            pieceObject.transform.position = new Vector3(coord.Coord(0), 0, coord.Coord(1));
        }
    }
}

