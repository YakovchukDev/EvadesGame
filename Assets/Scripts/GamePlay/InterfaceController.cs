using GamePlay.Enemy.Spawner;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class InterfaceController : MonoBehaviour
    {
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
            if (SelectionClassView._characterType == "Just")
            {
                if (Time > PlayerPrefs.GetFloat("JustTime"))
                {
                    PlayerPrefs.SetFloat("JustTime", Time);
                }
            }
            else if (SelectionClassView._characterType == "Necro")
            {
                if (Time > PlayerPrefs.GetFloat("NecroTime"))
                {
                    PlayerPrefs.SetFloat("NecroTime", Time);
                }
            }
            else if (SelectionClassView._characterType == "Morfe")
            {
                if (Time > PlayerPrefs.GetFloat("MorfeTime"))
                {
                    PlayerPrefs.SetFloat("MorfeTime", Time);
                }
            }
            else if (SelectionClassView._characterType == "Neo")
            {
                if (Time > PlayerPrefs.GetFloat("NeoTime"))
                {
                    PlayerPrefs.SetFloat("NeoTime", Time);
                }
            }
            else if (SelectionClassView._characterType == "Invulnerable")
            {
                if (Time > PlayerPrefs.GetFloat("InvulnerableTime"))
                {
                    PlayerPrefs.SetFloat("InvulnerableTime", Time);
                }
            }
            else if (SelectionClassView._characterType == "Nexus")
            {
                if (Time > PlayerPrefs.GetFloat("NexusTime"))
                {
                    PlayerPrefs.SetFloat("NexusTime", Time);
                }
            }
        }
    }
}