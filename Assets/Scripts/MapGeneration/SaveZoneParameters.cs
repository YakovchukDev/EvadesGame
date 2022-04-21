using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class SaveZoneParameters : MonoBehaviour
    {
        public delegate void GiveWherePlayer(int x, int y);
        public static event GiveWherePlayer PlayerHere;
        public bool IsSaveZone = false;
        [SerializeField] private GameObject _horizontalPrefab;
        [SerializeField] private GameObject _oneWayPrefab;
        [SerializeField] private List<GameObject> _saveZonePrefabs;
        [SerializeField] private List<GameObject> _wallPrefabs;
        [SerializeField] private GameObject _floorPrefab;
        private bool _isGetExpirience = false;
        private int coordinateX;
        private int coordinateY;
        
        public float GetLengthX()
        {
            return _floorPrefab.transform.localScale.x;
        }
        public float GetLengthZ()
        {
            return _floorPrefab.transform.localScale.y;
        }
        public bool GetExpirience()
        {
            if(_isGetExpirience)
            {
                _isGetExpirience = false;
                return true;
            }
            else
            {
                return false;
            }
        } 
        public void SetHorizontalParameters(bool isShowSaveZone)
        {
            _horizontalPrefab.SetActive(true);
            
            IsSaveZone = isShowSaveZone;
            _isGetExpirience = isShowSaveZone;

            foreach(GameObject saveZone in _saveZonePrefabs)
            {
                saveZone.SetActive(isShowSaveZone);
            }
        }
        public void SetVerticalParameters(bool isShowSaveZone)
        {
            IsSaveZone = isShowSaveZone;
            _horizontalPrefab.SetActive(true);
            _horizontalPrefab.transform.Rotate(0f, 90f, 0f, Space.World);

            foreach(GameObject saveZone in _saveZonePrefabs)
            {
                saveZone.SetActive(isShowSaveZone);
            }
        }
        public void SetOneWay(string side, bool isShowSaveZone)
        {
            IsSaveZone = isShowSaveZone;
            _isGetExpirience = isShowSaveZone;
            
            _oneWayPrefab.SetActive(true);
            int rotate = 0;
            if(side == "left")
            {
                rotate = 180;
            }
            else if(side == "down")
            {
                rotate = 90;
            }
            else if(side == "up")
            {
                rotate = -90;
            }
            else if(side == "right")
            {
                rotate = 0;
            }
            _oneWayPrefab.transform.Rotate(0, rotate, 0);

            foreach(GameObject saveZone in _saveZonePrefabs)
            {
                saveZone.SetActive(isShowSaveZone);
            }
        }

        public void SetSkins(Mesh wall, Mesh saveZone, Mesh floor)
        {

        }
        public void SetCoordinate(int x, int y)
        {
            coordinateX = x;
            coordinateY = y;
        }
        public Dictionary<string, int> GetCoordinate()
        {
            return new Dictionary<string, int> { {"x", coordinateX}, {"y", coordinateY} };
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                PlayerHere(coordinateX, coordinateY);
            }
        }
    }
}
