using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class _RoomParameters : MonoBehaviour
    {        
        public delegate void UpdateOfRoomsAround(int indexX, int indexY);
        public static event UpdateOfRoomsAround EnterNewRoom;

        [SerializeField]
        private GameObject _doorUpper;
        [SerializeField]
        private GameObject _doorRight;
        [SerializeField]
        private GameObject _doorLower;
        [SerializeField]
        private GameObject _doorLeft;
        [SerializeField]
        private GameObject _doorCentralLeft;
        [SerializeField]
        private GameObject _doorCentralRight;

        
        [SerializeField]
        private GameObject _wallUpperLeft;
        [SerializeField]
        private GameObject _wallUpperRight;
        [SerializeField]
        private GameObject _wallLowerLeft;
        [SerializeField]
        private GameObject _wallLowerRight;
        [SerializeField]
        private GameObject _floor;

        private int cordinatX;
        private int cordinatY;

        public float GetLengthX()
        {
            return _floor.transform.localScale.x;
        }
        public float GetLengthZ()
        {
            return _floor.transform.localScale.y;
        }

        public void SetDoorParameters(bool upper, bool right, bool lower, bool left, bool centralLeft, bool centralRight)
        {
            _doorUpper.SetActive(upper);
            _doorRight.SetActive(right);
            _doorLower.SetActive(lower);
            _doorLeft.SetActive(left);
            _doorCentralLeft.SetActive(centralLeft);
            _doorCentralRight.SetActive(centralRight);
        }
        public Dictionary<string, bool> GetDoorParameters()
        {
            return new Dictionary<string, bool>() 
            { 
                {"upper", _doorUpper},
                {"right", _doorRight},
                {"lower", _doorLower},
                {"left", _doorLeft},
                {"centralLeft", _doorCentralLeft},
                {"centralRight", _doorCentralRight}
            };
        }

        public void SetSkins(Mesh door, Mesh wallTurnLeft, Mesh wallTurnRight, Mesh floor)
        {
            
        }
        public void SetCordinatRoom(int x, int y)
        {
            cordinatX = x;
            cordinatY = y;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                EnterNewRoom(cordinatX, cordinatY);
            }
        }
    }
}