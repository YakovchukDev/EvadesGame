using UnityEngine;
using System;

namespace Map.Teleport
{
    public class TeleportControl : MonoBehaviour
    {
        public static event Action<bool> OpenTeleportMenu;
        public delegate SaveZoneParameters GetCentralSafeZone();
        public static event GetCentralSafeZone OnGetCentralSafeZone;
        private SaveZoneParameters _smallRoom;
        private void Start()
        {
            _smallRoom = GetComponent<SaveZoneParameters>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(_smallRoom == null)
            {
                _smallRoom = GetComponent<SaveZoneParameters>();
            }
            if(other.gameObject.tag == "Player" && OnGetCentralSafeZone().IsSaveZone && _smallRoom.IsSaveZone)
            {   
                OpenTeleportMenu(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Player" && OnGetCentralSafeZone().IsSaveZone && _smallRoom.IsSaveZone)
            {
                OpenTeleportMenu(false);
            }
        }
    }
}
