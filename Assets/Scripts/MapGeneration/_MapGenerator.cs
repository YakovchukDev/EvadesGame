using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class _MapGenerator : MonoBehaviour
    {
        [SerializeField]
        private _Rooms _rooms;
        [SerializeField]
        private _Enemies _enemies;
        [SerializeField]
        private int Complexity;

        void Start()
        {
        }
    }
}
