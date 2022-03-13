using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class _SaveZoneParameters : MonoBehaviour
    {
        [SerializeField]
        private GameObject _horizontalPrefab;
        [SerializeField]
        private GameObject _oneWayPrefab;
        [SerializeField]
        private List<GameObject> _saveZonePrefabs;
        [SerializeField]
        private List<GameObject> _wallPrefabs;
        [SerializeField]
        private GameObject _floorPrefab;

        public float GetLengthX()
        {
            return _floorPrefab.transform.localScale.x;
        }
        public float GetLengthZ()
        {
            return _floorPrefab.transform.localScale.y;
        }
        public void SetHorizontalParameters(bool isShowSaveZone)
        {
            _horizontalPrefab.SetActive(true);

            foreach(GameObject saveZone in _saveZonePrefabs)
            {
                saveZone.SetActive(isShowSaveZone);
            }
        }
        public void SetVerticalParameters(bool isShowSaveZone)
        {
            _horizontalPrefab.SetActive(true);
            _horizontalPrefab.transform.Rotate(0f, 90f, 0f, Space.World);

            foreach(GameObject saveZone in _saveZonePrefabs)
            {
                saveZone.SetActive(isShowSaveZone);
            }
        }
        public void SetOneWay(string side, bool isShowSaveZone)
        {
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
    }
}
