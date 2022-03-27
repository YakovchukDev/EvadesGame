using Joystick_Pack.Examples;
using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell.SpeederInvulnerable
{
    public class InvulnerableSpell : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerExample _joystickPlayerExample;
        [SerializeField] private ManaController _manaController;
        [SerializeField] private AccelerateSpell _accelerateSpell;
        [SerializeField] private GameObject _gravityRadius;
        [SerializeField] private GameObject _invulnerableParticle;
        [SerializeField] private GameObject _spellImage;
        [SerializeField] private GameObject _otherSpellImage;
        [SerializeField] private Button _otherSpellButton;
        [SerializeField] private AudioSource _invulnerableSound;
        [SerializeField] private Animator _animator;
        private float _maxManaCost;
        private float _time;
        private float _starterTime;
        private bool _checkMover;
        private static readonly int Magmax = Animator.StringToHash("Magmax");

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _maxManaCost = 2;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _maxManaCost = 1;
            }

            _starterTime = 0.1f;
        }

        private void Update()
        {
            if (_checkMover)
            {
                _time -= Time.deltaTime;
                if (_time <= 0)
                {
                    if (!_manaController.ManaReduction(_maxManaCost))
                    {
                        _checkMover = true;
                        Invulnerable();
                    }

                    _time = _starterTime;
                }

                gameObject.layer = 11;
            }
            else
            {
                gameObject.layer = 6;
            }

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
                        _maxManaCost -= 0.2f;
                    }

                    CharacterUpdate.CanSpell2Update = false;
                }
            }
        }

        public void Invulnerable()
        {
            _checkMover = !_checkMover;

            if (_checkMover)
            {
                _accelerateSpell.CheckAccelerate = true;
                _accelerateSpell.Accelerate();
                JoystickPlayerExample.Speed = 0;
                gameObject.layer = 11;
                _gravityRadius.layer = 11;
                _spellImage.SetActive(false);
                _otherSpellImage.SetActive(false);
                _otherSpellButton.interactable = false;
                _invulnerableParticle.SetActive(true);
                _invulnerableSound.Play();
                _animator.SetInteger(Magmax,3);
            }
            else
            {
                _accelerateSpell.CheckAccelerate = false;
                _accelerateSpell.Accelerate();
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                gameObject.layer = 6;
                _gravityRadius.layer = 6;
                _spellImage.SetActive(true);
                _otherSpellImage.SetActive(true);
                _otherSpellButton.interactable = true;
                _invulnerableParticle.SetActive(false);
                _accelerateSpell.Accelerate();
                _invulnerableSound.Stop();
                _animator.SetInteger(Magmax,0);
            }
        }
    }
}