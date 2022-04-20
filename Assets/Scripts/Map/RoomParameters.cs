using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class RoomParameters : MonoBehaviour
    {
        //***нужно сделать скрытие стен безопсной зоны
        public delegate void UpdateOfRoomsAround(int indexX, int indexY);
        public static event UpdateOfRoomsAround EnterNewRoom;

        public List<CoinControl> CoinList; 

        [SerializeField] private GameObject _saveZoneUp;
        [SerializeField] private GameObject _saveZoneDown;
        [SerializeField] private GameObject _doorRight;
        [SerializeField] private GameObject _doorLeft;
        [SerializeField] private GameObject _doorCentralLeft;
        [SerializeField] private GameObject _doorCentralRight;


        [SerializeField] private GameObject _wallUpperLeft;
        [SerializeField] private GameObject _wallUpperRight;
        [SerializeField] private GameObject _wallLowerLeft;
        [SerializeField] private GameObject _wallLowerRight;
        [SerializeField] private GameObject _longWallUp;
        [SerializeField] private GameObject _longWallDown;
        [SerializeField] private GameObject _floor;

        private int cordinatX;
        private int cordinatY;

        private bool destroyEnemy = false;

        public float GetLengthX()
        {
            return _floor.transform.localScale.x;
        }

        public float GetLengthZ()
        {
            return _floor.transform.localScale.y;
        }

        public void SetDoorParameters(bool upper, bool right, bool lower, bool left, bool centralLeft,
            bool centralRight)
        {
            _saveZoneUp.SetActive(!upper);
            _saveZoneDown.SetActive(!lower);

            if(upper)
            {
                _longWallUp.SetActive(upper);
                _wallUpperLeft.SetActive(!upper);
                _wallUpperRight.SetActive(!upper);
                _saveZoneUp.SetActive(!upper);
            }
            if(lower)
            {
                _longWallDown.SetActive(lower);
                _wallLowerLeft.SetActive(!lower);
                _wallLowerRight.SetActive(!lower);
                _saveZoneDown.SetActive(!lower);
            }
            _doorRight.SetActive(right);
            _doorLeft.SetActive(left);
            _doorCentralLeft.SetActive(centralLeft);
            _doorCentralRight.SetActive(centralRight);
        }

        public Dictionary<string, bool> GetDoorParameters()
        {
            return new Dictionary<string, bool>()
            {
                {"upper", _longWallUp},
                {"right", _doorRight},
                {"lower", _longWallDown},
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

        public void DestroyAllEnemy(bool isDestroy)
        {
            destroyEnemy = isDestroy;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                EnterNewRoom(cordinatX, cordinatY);
            }
            else if (destroyEnemy)
            {
                Destroy(other);
            }
        }
    }
}