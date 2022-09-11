using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.Achievement
{
    public class Achievement : MonoBehaviour
    {
        public Translator Title;
        public Translator Task;
        public Translator RewardStr;
        public object RequirementValue;
        public object RewardValue;
        public Predicate<object> Requirement;
        public Predicate<object> Reward;
        public bool Achieved;
        public static Action<Achievement> OnAchieveComplite;
        public Achievement(object requirementValue, object rewardValue, Translator title, Translator taskStr, Translator rewardStr, Predicate<object> requirement, Predicate<object> reward)
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
                    OnAchieveComplite.Invoke(this);
                    return true;
                }
            }
            return false;

        }

    }
}