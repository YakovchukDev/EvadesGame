using System;
using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Map.Data
{
    public class SafeZoneParameters : MonoBehaviour
    {
        public bool IsSaveZone;
        [SerializeField] private GameObject _horizontalRoom;
        [SerializeField] private GameObject _oneWayRoom;
        [SerializeField] private List<GameObject> _saveZoneWall;
        [SerializeField] private GameObject _floor;
        private bool _isHaveExpirience;
        private Vector2Int _positionRoom;
        private AudioManager _audioManager;
        public static event Action<Vector2Int> OnEnterSafeZone;

        private void Start()
        {
            _audioManager = AudioManager.Instanse;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player") && IsSaveZone)
            {
                OnEnterSafeZone(_positionRoom);
                if (_audioManager != null)
                {
                    _audioManager.IsMute("Friction",true);
                }

            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.transform.SetParent(this.transform);
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
        public void SetOneWayParameters(short openSide, bool isSaveZone)
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
