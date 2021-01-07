using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Rules;
using Scripts.Coord;

namespace Scripts.Board.Plane
{
    /// <summary>
    /// Plane class is the class of a Plane gameObject
    /// </summary>
    public class Plane : MonoBehaviour
    {
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
        private float scale;

        /// <summary>
        /// center of the plane object
        /// </summary>
        private float center;

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
        /// <param name="size">int size of plane</param>
        private void SetParameters(int size)
        {
            scale = (float)size / 10;
            center = (float)size / 2;

            planeObject.transform.parent = this.transform;
            planeObject.transform.localScale = new Vector3(scale, 1, scale);
            planeObject.transform.position = new Vector3((float)(center), 0, (float)(center));           
        }

        /// <summary>
        /// SetPosition shows plane and sets its differents parameters (size, scale, position)
        /// </summary>
        /// <param name="isGoal">bool goalPlane or not</param>
        /// <param name="size">int size of plane</param>
        public void SetPosition(bool isGoal, int size)
        {
            ShowPlane();
            SetParameters(size);
            if (isGoal)
            {
                coords = new Cartesian(0, (float)(size + Rules.UnityParams.spaceBetweenBoards));
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

