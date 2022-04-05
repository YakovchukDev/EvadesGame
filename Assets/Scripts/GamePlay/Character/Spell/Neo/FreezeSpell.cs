using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.Neo
{
    public class FreezeSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _freezeField;
        [SerializeField] private GameObject _gravityRadius;
        [SerializeField] private AudioSource _freezeSound;
        private float _maxSize;
        private float _manaCost;
        private float _timeExistenceAfterMaxSize;
        private float _allTimeExistence = 2.4f;
        private float _size;
        private bool _goFreeze;
        private float _timeReloadFirstSpell;
        private float _levelSpell1;

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _manaCost = 40;
                _maxSize = 4;
                _timeReloadFirstSpell = 8;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _manaCost = 30;
                _maxSize = 8;
                _timeReloadFirstSpell = 4;
            }

            _freezeField.SetActive(false);
        }

        private void Update()
        {
            if (_goFreeze)
            {
                OnFreezeField(_maxSize);
            }

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
                        _maxSize += 0.8f;
                        _timeReloadFirstSpell -= 0.8f;
                        _levelSpell1++;
                    }

                    CharacterUpdate.CanSpell1Update = false;
                }
            }
        }

        public void FreezeField()
        {
            if (_goFreeze == false)
            {
                if (_reloadSpell._canUseSpellFirst)
                {
                    _reloadSpell.ReloadFirstSpell(_timeReloadFirstSpell);
                    if (_manaController.ManaReduction(_manaCost))
                    {
                        _goFreeze = true;
                        _freezeSound.Play();
                    }
                }
            }
        }

        private void OnFreezeField(float maxSize)
        {
            _freezeField.SetActive(true);
            _gravityRadius.SetActive(false);
            _size += 4 * Time.deltaTime;
            _freezeField.transform.localScale = new Vector3(_size, transform.localScale.y, _size);
            if (_size >= maxSize)
            {
                _size = maxSize;
                _timeExistenceAfterMaxSize += Time.deltaTime;
                if (_timeExistenceAfterMaxSize >= 1 + _allTimeExistence)
                {
                    _freezeField.SetActive(false);
                    _gravityRadius.SetActive(true);

                    _size = 1;
                    _freezeField.transform.localScale = new Vector3(_size, transform.localScale.y, _size);
                    _goFreeze = false;
                    _timeExistenceAfterMaxSize = 0;
                    _allTimeExistence = 2.4f;
                }
            }
            else
            {
                _allTimeExistence -= Time.deltaTime;
            }
        }
    }
}