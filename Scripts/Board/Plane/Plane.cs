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
        /// represents the coordinates of this relative to the world
        /// </summary>
        public Cartesian coords;


        /// <summary>
        /// size of the plane, 0.1 is one piece
        /// </summary>
        protected float size = (float)Rules.Rules.size / 10;

        /// <summary>
        /// center of the plane object
        /// </summary>
        protected float center = (float)Rules.Rules.size / 2;

        // Start is called before the first frame update
        void Start()
        {
            coords = new Cartesian((float)-0.5, (float)-0.5);
            this.transform.position = new Vector3(coords.Coord(0), 0, coords.Coord(1));
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
            Material newMaterialRef = (Material)Resources.Load("Textures/Plane");
            Renderer m_ObjectRenderer = planeObject.GetComponent<Renderer>();
            m_ObjectRenderer.material = newMaterialRef;
        }

        /// <summary>
        /// SetParameters sets scale of plane and it's position relative to this
        /// </summary>
        private void SetParameters()
        {
            planeObject.transform.parent = GameObject.Find("LightOut/Board").transform;
            planeObject.transform.localScale = new Vector3(size, 1, size);

            planeObject.transform.position = new Vector3((float)(center), 0, (float)(center));
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

