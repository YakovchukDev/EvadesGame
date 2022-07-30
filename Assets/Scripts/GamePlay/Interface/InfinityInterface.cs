using DG.Tweening;
using GamePlay.Character;
using GamePlay.Character.Spell;
using GamePlay.Enemy.ForInfinity.Spawner;
using Map.Coins;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Interface
{
    public class InfinityInterface : InterfaceController
    {
        [SerializeField] private CoinController _coinController;
        [SerializeField] private Image _leftMana;
        [SerializeField] private Image _rightMana;

        private bool _isSave;

        private readonly string[] _characterObject =
        {
            "WeakTime", "NecroTime", "ShooterTime", "NeoTime",
            "TankTime", "NecromusTime"
        };

        [SerializeField] private TMP_Text _timer;
        public float _time;

        private void OnEnable()
        {
            HealthController.OnZeroHp += SaveData;
            ManaController.UpdateManaView += ManaViewChanger;
        }

        private void OnDisable()
        {
            HealthController.OnZeroHp -= SaveData;
            ManaController.UpdateManaView += ManaViewChanger;
        }

        private void Update()
        {
            Timer();
        }

        private void ManaViewChanger(float value)
        {
            _leftMana.DOFillAmount(value, 1);
            _rightMana.DOFillAmount(value, 1);
        }

        private void Timer()
        {
            _time += Time.deltaTime;
            _timer.text = Mathf.Round(_time).ToString();
        }

        public override void ExitAndSave()
        {
            base.ExitAndSave();
            SaveData();
            _time = 0;
            InfinityEnemySpawner.SpawnNumber = 0;
        }

        public override void Restart()
        {
            base.Restart();
            InfinityEnemySpawner.SpawnNumber = 0;
            _time = 0;
            SaveData();
            _isSave = false;
        }

        private void SaveData()
        {
            if (!_isSave)
            {
                //save time
                if (_time > PlayerPrefs.GetFloat(_characterObject[SelectionClassView.CharacterType]))
                {
                    PlayerPrefs.SetFloat(_characterObject[SelectionClassView.CharacterType], _time);
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