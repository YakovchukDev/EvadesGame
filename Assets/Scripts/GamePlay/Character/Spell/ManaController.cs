using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell
{
    public class ManaController : MonoBehaviour
    {
        [SerializeField] private Image _manaComponent;

        [SerializeField] private float _allMana = 100;
        [SerializeField] private float _startRegenTime;

        private float _regenTime;
        [SerializeField] private float _numberRegen;


        private void Update()
        {
            ManaRegen(_numberRegen);
        }
        public bool ManaReduction(float minusMana)
        {
            if (_allMana >= minusMana)
            {
                _allMana -= minusMana;
                _manaComponent.fillAmount = _allMana / 100;
                return true;
            }
            else
                return false;
        }
        private void ManaRegen(float speedRegen)
        {
            _regenTime -= Time.deltaTime;

            if (_allMana < 100 && _regenTime <= 0)
            {
                _regenTime = _startRegenTime;
                _allMana += speedRegen;
                _manaComponent.fillAmount = _allMana / 100;
            }
        }
    }
}
