using System;
using System.Collections.Generic;
using Joystick_Pack.Examples;
using Menu.Settings;
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
        private GameObject _killer;
        private Vector3 _diePosition;
        public static event Action<int> UpdateHearts;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
            {
                if (_spellNumber >= 1)
                {
                    foreach (var spell in _spell1)
                    {
                        spell.interactable = true;
                    }

                    _killer = other.gameObject;
                    _diePosition = transform.position;
                }
            }
        }

        public void Respawn()
        {
            if (_spellNumber >= 1 && _healthController.HpNumber > 0)
            {
                _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
                _killer.SetActive(false);
                transform.position = _diePosition;
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                _healthController.HpNumber++;
                UpdateHearts?.Invoke(_healthController.HpNumber);
                Time.timeScale = 1;
                _spellNumber--;
                _respawnParticle.SetActive(true);
                _healthController._immortalityTime = 0;
                _healthController._immortality = true;
                foreach (var spell in _spell1)
                {
                    spell.interactable = false;
                }

                SelectUIPosition diePanel = FindObjectOfType<SelectUIPosition>();
                diePanel.DieClosed();
            }
        }
    }
}