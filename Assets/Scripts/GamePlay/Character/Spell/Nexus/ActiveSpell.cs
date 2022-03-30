using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.Nexus
{
    public class ActiveSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _invulnerableField;
        [SerializeField] private AudioSource _invulnerableAudio;

        private float _manaCost;
        private float _timeReloadSecondSpell;
        private float _levelSpell1;

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
                if (CharacterUpdate.CanSpell1Update)
                {
                    if(_levelSpell1 < CharacterUpdate.NumberSpell1Update) 
                    {
                        _manaCost -= 2f;
                        _timeReloadSecondSpell -= 0.6f;
                        _levelSpell1++;
                    }


                    CharacterUpdate.CanSpell1Update = false;
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
                    _invulnerableAudio.Play();
                    _invulnerableField.transform.position = transform.position;
                    _invulnerableField.SetActive(true);
                }
            }
        }
    }
}