using System;
using UnityEngine;

namespace Menu.Achievement
{
    public class Achievement : MonoBehaviour
    {
        public static Action<Achievement> AchieveComplite;
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
                    AchieveComplite?.Invoke(this);
                    Reward?.Invoke(RewardValue);
                    return true;
                }
            }
            return false;

        }

    }
}
