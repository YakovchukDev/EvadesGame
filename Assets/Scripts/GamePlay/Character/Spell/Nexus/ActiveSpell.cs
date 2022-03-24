using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.Nexus
{
    public class ActiveSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _invulnerableField;
        private float _manaCost;
        private float _timeReloadSecondSpell;

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _manaCost = 25;
                _timeReloadSecondSpell = 6;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _manaCost = 15;
                _timeReloadSecondSpell = 3;
            }
        }

        private void Update()
        {
            CheckSpellUpdate();
        }

        private void CheckSpellUpdate()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                if (CharacterUpdate.CanSpell2Update)
                {
                    for (int i = 0; i < CharacterUpdate.NumberSpell2Update; i++)
                    {
                        _manaCost -= 2f;
                        _timeReloadSecondSpell -= 0.6f;
                    }


                    CharacterUpdate.CanSpell2Update = false;
                }
            }
        }

        public void InvulnerableField()
        {
            if (_reloadSpell._canUseSpellSecond)
            {
                _reloadSpell.ReloadSecondSpell(_timeReloadSecondSpell);
                if (_manaController.ManaReduction(_manaCost))
                {
                    _invulnerableField.transform.position = transform.position;
                    _invulnerableField.SetActive(true);
                }
            }
        }
    }
}