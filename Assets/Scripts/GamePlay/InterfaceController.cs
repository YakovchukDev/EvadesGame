using GamePlay.Enemy.Spawner;
using Menu;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay
{
    public class InterfaceController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timer;
        public static float _time;

        private void Update()
        {
            Timer();
        }

        private void Timer()
        {
            _time += Time.deltaTime;
            _timer.text = $"Time:{Mathf.Round(_time)}";
        }

        public void OnPause()
        {
            Time.timeScale = 0;
        }

        public void OffPause()
        {
            Time.timeScale = 1;
        }

        public void ExitButton()
        {
            SceneManager.LoadScene("Menu");

            Time.timeScale = 1;
            _time = 0;
            InfinityEnemySpawner.SpawnNumber = 0;
            TimeSave();
        }

        public static void TimeSave()
        {
            if (SelectionClassView._characterType == "Just")
            {
                if (_time > PlayerPrefs.GetFloat("JustTime"))
                {
                    PlayerPrefs.SetFloat("JustTime", _time);
                }
            }
            else if (SelectionClassView._characterType == "Necro")
            {
                if (_time > PlayerPrefs.GetFloat("NecroTime"))
                {
                    PlayerPrefs.SetFloat("NecroTime", _time);
                }
            }
            else if (SelectionClassView._characterType == "Morfe")
            {
                if (_time > PlayerPrefs.GetFloat("MorfeTime"))
                {
                    PlayerPrefs.SetFloat("MorfeTime", _time);
                }
            }
            else if (SelectionClassView._characterType == "Neo")
            {
                if (_time > PlayerPrefs.GetFloat("NeoTime"))
                {
                    PlayerPrefs.SetFloat("NeoTime", _time);
                }
            }
            else if (SelectionClassView._characterType == "Invulnerable")
            {
                if (_time > PlayerPrefs.GetFloat("InvulnerableTime"))
                {
                    PlayerPrefs.SetFloat("InvulnerableTime", _time);
                }
            }
            else if (SelectionClassView._characterType == "Nexus")
            {
                if (_time > PlayerPrefs.GetFloat("NexusTime"))
                {
                    PlayerPrefs.SetFloat("NexusTime", _time);
                }
            }
        }
    }
}