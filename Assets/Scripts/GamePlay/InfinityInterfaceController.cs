using GamePlay.Enemy.Spawner;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace GamePlay
{
    public class InfinityInterfaceController : SelectionClassView
    {
        [SerializeField] private AudioMixerGroup _audioMixer;

        private static readonly string[] CharacterObject =
        {
            "JustTime", "NecroTime", "MorfeTime", "NeoTime",
            "InvulnerableTime", "NexusTime"
        };

        [SerializeField] private TMP_Text _timer;
        public static float Time;

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
        }

        public void OffPause()
        {
            UnityEngine.Time.timeScale = 1;
            _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
        }

        public void ExitButton()
        {
            SceneManager.LoadScene("Menu");

            UnityEngine.Time.timeScale = 1;
            Time = 0;
            InfinityEnemySpawner.SpawnNumber = 0;
            TimeSave();
            _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
        }

        public static void TimeSave()
        {
            if (Time > PlayerPrefs.GetFloat(CharacterObject[CharacterType]))
            {
                PlayerPrefs.SetFloat(CharacterObject[CharacterType], Time);
            }
        }
    }
}