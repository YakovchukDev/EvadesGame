using UnityEngine;

namespace Map
{
    public class TeleportControl : MonoBehaviour
    {
        public delegate void Teleport(bool isEnter);
        public static event Teleport OpenTeleportMenu;
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
            if(other.gameObject.tag == "Player")
            {   
                OpenTeleportMenu(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                OpenTeleportMenu(false);
            }
        }
    }
}
