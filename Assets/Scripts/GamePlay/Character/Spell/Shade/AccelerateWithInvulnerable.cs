using Joystick_Pack.Examples;
using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.Shade
{
    public class AccelerateWithInvulnerable : MonoBehaviour
    {
        [SerializeField] private HealthController _healthController;
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        private float _manaCost;
        private float _timeReloadSecondSpell;
        private float _levelSpell2;
        private float _speedAccelerate;
        private float _startAbilityDuration;

        private float _abilityDuration;

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _manaCost = 50;
                _timeReloadSecondSpell = 4;
                _speedAccelerate = 1.5f;
                _startAbilityDuration = 5;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _manaCost = 30;
                _timeReloadSecondSpell = 1;
                _speedAccelerate = 1.5f;
                _startAbilityDuration = 10;
            }
        }

        private void Update()
        {
            _abilityDuration -= Time.deltaTime;
            if (_abilityDuration <= 0)
            {
                _abilityDuration = _startAbilityDuration;
            }

            CheckSpellUpdate();
        }

        private void CheckSpellUpdate()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                if (CharacterUpdate.CanSpell2Update)
                {
                    if (_levelSpell2 < CharacterUpdate.NumberSpell2Update)
                    {
                        _manaCost -= 4f;
                        _timeReloadSecondSpell -= 0.6f;
                        _startAbilityDuration++;
                        _levelSpell2++;
                    }

                    CharacterUpdate.CanSpell2Update = false;
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
            {
            }
        }

        public void Accelerate()
        {
            if (_reloadSpell._canUseSpellSecond && _manaController.ManaReduction(_manaCost))
            {
                _reloadSpell.ReloadSecondSpell(_timeReloadSecondSpell);

                JoystickPlayerExample.Speed *= _speedAccelerate;
                _healthController.HpNumber++;
            }
        }
    }
}