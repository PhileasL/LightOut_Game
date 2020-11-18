﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Rules;
using Scripts.Coord;

namespace Scripts.Board.Plane
{
    public class Plane : MonoBehaviour
    {
        //manage the gameObject and texture

        /// <summary>
        /// planePrefab is the plane GameObject
        /// </summary>
        public GameObject planeObject;

        /// <summary>
        /// coords is an instance of Cartesian
        /// </summary>
        public Cartesian coords;


        /// <summary>
        /// size of the plane, 0.1 is one piece
        /// </summary>
        protected float size = (float)Rules.Rules.size / 10;

        // Start is called before the first frame update
        void Start()
        {
            ShowPlane();
            SetParameters();
        }

        /// <summary>
        /// ShowPlane show the plane on the scene
        /// </summary>
        private void ShowPlane()
        {
            GameObject planePrefab = (GameObject)Resources.Load("Prefabs/Plane");
            planeObject = (GameObject)Instantiate(planePrefab);
        }

        /// <summary>
        /// SetParameters sets scale of plane and it's position relative to the world
        /// </summary>
        private void SetParameters()
        {
            planeObject.transform.localScale = new Vector3(size, 1, size);
            coords = new Cartesian(size * 5, size * 5);
            planeObject.transform.position = new Vector3(coords.Coord(0), 0, coords.Coord(1));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

