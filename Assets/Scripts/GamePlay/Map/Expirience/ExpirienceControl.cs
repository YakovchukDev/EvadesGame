using System;
using Map.Data;
using UnityEngine;

namespace GamePlay.Map.Expirience
{
    public class ExpirienceControl : MonoBehaviour
    {
        public static event Action GiveExpirience;
        public static event Action<bool> OpenBoostMenu;
        private SafeZoneParameters _saveZone;

        private void Awake()
        {
            _saveZone = GetComponent<SafeZoneParameters>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                if(_saveZone.IsSaveZone)
                {
                    OpenBoostMenu?.Invoke(true);
                    if(_saveZone.GetExpirience())
                    {
                        GiveExpirience?.Invoke();
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player") && _saveZone.IsSaveZone)
            {
                OpenBoostMenu?.Invoke(false);
            }
        }
    }
}