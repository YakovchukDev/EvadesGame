using System;
using System.Collections.Generic;
using UnityEngine;
using Map.Coins;
using Map.Data;

namespace Map
{
    public class RoomParameters : MonoBehaviour
    {
        public List<CoinControl> CoinList; 
        public List<GameObject> EnemyList;
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
        public static event Action<Vector2Int> OnExibtRoom;
        private Vector2Int _position;

        public float GetLengthX()
        {
            return _floor.transform.localScale.x;
        }

        public float GetLengthZ()
        {
            return _floor.transform.localScale.y;
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
        public Vector2Int GetPosition()
        {
            return _position;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                OnEnterRoom(_position);
            }
        }
    }
}