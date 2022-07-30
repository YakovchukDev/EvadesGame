using System;
using System.Collections;
using Audio;
using GamePlay.Character.Spell;
using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.Audio;

namespace GamePlay.Character
{
    public class HealthController : MonoBehaviour
    {
        [HideInInspector] public int HpNumber = 1;
        [SerializeField] private AudioMixerGroup _audioMixer;
        private bool _restartComponent;
        private AudioManager _audioManager;
        public float ImmortalityTime;
        public bool Immortality;
        public static Action OnZeroHp;
        public static event Action OnBackToSafeZone;
        public static event Action<int> HealthPanelUpdate;
        public static event Action OnEducationDie;
        private void Start()
        {
            _audioManager = AudioManager.Instanse;
            _restartComponent = FindObjectOfType<RespawnSpell>();
            if (SelectionClassView.WhatPlaying == "Level")
            {
                HpNumber = 3;
                HealthPanelUpdate?.Invoke(HpNumber);
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                HpNumber = 1;
            }
            else if (SelectionClassView.WhatPlaying == "Education")
            {
                HpNumber = 9999;
                HealthPanelUpdate?.Invoke(HpNumber);
            }

            ImmortalityTime = 0;
            Immortality = true;
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
                if (!_restartComponent)
                {
                    HpNumber--;
                    if (SelectionClassView.WhatPlaying == "Level")
                    {
                        if (HpNumber != 0)
                        {
                            OnBackToSafeZone?.Invoke();
                        }

                        HealthPanelUpdate?.Invoke(HpNumber);
                    }
                    else if (SelectionClassView.WhatPlaying == "Education")
                    {
                        HealthPanelUpdate?.Invoke(HpNumber);
                    }

                    _audioManager.Play("Die");
                    HpChecker();

                    ImmortalityTime = 0;
                    Immortality = true;
                }
            }
        }

        public void LevelMinusHp()
        {
            HpNumber--;
            _audioManager.Play("Die");

            if (SelectionClassView.WhatPlaying == "Level")
            {
                if (HpNumber != 0)
                {
                    OnBackToSafeZone?.Invoke();
                }

                HealthPanelUpdate?.Invoke(HpNumber);
            }
            else if (SelectionClassView.WhatPlaying == "Education")
            {
                HealthPanelUpdate?.Invoke(HpNumber);
                if (HpNumber > 0)
                {
                    OnEducationDie?.Invoke();
                }
            }
            Immortality = true;
            HpChecker();
        }

        private void HpImmortality()
        {
            if (Immortality)
            {
                ImmortalityTime += Time.deltaTime;
                gameObject.layer = 11;
                if (ImmortalityTime >= 1.6f)
                {
                    gameObject.layer = 6;
                    Immortality = false;
                    ImmortalityTime = 0;
                }
            }
        }

        private void HpChecker()
        {
            if (HpNumber <= 0)
            {
                Time.timeScale = 0;
                _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
                OnZeroHp?.Invoke();
            }
        }
    }
}