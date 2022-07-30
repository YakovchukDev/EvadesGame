using System;
using System.Collections.Generic;
using Audio;
using Map.Ending;
using UnityEngine;

namespace Map.Data
{
    public class SafeZoneParameters : MonoBehaviour
    {
        public bool IsSaveZone;
        [SerializeField] private GameObject _horizontalRoom;
        [SerializeField] private GameObject _oneWayRoom;
        [SerializeField] private PseudoDoor _pseudoDoor;
        [SerializeField] private GameObject _backWall;
        [SerializeField] private GameObject _floor;
        [SerializeField] private List<GameObject> _safeZoneWallList;
        private bool _isHaveExpirience;
        private Vector2Int _positionRoom;
        private AudioManager _audioManager;
        public static event Action<Vector2Int> OnEnterSafeZone;
        public static event Action<bool> OnEnter;

        private void Start()
        {
            _audioManager = AudioManager.Instanse;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && IsSaveZone)
            {
                OnEnterSafeZone?.Invoke(_positionRoom);
                OnEnter?.Invoke(IsSaveZone);
                if (_audioManager != null)
                {
                    _audioManager.IsMute("Friction", true);
                }
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.transform.SetParent(this.transform);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && IsSaveZone)
            {
                OnEnter?.Invoke(false);
            }
        }

        public void SetHorizontalParameters(bool isSaveZone)
        {
            IsSaveZone = isSaveZone;
            _isHaveExpirience = isSaveZone;
            foreach (GameObject safeZoneWall in _safeZoneWallList)
            {
                safeZoneWall.SetActive(isSaveZone);
            }
            Destroy(_oneWayRoom);
        }

        public void SetOneWayParameters(short openSide, bool isSaveZone, DoorStatusEnum doorStatus)
        {
            IsSaveZone = isSaveZone;
            _isHaveExpirience = isSaveZone;
            foreach (GameObject safeZoneWall in _safeZoneWallList)
            {
                safeZoneWall.SetActive(isSaveZone);
            }
            Destroy(_horizontalRoom);
            _oneWayRoom.transform.Rotate(0, openSide, 0);
            if (doorStatus == DoorStatusEnum.None)
            {
                Destroy(_pseudoDoor.gameObject);
                _pseudoDoor = null;
            }
            else
            {
                _pseudoDoor.UpdateDoor(doorStatus);
                Destroy(_backWall);
            }
        }

        public void SetPosition(Vector2Int positionRoom)
        {
            _positionRoom = positionRoom;
        }

        public Vector2Int GetPosition() => _positionRoom;
        public float GetLengthX() => _floor.transform.localScale.x;
        public float GetLengthZ() => _floor.transform.localScale.y;

        public bool GetExpirience()
        {
            switch (_isHaveExpirience)
            {
                case true:
                {
                    _isHaveExpirience = false;
                    return true;
                }
                case false:
                {
                    return false;
                }
            }
        }
    }
}