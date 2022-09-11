using Map.Coins;
using UnityEngine;

namespace Map.Data
{
    public class MainDataCollector
    {
        public Level Level;
        private CoinController _coinController;
        private int _levelNumber;

        public MainDataCollector()
        {
            Level = new Level();
            GiveDataAboutLevel();
        }

        public void SaveData()
        {
            try
            {
                SaveCoins();
                PlayerPrefs.SetString($"Level{_levelNumber}",
                    (Level.IsComplited ? "*" : " ") + (Level.UpStar ? "*" : " ") + (Level.MiddleStar ? "*" : " ") + (Level.DownStar ? "*" : " "));
                if (PlayerPrefs.HasKey("CompleteLevel"))
                {
                    int complitedLevelsCount = 0;
                    for (int levelNumber = 1; levelNumber <= 30; levelNumber++)
                    {
                        if (PlayerPrefs.HasKey($"Level{levelNumber}"))
                        {
                            string levelData = PlayerPrefs.GetString($"Level{levelNumber}");
                            complitedLevelsCount += levelData[0] == '*' ? 1 : 0;
                        }
                    }

                    PlayerPrefs.SetInt("CompleteLevel", complitedLevelsCount);
                }
                else
                {
                    PlayerPrefs.SetInt("CompleteLevel", 0);
                }
            }
            catch (UnityException exception)
            {
                Debug.Log(exception.GetBaseException());
            }
        }

        public void SaveCoins()
        {
            int coins = _coinController.GetCoinsResult();
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

        public void GiveDataAboutLevel()
        {
            try
            {
                _levelNumber = PlayerPrefs.GetInt("LevelNumber");
                if (PlayerPrefs.HasKey($"Level{_levelNumber}"))
                {
                    Level = new Level();
                    string stars = PlayerPrefs.GetString($"Level{_levelNumber}");
                    Level.IsComplited = stars[0] == '*' ? true : false;
                    Level.UpStar = stars[1] == '*' ? true : false;
                    Level.DownStar = stars[2] == '*' ? true : false;
                    Level.MiddleStar = stars[3] == '*' ? true : false;
                }
                else
                {
                    Level = new Level();
                }
            }
            catch
            {
                Level = new Level();
            }
        }

        public int GetLevelNumber() => _levelNumber;

        public void SetCoinController(CoinController coinController)
        {
            _coinController = coinController;
        }
    }
}