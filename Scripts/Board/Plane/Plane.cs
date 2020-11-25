using System.Collections;
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
        protected float scale = (float)Rules.Rules.size / 10;

        /// <summary>
        /// center of the plane object
        /// </summary>
        protected float center = (float)Rules.Rules.size / 2;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        /// <summary>
        /// ShowPlane show the plane on the scene and assign it a texture
        /// </summary>
        private void ShowPlane()
        {
            GameObject planePrefab = (GameObject)Resources.Load(Rules.UnityParams.planeObjectPath);
            planeObject = (GameObject)Instantiate(planePrefab);

            Material mat = (Material)Resources.Load(Rules.UnityParams.planeMaterialPath);
            Renderer rend = planeObject.GetComponent<Renderer>();
            rend.material = mat;
        }

        /// <summary>
        /// SetParameters sets scale of plane and it's position relative to this
        /// </summary>
        private void SetParameters()
        {
            planeObject.transform.parent = this.transform;
            planeObject.transform.localScale = new Vector3(scale, 1, scale);
            planeObject.transform.position = new Vector3((float)(center), 0, (float)(center));           
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPosition(bool isGoal)
        {
            ShowPlane();
            SetParameters();
            if (isGoal)
            {
                coords = new Cartesian(0, (float)(Rules.Rules.size + Rules.UnityParams.spaceBetweenBoards));
                this.transform.position = new Vector3(coords.Coord(0), 0, coords.Coord(1));
            }
            else
            {
                coords = new Cartesian(0,0);
                this.transform.position = new Vector3(coords.Coord(0), 0, coords.Coord(1));
            }
        }
    }
}

