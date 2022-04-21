using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class ExpirienceControl : MonoBehaviour
    {
        public delegate void giveExpirience();
        public static event giveExpirience GiveExpiriencs;
        private SaveZoneParameters _saveZone;

        private void Start()
        {
            _saveZone = GetComponent<SaveZoneParameters>();
        }
        void OnTriggerEnter(Collider other)
        {
            if(_saveZone == null)
            {
                _saveZone = GetComponent<SaveZoneParameters>();
            }
            if(other.gameObject.tag == "Player" && _saveZone.IsSaveZone && _saveZone.GetExpirience())
            {
                GiveExpiriencs();
            }
        }
    }
}
