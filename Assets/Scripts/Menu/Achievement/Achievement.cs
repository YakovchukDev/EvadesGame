using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.Achievement
{
    public class Achievement : MonoBehaviour
    {
        public string Title;
        public string Task;
        public string RewardStr;
        public object RequirementValue;
        public object RewardValue;
        public Predicate<object> Requirement;
        public Predicate<object> Reward;
        public bool Achieved;
        public Achievement(object requirementValue, object rewardValue, string title, string taskStr, string rewardStr, Predicate<object> requirement, Predicate<object> reward)
        {
            RequirementValue = requirementValue;
            RewardValue = rewardValue;
            Title = title;
            Task = taskStr;
            RewardStr = rewardStr;
            Requirement = requirement;
            Reward = reward;
        }
        public bool UpdateCompletion()
        {
            if (!Achieved)
            {
                if (Requirement.Invoke(RequirementValue))
                {
                    Achieved = true;
                    Reward?.Invoke(RewardValue);
                    return true;
                }
            }
            return false;

        }

    }
    public class AchievementData
    {
        public bool[] IsAchieveds;
        public AchievementData() { }

        public AchievementData(List<Achievement> achievements)
        {
            IsAchieveds = new bool[achievements.Count];
            for(int i = 0; i < IsAchieveds.Length; i++)
            {
                IsAchieveds[i] = achievements[i].Achieved;
            }
        }
    }
}
