using UnityEngine;
using System;
using Map.Data;

namespace Map.Teleport
{
    public class TeleportControl : MonoBehaviour
    {
        public static event Action<bool> OpenTeleportMenu;
        public delegate SafeZoneParameters GetCentralSafeZone();
        public static event GetCentralSafeZone OnGetCentralSafeZone;
        private SafeZoneParameters _smallRoom;
        private void Start()
        {
            _smallRoom = GetComponent<SafeZoneParameters>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(_smallRoom == null)
            {
                _smallRoom = GetComponent<SafeZoneParameters>();
            }
            if(other.gameObject.CompareTag("Player") && OnGetCentralSafeZone().IsSaveZone && _smallRoom.IsSaveZone)
            {   
                OpenTeleportMenu?.Invoke(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Player") && OnGetCentralSafeZone().IsSaveZone && _smallRoom.IsSaveZone)
            {
                OpenTeleportMenu?.Invoke(false);
            }
        }
    }
}
