using System;
using Audio;
using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.Audio;

namespace GamePlay.Character
{
    public class HealthController : MonoBehaviour
    {
        public int _hpNumber = 1;
       // [SerializeField] private AudioSource _dieSound;
        [SerializeField] private AudioMixerGroup _audioMixer;
        public float _immortalityTime;
        public bool _immortality;
        public static Action OnZeroHp;
        private AudioManager _audioManager;

        private void Start()
        {
            _audioManager=AudioManager.Instanse;

            if (SelectionClassView.WhatPlaying == "Level")
            {
                _hpNumber = 3;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _hpNumber = 1;
            }

            _immortalityTime = 0;
            _immortality = true;
        }

        private void Update()
        {
            HpImmortality();
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
            {
                transform.localScale = Vector3.one;
                _hpNumber--;
                _immortality = true;
                HpChecker();
            }
        }

        private void HpImmortality()
        {
            if (_immortality)
            {
                _immortalityTime += Time.deltaTime;
                gameObject.layer = 11;
                if (_immortalityTime >= 1.6f)
                {
                    gameObject.layer = 6;
                    _immortality = false;
                    _immortalityTime = 0;
                }
            }
        }

        private void HpChecker()
        {
            if (_hpNumber <= 0)
            {
                Time.timeScale = 0;
                _audioManager.Play("Die");
                _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
                OnZeroHp?.Invoke();
            }
        }
    }
}