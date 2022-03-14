using System;
using System.Collections.Generic;
using UnityEngine;

namespace MapGeneration
{
    public class _RoomParameters : MonoBehaviour
    {
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

        /*public _RoomParameters(_RoomParameters other)
        {
            this._doorUpper = other._doorUpper;
            this._doorRight = other._doorRight;
            this._doorLower = other._doorLower;
            this._doorLeft = other._doorLeft;
            this._doorCentralLeft = other._doorCentralLeft;
            this._doorCentralRight = other._doorCentralRight;

            this._wallUpperLeft = other._wallUpperLeft;
            this._wallUpperRight = other._wallUpperRight;
            this._wallLowerLeft = other._wallLowerLeft;
            this._wallLowerRight = other._wallLowerRight;
            this._floor = other._floor;
        }*/

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
    }
}