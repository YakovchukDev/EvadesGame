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
        [SerializeField] private InteractionWithTag _interactionWithTag;
        [SerializeField] private HealthController _healthController;
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private GameObject _respawnParticle;
        [SerializeField] private List<Button> _spell1;
        [SerializeField] private int _spellNumber = 1;
        [SerializeField] private GameObject _readoutPanel;
        [SerializeField] private TMP_Text _readoutText;
        [SerializeField] private AudioSource _respawnSound;
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
                _respawnSound.Play();
                _audioMixer.audioMixer.SetFloat("ImportantVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("ImportantVolume")));
                _audioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectVolume")));
                _audioMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("MusicVolume")));
                _killer.SetActive(false);
                _interactionWithTag.Refresh();
                UpdateHearts?.Invoke(_healthController.HpNumber);
                _respawnParticle.SetActive(true);
                _healthController.ImmortalityTime = 0;
                _healthController.Immortality = true;
                foreach (var spell in _spell1)
                {
                    spell.interactable = false;
                }
            }
        }
    }
}