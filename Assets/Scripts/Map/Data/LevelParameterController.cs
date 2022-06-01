using System;
using System.Collections.Generic;
using UnityEngine;
using Map;

namespace Map.Data
{
    public class LevelParameterController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _levels;
        private float _complexity;

        private void Awake()
        {
            

            for (int i = 0; i < _levels.Count; i++)
            {
                var levelParameter = _levels[i].GetComponent<LevelParameters>();
                
                float levelsCount = _levels.Count;
                _complexity += 10 / levelsCount;
                levelParameter.Difficulty = (int) Math.Round(_complexity);
                
                levelParameter.Branchs = 5 + i;
                levelParameter.Length = 10 + i;
                levelParameter.SizeChank = 10 + i;
                levelParameter.TargetExpirience = 4 + i;
            }
        }
    }
}