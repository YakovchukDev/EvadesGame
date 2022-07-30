using System;
using UnityEngine;

namespace Education.Level.Controls
{
    public class EducationExperienceControl : MonoBehaviour
    {
        public static event Action GiveExperience;
        public static event Action<bool> OpenBoostMenu;
        private EducationSaveZone _saveZone;
        private bool _canGiveExperience = true;
        private void Awake()
        {
            _saveZone = GetComponent<EducationSaveZone>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_saveZone.GiveExpirience)
                {
                    OpenBoostMenu?.Invoke(true);
                    if (_canGiveExperience)
                    {
                        GiveExperience?.Invoke();
                        _canGiveExperience = false;
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && _saveZone.GiveExpirience)
            {
                OpenBoostMenu?.Invoke(false);
            }
        }
    }
}