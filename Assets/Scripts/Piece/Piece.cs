﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Coord;
using System.Globalization;
using Scripts.Rules;

namespace Scripts.Piece
{
    /// <summary>
    /// Piece class is a supervisor for a Piece GameObject
    /// </summary>
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

        /// <summary>
        /// State of a Piece
        /// </summary>
        public int state;

        /// <summary>
        /// ApplyHighlight function change the highlight of the Piece or not depending on the given argument
        /// </summary>
        /// <param name="highlight">bool</param>
        public void ApplyHighlight(bool highlight)
        {
            if (highlight) ApplyNewMaterial(Rules.UnityParams.stateToHighlightMaterial[state]);
            else ApplyNewMaterial(Rules.UnityParams.stateToMaterial[state]);
        }

        /// <summary>
        /// ApplyNewMaterial function takes the material path in argument an apply it to the Piece
        /// </summary>
        /// <param name="materialPath"></param>
        public void ApplyNewMaterial(string materialPath)
        {
            Material mat = (Material)Resources.Load(materialPath);
            Renderer rend = pieceObject.GetComponent<Renderer>();
            rend.material = mat;
        }

        /// <summary>
        /// ChangeState function changes the state of the Piece
        /// </summary>
        public void ChangeState()
        {
            if (state == 1) { state = 0; }
            else { state = 1; }
            ApplyNewMaterial(Rules.UnityParams.stateToMaterial[state]);
        }

        /// <summary>
        /// CreatePieceAt create a piece at given coord
        /// it puts its parent to the name of the piece
        /// </summary>
        /// <param name="coord"></param>
        public void CreatePieceAt(Cartesian coord, bool isGoal, int size)
        {
            GameObject piecePrefab = (GameObject)Resources.Load(Rules.UnityParams.pieceObjectPath);
            pieceObject = (GameObject)Instantiate(piecePrefab);
            pieceObject.transform.parent = this.transform;

            this.coord = coord;
            pieceObject.transform.position = new Vector3((float)(coord.Coord(0)-0.5), 0, (float)(coord.Coord(1)-0.5));
            if (isGoal) { pieceObject.transform.position += new Vector3(0, 0, size + Rules.UnityParams.spaceBetweenBoards); }
        }

        /// <summary>
        /// Equals function compare two Piece state and coordinates
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(Piece other)
        {
            return (other.state == state && other.coord.Equals(coord));
        }
    }
}

