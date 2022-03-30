using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell
{
    public class ManaController : MonoBehaviour
    {
        [SerializeField] private Image _manaComponent;
        private float _allMana;
        private float _startRegenTime;
        private float _numberRegen;
        private float _regenTime;
        public static float Mana;
        public static float Regen;

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _allMana = 100;
                _numberRegen = 1;
                _startRegenTime = 1;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _allMana = 200;
                _numberRegen = 2;
                _startRegenTime = 1;
            }

            Mana = _allMana;
            Regen = _numberRegen;
        }

        private void Update()
        {
            if (_allMana < Mana)
            {
                _allMana = Mana;
            }

            if (_numberRegen < Regen)
            {
                _numberRegen = Regen;
            }

            ManaRegen(Regen);
        }

        public void SetAllMana(float allMana)
        {
            _allMana = allMana;
        }

        public bool ManaReduction(float minusMana)
        {
            if (Mana >= minusMana)
            {
                Mana -= minusMana;
                _manaComponent.fillAmount = Mana / _allMana;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ManaRegen(float speedRegen)
        {
            _regenTime -= Time.deltaTime;

            if (Mana < _allMana && _regenTime <= 0)
            {
                _regenTime = _startRegenTime;
                Mana += speedRegen;
                _manaComponent.fillAmount = Mana / _allMana;
            }
        }
    }
}