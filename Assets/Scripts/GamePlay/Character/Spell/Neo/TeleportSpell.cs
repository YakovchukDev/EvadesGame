using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.Neo
{
    public class TeleportSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _splashParticle;
        [SerializeField] private GameObject _indicator;
        [SerializeField] private AudioSource _teleportSound;
        private float _manaCost;
        private float _teleportationLength;
        private float _timeReloadSecondSpell;
        private float _levelSpell2;

        private void Start()
        {
            _indicator.SetActive(false);
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _manaCost = 20;
                _teleportationLength = 6;
                _timeReloadSecondSpell = 4;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _manaCost = 10;
                _teleportationLength = 6;
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

        public void TeleportPointerDown()
        {
            _indicator.SetActive(true);
            Raycast(_indicator);
        }
       
        public void TeleportPointerUp()
        {
            _indicator.SetActive(false);
        }

        public void TeleportSkill()
        {
            if (_reloadSpell._canUseSpellSecond && _manaController.ManaReduction(_manaCost))
            {
                _reloadSpell.ReloadSecondSpell(_timeReloadSecondSpell);

                _teleportSound.Play();
                Instantiate(_splashParticle, transform.position, Quaternion.identity);
                Raycast(gameObject);

            }
        }
        private void Raycast(GameObject objectTransform)
        {
            Ray ray = new Ray( objectTransform.transform.position,  objectTransform.transform.forward);
            if (Physics.Raycast(ray, out var hit))
            {
                if (Physics.Raycast(ray, out hit, _teleportationLength))
                {
                    if (hit.transform.gameObject.CompareTag("Wall"))
                    {
                        objectTransform.transform.position = hit.point;
                        objectTransform.transform.position -= objectTransform.transform.forward;
                    }
                    else
                    {
                        objectTransform.transform.position +=  transform.forward * _teleportationLength;
                    }
                }
                else
                {
                    objectTransform.transform.position +=  transform.forward * _teleportationLength;
                }
            }
        }
    }
}