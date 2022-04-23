using Menu.SelectionClass;
using Menu.Settings;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace GamePlay.Character
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private SelectUIPosition _selectUIPosition;
        [SerializeField] private int _hpNumber = 1;
        [SerializeField] private AudioSource _dieSound;
        [SerializeField] private AudioMixerGroup _audioMixer;
        public static float ImmortalityTime;
        public static bool Immortality;
        [SerializeField] private Animator _leftDieAnimator;
        [SerializeField] private Animator _rightDieAnimator;
       

        public int HpNumber { get; set; }

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                _hpNumber = 3;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                _hpNumber = 1;
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
                transform.localScale = Vector3.one;
                _hpNumber--;
                Immortality = true;
                HpChecker();
            }
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
            if (_hpNumber <= 0)
            {
                Time.timeScale = 0;
                InfinityInterfaceController.TimeSave();
                _dieSound.Play();
                _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
                _selectUIPosition.SelectDiePanelPosition();
                DieOpen();
            }
        }
        public void DieOpen()
        {
            _leftDieAnimator.SetInteger("LeftDie", 0);
            _rightDieAnimator.SetInteger("RightDie", 0);
        }
    }
}