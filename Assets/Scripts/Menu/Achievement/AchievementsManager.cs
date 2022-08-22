using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Menu.Achievement
{
    public class AchievementsManager : MonoBehaviour
    {
        [SerializeField] private bool _testMode;
        public bool ToLoop;
        public List<Achievement> Achievements;
        [SerializeField] private int _timeInterval;
        private string _path;
        private int _count;
        private bool _isInitialized;

        private void Start()
        {

            _isInitialized = false;
            _count = 0; 
            if (_testMode)
            {
                _path = Application.streamingAssetsPath + "/Achievements.json";
            }
            else
            {
                _path = Application.persistentDataPath + "/Achievements.json";
            }
            InitializeAchievement();
        }
        public void Update()
        {
            CheckAchievementCompletion();
        }
        private void OnApplicationPause(bool pause)
        {
            StartCoroutine(SaveAchievements());
        }
        private void OnApplicationQuit()
        {
             StartCoroutine(SaveAchievements());
        }
        private IEnumerator SaveAchievements()
        {
            if(Achievements == null)
            {
                InitializeAchievement();
            }
            yield return new WaitUntil(() => _path != null);
            if(!File.Exists(_path))
            {
                FileStream fileStream = File.Create(_path);
                fileStream.Close();
            }
            AchievementData achievementData = new AchievementData(Achievements);
            File.WriteAllText(_path, JsonUtility.ToJson(achievementData));
        }
        private void InitializeAchievement()
        {
            CreateListAchievement();
            if (File.Exists(_path))
            {
                AchievementData achievementsData = JsonUtility.FromJson<AchievementData>(File.ReadAllText(_path));
                for(int i = 0; i < Achievements.Count; i++)
                {
                    if(achievementsData.IsAchieveds.Length > i)
                    {
                        Achievements[i].Achieved = achievementsData.IsAchieveds[i];
                    }
                }
            }
            _isInitialized = true;
        }
        private void CreateListAchievement()
        {
            Achievements = new List<Achievement>();
            Achievements.Add(new Achievement(
                (object)10, (object)2,
                "Level 1 explorer",
                "complete 10 levels with 1 star or more",
                "get NEO character",
                CheckCompletedLevels,
                OpenCharacter));
            Achievements.Add(new Achievement(
                (object)20, (object)3,
                "Level 2 explorer",
                "complete 20 levels with 1 star or more",
                "get Tank character",
                CheckCompletedLevels,
                OpenCharacter));
            Achievements.Add(new Achievement(
                (object)100, 100,
                "You are resilience itself",
                "Stay alive for 100 seconds",
                "get Tank character",
                CheckCompletedLevels,
                AddCoins));
        }
        public void CheckAchievementCompletion()
        {
            if (Achievements != null && _isInitialized)
            {
                if (_count >= Achievements.Count)
                {
                    _count = 0;
                }
                if (Achievements[_count].UpdateCompletion())
                {
                    Achievements[_count].Achieved = true;
                }
                _count++;
            }
        }

        //task
        private bool CheckCompletedLevels(object target)
        {
            print($"check {target}");
            if (PlayerPrefs.HasKey("CompleteLevel"))
            {
                return PlayerPrefs.GetInt("CompleteLevel") >= (int)target;
            }
            return false;
        }
        private bool CheckSurviveRecord(object target)
        {
            float[] recordTime = new float[6];
            recordTime[0] = PlayerPrefs.GetFloat("WeakTime");
            recordTime[1] = PlayerPrefs.GetFloat("NecroTime");
            recordTime[2] = PlayerPrefs.GetFloat("ShooterTime");
            recordTime[3] = PlayerPrefs.GetFloat("NeoTime");
            recordTime[4] = PlayerPrefs.GetFloat("TankTime");
            recordTime[5] = PlayerPrefs.GetFloat("NecromusTime");
            for (int i = 0; i < 6; i++)
            {
                if (recordTime[i] >= (int)target)
                {
                    return PlayerPrefs.GetInt("CompleteLevel") >= (int)target;
                }
            }
            return false;
        }
        //reward
        private bool OpenCharacter(object index)
        {
            print($"Character open {index}");
            PlayerPrefs.SetInt($"Open{(int)index}", 1);
            return true;
        }
        private bool AddCoins(object coins)
        {
            if (PlayerPrefs.HasKey("Coins"))
            {
                int allCoins = PlayerPrefs.GetInt("Coins") + (int)coins;
                PlayerPrefs.SetInt("Coins", allCoins);
                return true;
            }
            else
            {
                PlayerPrefs.SetInt("Coins", (int)coins);
                return true;
            }
        }
        public void TestClick()
        {
            PlayerPrefs.SetInt("CompleteLevel", PlayerPrefs.GetInt("CompleteLevel") + 10);
        }
    }
}
