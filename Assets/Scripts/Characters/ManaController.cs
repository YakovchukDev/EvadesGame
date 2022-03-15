using UnityEngine;
using UnityEngine.UI;
using System.Collections;
namespace Character
{
    public class ManaController : MonoBehaviour
    {
        [SerializeField] private Image _manaComponent;

        private float _manaController = 100;
        private float _regenTime;


        private void Update()
        {
            ManaRegen(1f);
        }
        public bool ManaReduction(float minusMana)
        {
            if (_manaController >= minusMana)
            {
                _manaController -= minusMana;
                _manaComponent.fillAmount = _manaController/100;
                return true;
            }
            else
                return false;
        }
        private void ManaRegen(float speedRegen)
        {
            _regenTime += Time.deltaTime;

            if (_manaController < 100&& _regenTime>2)
            {
                _regenTime = 0;
                   _manaController += speedRegen;
                _manaComponent.fillAmount = _manaController / 100;
            }
        }
    }
}
