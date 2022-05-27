using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class SafeZoneParameters : MonoBehaviour
    {
        public bool IsSaveZone = false;
        [SerializeField] private GameObject _horizontalRoom;
        [SerializeField] private GameObject _oneWayRoom;
        [SerializeField] private List<GameObject> _saveZoneWall;
        [SerializeField] private GameObject _floor;
        private bool _isHaveExpirience = false;
        private Vector2Int _positionRoom;
        public static event Action<Vector2Int> OnEnterSafeZone;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player" && IsSaveZone)
            {
                OnEnterSafeZone(_positionRoom);
            }
        }
        public float GetLengthX()
        {
            return _floor.transform.localScale.x;
        }
        public float GetLengthZ()
        {
            return _floor.transform.localScale.y;
        }
        public bool GetExpirience()
        {
            if(_isHaveExpirience)
            {
                _isHaveExpirience = false;
                return true;
            }
            else
            {
                return false;
            }
        } 
        public void SetHorizontalParameters(bool isSaveZone)
        {
            IsSaveZone = isSaveZone;
            _isHaveExpirience = isSaveZone;
            
            _horizontalRoom.SetActive(true);
            Destroy(_oneWayRoom);
            foreach(GameObject saveZone in _saveZoneWall)
            {
                saveZone.SetActive(isSaveZone);
            }
        }
        public void SetOneWay(short openSide, bool isSaveZone)
        {
            IsSaveZone = isSaveZone;
            _isHaveExpirience = isSaveZone;

            _oneWayRoom.SetActive(true);
            Destroy(_horizontalRoom);
            _oneWayRoom.transform.Rotate(0, openSide, 0);
            foreach(GameObject saveZone in _saveZoneWall)
            {
                saveZone.SetActive(isSaveZone);
            }
        }
        public void SetPosition(Vector2Int positionRoom)
        {
            _positionRoom = positionRoom;
        }
        public void GetPosition(Vector2Int positionRoom)
        {
            _positionRoom = positionRoom;
        }
    }
}
