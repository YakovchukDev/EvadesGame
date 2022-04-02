using Joystick_Pack.Examples;
using Menu.SelectionClass;
using UnityEngine;

namespace GamePlay.Character.Spell.SpeederInvulnerable
{
    public class AccelerateSpell : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerExample _joystickPlayerExample;
        [SerializeField] private ManaController _manaController;
        [SerializeField] private GameObject _spellImage;
        [SerializeField] private GameObject _accelerationParticle;
        [SerializeField] private AudioSource _accelerateSound;
        [SerializeField] private AudioSource _windSound;
        [SerializeField] private Animator _animator;
        private float _speedAccelerate;
        private float _maxManaCost;
        private float _time;
        private float _starterTime;
        private static readonly int Magmax = Animator.StringToHash("Magmax");
        private static readonly int Accelerate1 = Animator.StringToHash("Accelerate");
        private float _levelSpell1;
        public bool CheckAccelerate { get; set; }


        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _maxManaCost = 0.4f;
                _time = 0.1f;
                _speedAccelerate = 2;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _maxManaCost = 0.1f;
                _time = 0.1f;
                _speedAccelerate = 2;
            }

            _starterTime = _time;
        }

        private void Update()
        {
            if (CheckAccelerate)
            {
                _time -= Time.deltaTime;
                if (_time <= 0)
                {
                    if (!_manaController.ManaReduction(_maxManaCost))
                    {
                        CheckAccelerate = false;
                    }

                    _time = _starterTime;
                }
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
                        _maxManaCost -= 0.06f;
                        _levelSpell1++;
                    }

                    CharacterUpdate.CanSpell1Update = false;
                }
            }
        }


        public void Accelerate()
        {
            CheckAccelerate = !CheckAccelerate;

            if (CheckAccelerate)
            {
                JoystickPlayerExample.Speed *= _speedAccelerate;
                _spellImage.SetActive(false);
                _accelerationParticle.SetActive(true);
                _accelerateSound.Play();
                _windSound.Play();
                _animator.SetInteger(Magmax,1);
            }
            else
            {
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                _spellImage.SetActive(true);
                _accelerationParticle.SetActive(false);
                _accelerateSound.Stop();
                _windSound.Stop();
                _animator.SetInteger(Magmax,2);
            }
        }
    }
}