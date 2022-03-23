using GamePlay.Enemy.Spawner;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class InterfaceController : SelectionClassView
    { private static string[] _characterObject =
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
        }

        public void OffPause()
        {
            UnityEngine.Time.timeScale = 1;
        }

        public void ExitButton()
        {
            SceneManager.LoadScene("Menu");

            UnityEngine.Time.timeScale = 1;
            Time = 0;
            InfinityEnemySpawner.SpawnNumber = 0;
            TimeSave();
        }

        public static void TimeSave()
        {
            if (Time > PlayerPrefs.GetFloat(_characterObject[CharacterType]))
            {
                PlayerPrefs.SetFloat(_characterObject[CharacterType], Time);
            }
        }
    }
}