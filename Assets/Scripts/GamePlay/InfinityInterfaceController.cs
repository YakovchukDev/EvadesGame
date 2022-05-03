using System;
using GamePlay.Character;
using GamePlay.Enemy.Spawner;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class InfinityInterfaceController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;

        private static readonly string[] CharacterObject =
        {
            "WeakTime", "NecroTime", "ShooterTime", "NeoTime",
            "TankTime", "NecromusTime"
        };

        [SerializeField] private TMP_Text _timer;
        public static float Time;

        private void Start()
        {
            HealthController.OnZeroHp += TimeSave;
        }

        private void Update()
        {
            Timer();
        }


        private void Timer()
        {
            Time += UnityEngine.Time.deltaTime;
            _timer.text = $"Time:{Mathf.Round(Time)}";
        }

        public void OnPause()
        {
            UnityEngine.Time.timeScale = 0;
            _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
            _audioMixer.audioMixer.SetFloat("ImportantVolume", -80);
        }

        public void OffPause()
        {
            UnityEngine.Time.timeScale = 1;
            _audioMixer.audioMixer.SetFloat("ImportantVolume",
                Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("ImportantVolume")));
            _audioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectVolume")));
        }


        public void ExitButton()
        {
            TimeSave();
            SceneManager.LoadScene("Menu");
            UnityEngine.Time.timeScale = 1;
            Time = 0;
            InfinityEnemySpawner.SpawnNumber = 0;
        }

        public static void TimeSave()
        {
            if (Time > PlayerPrefs.GetFloat(CharacterObject[SelectionClassView.CharacterType]))
            {
                PlayerPrefs.SetFloat(CharacterObject[SelectionClassView.CharacterType], Time);
            }
        }

        private void OnDestroy()
        {
            HealthController.OnZeroHp -= TimeSave;
        }
    }
}