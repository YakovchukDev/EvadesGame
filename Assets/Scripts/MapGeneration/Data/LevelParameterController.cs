using System.Collections.Generic;
using MapGeneration.Data;
using UnityEngine;

public class LevelParameterController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _levels;

    private void Awake()
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            var levelParameter = _levels[i].GetComponent<LevelParameters>();
            levelParameter.Difficulty = 10 + i;
            levelParameter.Branchs = 5 + i;
            levelParameter.Length = 10 + i;
            levelParameter.SizeChank = 10 + i;
            levelParameter.TargetExpirience = 4 + i;
        }
    }
}
