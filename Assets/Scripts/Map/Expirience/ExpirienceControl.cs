using Audio;
using UnityEngine;
using Map.Data;

namespace Map.Expirience
{
    public class ExpirienceControl : MonoBehaviour
    {
        public delegate void giveExpirience();

        public delegate void BoostMenu(bool isEnter);

        public static event giveExpirience GiveExpiriencs;
        public static event BoostMenu OpenBoostMenu;
        private SafeZoneParameters _saveZone;
        private AudioManager _audioManager;
        public static bool InSaveZone;
        private void Awake()
        {
            _saveZone = GetComponent<SafeZoneParameters>();
            _audioManager = AudioManager.Instanse;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                InSaveZone = true;
                _audioManager.Play("SaveZoneOn");
                if (_saveZone.IsSaveZone)
                {
                    OpenBoostMenu(true);
                    if (_saveZone.GetExpirience())
                    {
                        GiveExpiriencs();
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player" && _saveZone.IsSaveZone)
            {
                InSaveZone = false;
                _audioManager.Play("SaveZoneOff");

                OpenBoostMenu(false);
            }
        }
    }
}