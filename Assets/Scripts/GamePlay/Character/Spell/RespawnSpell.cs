using System;
using System.Collections.Generic;
using Joystick_Pack.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace GamePlay.Character.Spell
{
    public class RespawnSpell : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerExample _joystickPlayerExample;
        [SerializeField] private HealthController _healthController;
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private GameObject _respawnParticle;
        [SerializeField] private List<Button> _spell1;
        [SerializeField] private int _spellNumber = 1;
        [SerializeField] private GameObject _readoutPanel;
        [SerializeField] private TMP_Text _readoutText;
        private float _readout = 3;
        private bool _forReadout;
        private GameObject _killer;
        public static event Action<int> UpdateHearts;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
            {
                if (_spellNumber >= 1)
                {
                    Time.timeScale = 0;
                    _forReadout = true;
                    _readoutPanel.SetActive(true);
                    foreach (var spell in _spell1)
                    {
                        spell.interactable = true;
                    }

                    _killer = other.gameObject;
                }
                else
                {
                    _healthController.LevelMinusHp();
                }
            }
        }

        private void Update()
        {
            if (_forReadout)
                Readout();
        }

        private void Readout()
        {
            _readout -= Time.unscaledDeltaTime;
            _readoutText.text = Mathf.Round(_readout).ToString();
            if (_readout <= 0 && _healthController.HpNumber <= 1)
            {
                Respawn();
            }
            else if (_readout <= 0)
            {
                _healthController.LevelMinusHp();
                Time.timeScale = 1;
                _readoutPanel.SetActive(false);
                _forReadout = false;
                foreach (var spell in _spell1)
                {
                    spell.interactable = false;
                }

                _readout = 3;
            }
        }

        public void Respawn()
        {
            if (_spellNumber >= 1)
            {
                Time.timeScale = 1;
                _readoutPanel.SetActive(false);
                _forReadout = false;
                _readout = 5;
                _spellNumber--;
                _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
                _killer.SetActive(false);
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                UpdateHearts?.Invoke(_healthController.HpNumber);
                _respawnParticle.SetActive(true);
                _healthController._immortalityTime = 0;
                _healthController._immortality = true;
                foreach (var spell in _spell1)
                {
                    spell.interactable = false;
                }
            }
        }
    }
}