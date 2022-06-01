using System;
using UnityEngine;

namespace Map.Data
{
    public class RoomParameters : MonoBehaviour
    {
        public bool IsFirstEntrance = true;
        [SerializeField] private GameObject _saveZoneUp;
        [SerializeField] private GameObject _saveZoneDown;
        [SerializeField] private GameObject _doorRight;
        [SerializeField] private GameObject _doorLeft;
        [SerializeField] private GameObject _wallUpperLeft;
        [SerializeField] private GameObject _wallUpperRight;
        [SerializeField] private GameObject _wallLowerLeft;
        [SerializeField] private GameObject _wallLowerRight;
        [SerializeField] private GameObject _longWallUp;
        [SerializeField] private GameObject _longWallDown;
        [SerializeField] private GameObject _floor;
        public static event Action<Vector2Int> OnEnterRoom;
        public static event Action<Vector2Int> OnExitRoom;
        private Vector2Int _position;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                OnEnterRoom(_position);
            }
            else if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.transform.SetParent(this.transform);
            }
        }
        public void SetDoorParameters(bool upper, bool right, bool lower, bool left)
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
        }
        public void SetPosition(Vector2Int position)
        {
            _position = position;
        }
        public Vector2Int GetPosition() => _position;
        public float GetLengthX() => _floor.transform.localScale.x;
        public float GetLengthZ() => _floor.transform.localScale.y;
    }
}