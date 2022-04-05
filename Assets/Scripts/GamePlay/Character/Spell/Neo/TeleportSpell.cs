using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.TeleportFreeze
{
    public class TeleportSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _splashParticle;
        [SerializeField] private AudioSource _teleportSound;
        private float _manaCost;
        private float _teleportationLength;
        private float _timeReloadSecondSpell;
        private float _levelSpell2;

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _manaCost = 20;
                _teleportationLength = 8;
                _timeReloadSecondSpell = 4;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _manaCost = 10;
                _teleportationLength = 8;
                _timeReloadSecondSpell = 1;
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
                    if (_levelSpell2 < CharacterUpdate.NumberSpell2Update)
                    {
                        _manaCost -= 2f;
                        _timeReloadSecondSpell -= 0.6f;
                        _levelSpell2++;
                    }

                    CharacterUpdate.CanSpell2Update = false;
                }
            }
        }

        public void TeleportSkill()
        {
            if (_reloadSpell._canUseSpellSecond)
            {
                _reloadSpell.ReloadSecondSpell(_timeReloadSecondSpell);
                if (_manaController.ManaReduction(_manaCost))
                {
                    _teleportSound.Play();
                    Instantiate(_splashParticle, transform.position, Quaternion.identity);
                    Ray ray = new Ray(transform.position, transform.forward);
                    if (Physics.Raycast(ray, out var hit))
                    {
                        if (Physics.Raycast(ray, out hit, _teleportationLength))
                        {
                            if (hit.transform.gameObject.CompareTag("Wall"))
                            {
                                transform.position = hit.point;
                                transform.position -= transform.forward;
                            }
                            else
                            {
                                transform.position += transform.forward * _teleportationLength;
                            }
                        }
                        else
                        {
                            transform.position += transform.forward * _teleportationLength;
                        }
                    }

                }
            }
        }
    }
}