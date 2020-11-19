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
            GameObject piecePrefab = (GameObject)Resources.Load("Prefabs/Piece");
            pieceObject = (GameObject)Instantiate(piecePrefab);
            float x = float.Parse(this.name.Split('_')[1], CultureInfo.InvariantCulture.NumberFormat);
            float y = float.Parse(this.name.Split('_')[2], CultureInfo.InvariantCulture.NumberFormat);
            coord = new Cartesian(x, y);
            Debug.Log(coord.Coord(0).ToString() + " " + coord.Coord(1).ToString());
            pieceObject.transform.parent = this.transform;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

