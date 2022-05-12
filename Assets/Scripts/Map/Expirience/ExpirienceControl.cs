using UnityEngine;

namespace Map.Expirience
{
    public class ExpirienceControl : MonoBehaviour
    {
        public delegate void giveExpirience();
        public delegate void BoostMenu(bool isEnter);
        public static event giveExpirience GiveExpiriencs;
        public static event BoostMenu OpenBoostMenu;
        private SaveZoneParameters _saveZone;

        private void Awake()
        {
            _saveZone = GetComponent<SaveZoneParameters>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                if(_saveZone.IsSaveZone)
                {
                    OpenBoostMenu(true);
                    if(_saveZone.GetExpirience())
                    {
                        GiveExpiriencs();
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.tag == "Player" && _saveZone.IsSaveZone)
            {
                OpenBoostMenu(false);
            }
        }
    }
}
