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
        private int _count;
        private bool _isInitialized;

        private void Start()
        {
            _isInitialized = false;
            InitializeAchievement();
        }

        public void Update()
        {
            CheckAchievementCompletion();
        }

        private void OnApplicationPause(bool pause)
        {
            SaveAchievements();
        }

        private void OnApplicationQuit()
        {
            SaveAchievements();
        }

        private void SaveAchievements()
        {
            if (Achievements == null)
            {
                InitializeAchievement();
            }

            for (int i = 0; i < Achievements.Count; i++)
            {
                PlayerPrefs.SetInt($"Achievement{i}", Achievements[i].Achieved ? 1 : 0);
            }
        }

        private void InitializeAchievement()
        {
            CreateListAchievement();

            for (int i = 0; i < Achievements.Count; i++)
            {
                if (PlayerPrefs.HasKey($"Achievement{i}"))
                {
                    Achievements[i].Achieved = PlayerPrefs.GetInt($"Achievement{i}") == 1;
                }
            }

            _isInitialized = true;
        }

        private void CreateListAchievement()
        {
            Achievements = new List<Achievement>();
            Achievements.Add(new Achievement(
                (object) 10, (object) 1,
                new Translator("Level 1 explorer", "Дослідник 1-го рівня", "Исследователь 1-го уровня"),
                new Translator("Complete 10 company levels.", "Пройдіть 10 рівнів компанії.",
                    "Пройдите 10 уровней компании."),
                new Translator("You get a Necro character!", "Ти отримуєш персонажа Necro!",
                    "Ты Получаешь персонажа Necro!"),
                CheckCompletedLevels,
                OpenCharacter));
            Achievements.Add(new Achievement(
                (object) 20, (object) 2,
                new Translator("Level 2 explorer", "Дослідник 2-го рівня", "Исследователь 2-го уровня"),
                new Translator("Complete 20 company levels.", "Пройдіть 20 рівнів компанії.",
                    "Пройдите 20 уровней компании."),
                new Translator("You get a Shooter character!", "Ти отримуєш персонажа Shooter!",
                    "Ты Получаешь персонажа Shooter!"),
                CheckCompletedLevels,
                OpenCharacter));
            Achievements.Add(new Achievement(
                (object) 30, (object) 3,
                new Translator("Level 3 explorer", "Дослідник 3-го рівня", "Исследователь 3-го уровня"),
                new Translator("Complete all company levels.", "Пройдіть всі рівні компанії.",
                    "Пройдите все уровни компании."),
                new Translator("You get a NEO character!", "Ти отримуєш персонажа NEO!", "Ты Получаешь персонажа NEO!"),
                CheckCompletedLevels,
                OpenCharacter));
            Achievements.Add(new Achievement(
                (object) 100, 4,
                new Translator("You are resilience itself!", "Ти сама стійкість!", "Ты сама устойчивость!"),
                new Translator("Stay alive for 100 seconds.", "Залишайтеся в живих протягом 100 секунд.",
                    "Остаться в живых в течение 100 секунд."),
                new Translator("You get a Tank character", "Ти отримуєш персонажа Tank", "Ты Получаешь персонажа Tank"),
                CheckSurviveRecord,
                OpenCharacter));
            Achievements.Add(new Achievement(
                (object) 200, 5,
                new Translator("You are elusive!", "Ти невловимий!", "Ты неуловим!"),
                new Translator("Stay alive for 200 seconds.", "Залишайтеся в живих протягом 200 секунд.",
                    "Остаться в живых в течение 200 секунд."),
                new Translator("You get a Necromus character", "Ти отримуєш персонажа Necromus",
                    "Ты Получаешь персонажа Necromus"),
                CheckSurviveRecord,
                OpenCharacter));
            Achievements.Add(new Achievement(
                (object) 300, 1000,
                new Translator("Are you sure you're not a cheater?", "Ти точно не чітер?", "Ты точно не читер?"),
                new Translator("Stay alive for 300 seconds.", "Залишайтеся в живих протягом 300 секунд.",
                    "Остаться в живых в течение 300 секунд."),
                new Translator("You get a 1000 coins", "Ти отримуєш 1000 монет", "Ты Получаешь 1000 монет"),
                CheckSurviveRecord,
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
                    PlayerPrefs.SetInt($"Achievement{_count}", Achievements[_count].Achieved ? 1 : 0);
                }

                _count++;
            }
        }

        //task
        private bool CheckCompletedLevels(object target)
        {
            if (PlayerPrefs.HasKey("CompleteLevel"))
            {
                return PlayerPrefs.GetInt("CompleteLevel") >= (int) target;
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
                if (recordTime[i] >= (int) target)
                {
                    return true;
                }
            }

            return false;
        }

        //reward
        private bool OpenCharacter(object index)
        {
            PlayerPrefs.SetInt($"Open{(int) index}", 1);
            return true;
        }

        private bool AddCoins(object coins)
        {
            if (PlayerPrefs.HasKey("Coins"))
            {
                int allCoins = PlayerPrefs.GetInt("Coins") + (int) coins;
                PlayerPrefs.SetInt("Coins", allCoins);
                return true;
            }
            else
            {
                PlayerPrefs.SetInt("Coins", (int) coins);
                return true;
            }
        }
    }
}