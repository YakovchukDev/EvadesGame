using System;
using UnityEngine;

namespace Map.Data
{
    public class RoomParameters : MonoBehaviour
    {
        public bool IsFirstEntrance = true;
        public bool Upper = false;
        public bool Bottom = false;
        [SerializeField] private GameObject _doorRight;
        [SerializeField] private GameObject _doorLeft;
        [SerializeField] private GameObject _topSide;
        [SerializeField] private GameObject _bottomSide;
        [SerializeField] private GameObject _longTopWall;
        [SerializeField] private GameObject _longBottomWall;
        [SerializeField] private GameObject _floor;
        public static event Action<Vector2Int> OnEnterRoom;
        private Vector2Int _position;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnEnterRoom?.Invoke(_position);
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.transform.SetParent(this.transform);
            }
        }
        public void SetDoorParameters(bool upper, bool right, bool lower, bool left)
        {
            Upper = upper;
            Bottom = lower;
            Destroy(upper ? _topSide : _longTopWall);
            Destroy(lower ? _bottomSide : _longBottomWall);
            Destroy(!left ? _doorLeft : null);
            Destroy(!right ? _doorRight : null);
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