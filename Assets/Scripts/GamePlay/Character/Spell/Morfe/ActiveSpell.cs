using System.Collections.Generic;
using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.Morfe
{
    public class ActiveSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private List<Transform> _patronPoint;
        [SerializeField] private List<GameObject> _minimizePatrons;
        [SerializeField] private List<GameObject> _deactivatingPatrons;
        [SerializeField] private GameObject _minimizePatron;
        [SerializeField] private GameObject _deactivatingPatron;
        [SerializeField] private float _manaCostMinimize;
        [SerializeField] private float _manaCostDeactivating;
        [SerializeField] private Animator _animator; 
        private float _minimizeRotation = -12.5f;
        private float _deactivatingRotation = -12.5f;
        private float _timeReloadFirstSpell;
        private float _timeReloadSecondSpell;
        private static readonly int Morfe = Animator.StringToHash("Morfe");
        private static readonly int Fire = Animator.StringToHash("Fire");

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _manaCostMinimize = 20;
                _manaCostDeactivating = 20;
                _timeReloadFirstSpell=6;
                _timeReloadSecondSpell=6;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _manaCostMinimize = 10;
                _manaCostDeactivating = 10;
                _timeReloadFirstSpell=3;
                _timeReloadSecondSpell=3;
            }

            for (var i = 0; i < 5; i++)
            {
                _minimizePatrons.Add(Instantiate(_minimizePatron, transform.position, transform.rotation));
                _minimizePatrons[i].SetActive(false);
            }

            for (var i = 0; i < 5; i++)
            {
                _deactivatingPatrons.Add(Instantiate(_deactivatingPatron, transform.position, transform.rotation));
                _deactivatingPatrons[i].SetActive(false);
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
                        _manaCostMinimize -= 2f;
                        _timeReloadFirstSpell-=0.6f;
                    }

                    CharacterUpdate.CanSpell2Update = false;
                }

                if (CharacterUpdate.CanSpell1Update)
                {
                    for (int i = 0; i < CharacterUpdate.NumberSpell1Update; i++)
                    {
                        _manaCostDeactivating -= 2f;
                        _timeReloadSecondSpell-=0.6f;
                    }

                    CharacterUpdate.CanSpell1Update = false;
                }
            }
        }

        public void MinimizeSpell()
        {
            if (_reloadSpell._canUseSpellFirst)
            {
                _reloadSpell.ReloadFirstSpell(_timeReloadFirstSpell);
                if (_manaController.ManaReduction(_manaCostMinimize))
                {
                    _animator.SetTrigger(Fire);

                    _minimizeRotation = -12.5f;
                    foreach (var minimizePatrons in _minimizePatrons)
                    {
                        minimizePatrons.transform.rotation = Quaternion.identity;
                    }

                    /*foreach (var minimizePatrons in _minimizePatrons)
                    {
                        minimizePatrons.transform.position = _patronPoint[minimizePatrons].position;
                        minimizePatrons.transform.rotation = transform.rotation;
                        minimizePatrons.transform.Rotate(0, _minimizeRotation += 5, 0);
                        minimizePatrons.SetActive(true);
                    }*/

                    for (int i = 0; i < _minimizePatrons.Count; i++)
                    {
                       _minimizePatrons[i].transform.position = _patronPoint[i].position;
                       _minimizePatrons[i].transform.rotation = transform.rotation;
                       _minimizePatrons[i].transform.Rotate(0, _minimizeRotation += 5, 0);
                       _minimizePatrons[i].SetActive(true);
                    }
                }
            }
        }

        public void DeactivatingSpell()
        {
            if (_reloadSpell._canUseSpellSecond)
            {
                _reloadSpell.ReloadSecondSpell(_timeReloadSecondSpell);
                if (_manaController.ManaReduction(_manaCostDeactivating))
                {
                    _animator.SetTrigger(Fire);
                    
                    _deactivatingRotation = -12.5f;
                    foreach (var deactivatingPatrons in _deactivatingPatrons)
                    {
                        deactivatingPatrons.transform.rotation = Quaternion.identity;
                    }

                    /*foreach (var deactivatingPatrons in _deactivatingPatrons)
                    {
                        deactivatingPatrons.transform.position = transform.position;
                        deactivatingPatrons.transform.rotation = transform.rotation;
                        deactivatingPatrons.transform.Rotate(0, _deactivatingRotation += 5, 0);
                        deactivatingPatrons.SetActive(true);
                    }*/
                    for (int i = 0; i < _deactivatingPatrons.Count; i++)
                    {
                        _deactivatingPatrons[i].transform.position = _patronPoint[i].position;
                        _deactivatingPatrons[i].transform.rotation = transform.rotation;
                        _deactivatingPatrons[i].transform.Rotate(0, _deactivatingRotation += 5, 0);
                        _deactivatingPatrons[i].SetActive(true);
                    }
                }
            }
        }
    }
}