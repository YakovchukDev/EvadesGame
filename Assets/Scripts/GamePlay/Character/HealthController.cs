using System;
using Menu.SelectionClass;
using Menu.Settings;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace GamePlay.Character
{
    public class HealthController : MonoBehaviour
    {
        public int HpNumber = 1;
        [SerializeField] private AudioSource _dieSound;
        [SerializeField] private AudioMixerGroup _audioMixer;
        public float _immortalityTime;
        public bool _immortality;
        public static Action OnZeroHp;
        public static event Action OnBackToSafeZone;
        public static event Action<int> HealthPanelUpdate;    
        
        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                HpNumber = 3;
                HealthPanelUpdate?.Invoke(HpNumber);
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                HpNumber = 1;
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
                HpNumber--;
                if (SelectionClassView.WhatPlaying == "Level")
                {
                    if(HpNumber != 0)
                    {
                        OnBackToSafeZone?.Invoke();
                    }
                    HealthPanelUpdate?.Invoke(HpNumber);
                }
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
            if (HpNumber <= 0)
            {
                Debug.Log("allo blet");
                Time.timeScale = 0;
                _dieSound.Play();
                _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
                OnZeroHp?.Invoke();
            }
        }
    }
}