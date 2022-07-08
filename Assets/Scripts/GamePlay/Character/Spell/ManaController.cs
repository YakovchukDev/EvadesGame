using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell
{
    public class ManaController : MonoBehaviour
    {
        [SerializeField] private Image _manaComponent;
        public static float AllMana;
        private float _startRegenTime;
        private float _numberRegen;
        private float _regenTime;
        private float _mana;
        public static float Regen;

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                AllMana = 100;
                _numberRegen = 1;
                _startRegenTime = 1;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                AllMana = 200;
                _numberRegen = 2;
                _startRegenTime = 1;
            }

            _mana = AllMana;
            Regen = _numberRegen;
        }

        private void Update()
        {
            if (AllMana < _mana)
            {
                AllMana = _mana;
            }

            if (_numberRegen < Regen)
            {
                _numberRegen = Regen;
            }

            ManaRegen(Regen);
        }

        public bool ManaReduction(float minusMana)
        {
            if (_mana >= minusMana)
            {
                _mana -= minusMana;
                _manaComponent.fillAmount = _mana / AllMana;
                return true;
            }
            return false;
        }

        private void ManaRegen(float speedRegen)
        {
            _regenTime -= Time.deltaTime;

            if (_mana < AllMana && _regenTime <= 0)
            {
                _regenTime = _startRegenTime;
                _mana += speedRegen;
                _manaComponent.fillAmount = _mana / AllMana;
            }
        }
    }
}