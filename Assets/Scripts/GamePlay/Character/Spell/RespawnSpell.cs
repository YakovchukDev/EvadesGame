using System.Collections.Generic;
using Joystick_Pack.Examples;
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
        [SerializeField] private Animator _leftDieAnimator;
        [SerializeField] private Animator _rightDieAnimator;
        private GameObject _killer;
        private Vector3 _diePosition;

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
            if (_spellNumber >= 1)
            {
                _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
                _killer.SetActive(false);
                transform.position = _diePosition;
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                _healthController.HpNumber++;
                Time.timeScale = 1;
                _spellNumber--;
                _respawnParticle.SetActive(true);
                HealthController.ImmortalityTime = 0;
                HealthController.Immortality = true;
                foreach (var spell in _spell1)
                {
                    spell.interactable = false;
                }

                DieClosed();
            }
        }
        public void DieClosed()
        {
            _leftDieAnimator.SetInteger("LeftDie", 1);
            _rightDieAnimator.SetInteger("RightDie", 1);
        }
    }
}