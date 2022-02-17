using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maps
{
    public class Room : MonoBehaviour
    {
        public GameObject RoomPrefab;
        
        public bool SaveLeft;
        public bool SaveRight;

        public bool ExitLeft;
        public bool ExitRight;
        public bool ExitUp;
        public bool ExitDown;
    }
}
