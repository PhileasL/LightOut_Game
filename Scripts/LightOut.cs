using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Board;

namespace Scripts
{
    public class LightOut : MonoBehaviour
    {
        private Board.Board board;
        // Start is called before the first frame update
        void Start()
        {
            board = new Board.Board(gameObject);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

