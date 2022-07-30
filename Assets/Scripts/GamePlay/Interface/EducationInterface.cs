using Education.Level;
using Map.Coins;
using TMPro;
using UnityEngine;

namespace GamePlay.Interface
{
    public class EducationInterface : InterfaceController
    {
        [SerializeField] private CoinController _coinController;
        [SerializeField] private GameObject _particleAfterStar;
        [SerializeField] private TMP_Text _starCountText;
        public int CountGetStar { get; private set; }

        private void Start()
        {
            EducationStarControl.OnGetStar += GetStar;
            _starCountText.text = CountGetStar.ToString();
        }
        private void OnDisable()
        {
            EducationStarControl.OnGetStar -= GetStar;
        }

        private void GetStar(Vector3 starPosition)
        {
            _particleAfterStar.transform.position = starPosition;
            _particleAfterStar.SetActive(true);
            CountGetStar++;
            _starCountText.text = CountGetStar.ToString();
        }

        public override void ExitAndSave()
        {
            base.ExitAndSave();
            PlayerPrefs.SetInt("Education", 1);
        }

        public void FinishExit()
        {
            base.ExitAndSave();
            PlayerPrefs.SetInt("Education", 1);
            SaveCoins();
        }

        private void SaveCoins()
        {
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
        }
    }
}