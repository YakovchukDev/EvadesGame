using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private int _hpNumber = 1;
        [SerializeField] private GameObject _panelAfterDie;
        [SerializeField] private List<Button> _spell;
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
                if (_spell != null)
                {
                    foreach (var spell in _spell)
                    {
                        spell.interactable = true;
                    }
                }

                transform.localScale = Vector3.one;
                _hpNumber--;
                HpChecker();
                Destroy(other.gameObject);
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