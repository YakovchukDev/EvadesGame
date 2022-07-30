using UnityEngine;

namespace Map.Data
{
    public class LevelParameters : MonoBehaviour
    {
        [Range(0, 10)] public int Difficulty;
        [Min(0)] public int Branchs;
        [Min(1)] public int Length;
        [Min(1)] public int SizeChank;
        [Min(0)] public int MaxCoinInRoom;
        [Min(0)] public int MaxEnemiesInRoom;
    }
}
