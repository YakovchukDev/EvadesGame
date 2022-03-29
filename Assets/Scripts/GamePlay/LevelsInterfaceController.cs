using GamePlay.Enemy.Spawner;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class LevelsInterfaceController : SelectionClassView
    {
        [SerializeField] private AudioMixerGroup _audioMixer;

        private static readonly string[] CharacterObject =
        {
            "JustTime", "NecroTime", "MorfeTime", "NeoTime",
            "InvulnerableTime", "NexusTime"
        };

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

            Time.timeScale = 1;
            InfinityEnemySpawner.SpawnNumber = 0;
            _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
        }
    }
}