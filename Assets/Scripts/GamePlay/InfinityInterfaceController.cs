using GamePlay.Character;
using GamePlay.Enemy.ForInfinity.Spawner;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Map.Coins;

namespace GamePlay
{
    public class InfinityInterfaceController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private CoinController _coinController;
        private bool _isSave;

        private readonly string[] CharacterObject =
        {
            "WeakTime", "NecroTime", "ShooterTime", "NeoTime",
            "TankTime", "NecromusTime"
        };

        [SerializeField] private TMP_Text _timer;
        public float Time;

        private void OnEnable()
        {
            HealthController.OnZeroHp += SaveData;
            PanelAfterDie.RestartLevel += Restart;
            PanelAfterDie.ExitToMenu += Exit;
        }

        private void Disable()
        {
            PanelAfterDie.RestartLevel -= Restart;
            HealthController.OnZeroHp -= SaveData;
            PanelAfterDie.ExitToMenu -= Exit;
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
        }

        public void OffPause()
        {
            UnityEngine.Time.timeScale = 1;
            _audioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectVolume")));
        }

        public void ExitButton()
        {
            SaveData();
            SceneManager.LoadScene("Menu");
            UnityEngine.Time.timeScale = 1;
            Time = 0;
            InfinityEnemySpawner.SpawnNumber = 0;
        }

        private void Restart()
        {
            Time = 0;
            SaveData();
            _isSave = false;
        }

        private void Exit()
        {
            SaveData();
        }

        private void SaveData()
        {
            if (!_isSave)
            {
                //save time
                if (Time > PlayerPrefs.GetFloat(CharacterObject[SelectionClassView.CharacterType]))
                {
                    PlayerPrefs.SetFloat(CharacterObject[SelectionClassView.CharacterType], Time);
                }

                //save coins
                int coins = _coinController.GetCoinsResult();
                try
                {
                    if (PlayerPrefs.HasKey("Coins"))
                    {
                        int allCoins = PlayerPrefs.GetInt("Coins") + coins;
                        PlayerPrefs.SetInt("Coins", allCoins);
                    }
                    else
                    {
                        PlayerPrefs.SetInt("Coins", coins);
                    }
                }
                catch (UnityException exception)
                {
                    Debug.Log(exception.GetBaseException());
                }

                _isSave = true;
            }
        }
    }
}