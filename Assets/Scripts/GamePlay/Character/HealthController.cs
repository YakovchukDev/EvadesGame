using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private int _hpNumber = 1;
        [SerializeField] private GameObject _panelAfterDie;
        [SerializeField] private List<Button> _spellButtons;
        public int HpNumber
        {
            get => _hpNumber;
            set => _hpNumber = value;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
            {
                if (_spellButtons != null)
                {
                    foreach (var spell in _spellButtons)
                    {
                        spell.interactable = true;
                    }
                }

                transform.localScale = Vector3.one;
                _hpNumber--;
                HpChecker();
               other.gameObject.SetActive(false);
            }
        }

        private void HpChecker()
        {
            if (_hpNumber <= 0)
            {
                Time.timeScale = 0;
                _panelAfterDie.SetActive(true);
                InterfaceController.TimeSave();
            }
        }
    }
}