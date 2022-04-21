using System.IO;
using UnityEngine;

namespace Map
{
    public class MainDataCollector : MonoBehaviour
    {
        public Level Level = new Level();
        public int Coins = 0;
        [SerializeField] private string _path = @"B:\Програмирувание\Games\Evades\Assets\ProgressPlayer.json";
        private int _levelNumber = 0;
        public void SaveData()
        {
            try
            {
                SaveCoins();

                PlayerPrefs.SetString($"Level{_levelNumber}", (Level.IsComplited ? "*" : " ") + (Level.LeftStars ? "*" : " ") + (Level.RightStars ? "*" : " "));

                if(PlayerPrefs.HasKey("CompleteLevel"))
                {
                    int complitedLevels = PlayerPrefs.GetInt("CompleteLevel")+1;
                    PlayerPrefs.SetInt("CompleteLevel", complitedLevels);
                }
                else
                {
                    PlayerPrefs.SetInt("CompleteLevel", 0);
                }
            }
            catch(UnityException exception)
            {
                Debug.Log(exception.GetBaseException());
            }
        }
        public void SaveCoins()
        {
            try
            {
                if(PlayerPrefs.HasKey("Coins"))
                {
                    int coins = PlayerPrefs.GetInt("Coins") + Coins;
                    PlayerPrefs.SetInt("Coins", coins);
                }
                else
                {
                    PlayerPrefs.SetInt("Coins", Coins);
                }
            }
            catch(UnityException exception)
            {
                Debug.Log(exception.GetBaseException());
            }
        }
        public void GiveDataAboutLevel(int levelNumber)
        {
            _levelNumber = levelNumber;
            if(PlayerPrefs.HasKey($"Level{_levelNumber}"))
            {
                string stars = PlayerPrefs.GetString($"Level{_levelNumber}");
                Level = new Level();
                Level.IsComplited = stars[0] == '*' ? true : false;
                Level.LeftStars = stars[1] == '*' ? true : false;
                Level.RightStars = stars[2] == '*' ? true : false;
            }
            else
            {
                Level = new Level();
            }
        }
    }
}

/*
            byte[] buffer = new byte[255];
            FileStream stream = new FileStream(_path, FileMode.Open, FileAccess.Read);
            stream.Read(buffer, 0, buffer.Length);
            DataOfProgressPlayer data = JsonUtility.FromJson<DataOfProgressPlayer>(buffer.ToString());
            Level = data.Company.Levels[id];
*/

/*
            DataOfProgressPlayer data;
            string jsonData;
            byte[] buffer = new byte[255];
            bool isFirstEnter = File.Exists(_path);

            try
            {
                FileStream stream;
                if(!isFirstEnter)
                {
                    stream = new FileStream(_path, FileMode.Create);
                    data = new DataOfProgressPlayer(40);//1------------------------------------------------------
                    stream.Close();
                }
                else
                {
                    stream = new FileStream(_path, FileMode.Open, FileAccess.Read);
                    stream.Read(buffer, 0, buffer.Length);
                    jsonData = System.Text.Encoding.Default.GetString(buffer);
                    data = JsonUtility.FromJson<DataOfProgressPlayer>(jsonData);
                    stream.Close();
                }
                //update info about level
                data.Company.Levels[_idLevel] = Level;
                data.QuantityCoins += Coins;

                jsonData = JsonUtility.ToJson(data);
                buffer = System.Text.Encoding.Default.GetBytes(jsonData);
                stream = new FileStream(_path, FileMode.Open, FileAccess.Write);
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }
            catch(UnityException exception)
            {
                Debug.Log(exception.GetBaseException());
            }
*/