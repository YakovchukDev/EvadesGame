using System;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Data
{
    public class LevelParameterController : MonoBehaviour
    {
        [SerializeField] private List<LevelParameters> _levels;

        private void Awake()
        {
            for (int i = 0; i < _levels.Count; i++)
            {
                LevelParameters levelParameter = _levels[i];
                levelParameter.Difficulty = (int)Math.Round(10f * i / _levels.Count);
                levelParameter.Length = 3 + (int)Math.Round(i * levelParameter.Difficulty / 4f);
                levelParameter.Branchs = levelParameter.Difficulty + (levelParameter.Length / 10);
                levelParameter.SizeChank = 10;
               // levelParameter.TargetExpirience = 2;
            }
        }
    }
}