using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Rules;

namespace Scripts.Board.Plane
{
    public class Plane : MonoBehaviour
    {
        //manage the gameObject and texture

        /// <summary>
        /// planePrefab is the plane GameObject
        /// </summary>
        public GameObject planePrefab;

        // Start is called before the first frame update
        void Start()
        {
            planePrefab = (GameObject)Resources.Load("Prefabs/Plane");
            GameObject planeObject = (GameObject)Instantiate(planePrefab);
            planeObject.transform.localScale = new Vector3((float)Rules.Rules.size/10, 1, (float)Rules.Rules.size / 10);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

