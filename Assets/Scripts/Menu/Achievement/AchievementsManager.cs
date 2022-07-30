using System.Collections.Generic;
using UnityEngine;

namespace Menu.Achievement
{
    public class AchievementsManager : MonoBehaviour
    {
        public bool ToLoop;
        public List<Menu.Achievement.Achievement> Achievements;
        public List<Menu.Achievement.Achievement> AccomplishedAchievements;
        private int _count;

        private void Start()
        {
            _count = 0;
            InitializeAchievement();
        }
        public void Update()
        {
            CheckAchievementCompletion();
        }
        private void InitializeAchievement()
        {
            Achievements = new List<Achievement>();
            AccomplishedAchievements = new List<Achievement>();
            Achievements.Add(new Menu.Achievement.Achievement(
                (object)10, (object)2,
                "Level 1 explorer",
                "complete 10 levels with 1 star or more",
                "get NEO character",
                CheckCompletedLevels,
                OpenCharacter));
            Achievements.Add(new Menu.Achievement.Achievement(
                (object)100, (object)4,
                "You are resilience itself",
                "Stay alive for 100 seconds",
                "get Tank character",
                CheckCompletedLevels,
                OpenCharacter));
        }
        public void CheckAchievementCompletion()
        {
            if (Achievements != null)
            {
                if (_count >= Achievements.Count)
                {
                    _count = 0;
                }
                print(_count);
                if (Achievements.Count > 0 && Achievements[_count].UpdateCompletion())
                {
                    AccomplishedAchievements.Add(Achievements[_count]);
                    Achievements.RemoveAt(_count);
                    _count--;
                }
                _count++;
            }
        }

        //task
        private bool CheckCompletedLevels(object target)
        {
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
            if (PlayerPrefs.HasKey($"Open{(int)index}"))
            {
                PlayerPrefs.SetInt($"Open{(int)index}", 1);
                return true;
            }
            return false;
        }

        //test
        public void Click()
        {
            PlayerPrefs.SetInt("CompleteLevel", 10);
        }
    }
}
