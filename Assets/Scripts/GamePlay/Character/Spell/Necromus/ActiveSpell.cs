using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.Necromus
{
    public class ActiveSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _invulnerableField;
        [SerializeField] private AudioSource _invulnerableAudio;
        [SerializeField]  private GameObject _indicator;
        private float _manaCost;
        private float _timeReloadSecondSpell;
        private float _levelSpell1;

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _manaCost = 45;
                _timeReloadSecondSpell = 6;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _manaCost = 25;
                _timeReloadSecondSpell = 3;
            }
            _indicator.SetActive(false);
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
                    if (_levelSpell1 < CharacterUpdate.NumberSpell1Update)
                    {
                        _manaCost -= 4f;
                        _timeReloadSecondSpell -= 0.6f;
                        _levelSpell1++;
                    }


                    CharacterUpdate.CanSpell1Update = false;
                }
            }
        }
        public void InvulnerablePointerDown()
        {
            _indicator.SetActive(true);
        }
       
        public void InvulnerablePointerUp()
        {
            _indicator.SetActive(false);
        }
        public void InvulnerableField()
        {
            if ( _reloadSpell._canUseSpellSecond&&_manaController.ManaReduction(_manaCost))
            {
                _reloadSpell.ReloadSecondSpell(_timeReloadSecondSpell);

                _invulnerableAudio.Play();
                _invulnerableField.transform.position = transform.position;
                _invulnerableField.SetActive(true);
            }
        }
    }
}