using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Coord;
using System.Globalization;
using Scripts.Rules;

namespace Scripts.Piece
{
    public class Piece : MonoBehaviour
    {

        /// <summary>
        /// pieceObject is an instance of piece in prefabs
        /// </summary>
        public GameObject pieceObject;

        /// <summary>
        /// coord represents the Cartesian coordinates of the piece relative to the Board
        /// </summary>
        public Cartesian coord;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ApplyNewMaterial(string pathToMaterial)
        {

        }

        /// <summary>
        /// CreatePieceAt create a piece at given coord
        /// it puts its parent to the name of the piece
        /// </summary>
        /// <param name="coord"></param>
        public void CreatePieceAt(Cartesian coord)
        {
            GameObject piecePrefab = (GameObject)Resources.Load(Rules.unityParams.pieceObjectPath);
            pieceObject = (GameObject)Instantiate(piecePrefab);
            pieceObject.transform.parent = this.transform;

            this.coord = coord;
            pieceObject.transform.position = new Vector3(coord.Coord(0), 0, coord.Coord(1));
        }
    }
}

