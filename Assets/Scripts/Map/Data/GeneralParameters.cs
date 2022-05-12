using System.Collections.Generic;
using UnityEngine;
using System;

namespace Map.Data
{
    //TDO class
    //GeneralParametrs упрощяет передачу обьектов между классами, а так же избегает множественного повторения одних и тех же обьектов в разных классах, тем самым он частично нарушает принципы SOLID
    public class GeneralParameters : MonoBehaviour
    {
        public static MainDataCollector MainDataCollector { get; set; }
        public static RoomParameters LongRoom { get; private set; }
        public static SaveZoneParameters ShortRoom { get; private set; }
        public static GameObject IndestructibleEnemy { get; private set; }
        public static LevelParameters LevelParameters { get; private set; }
        public static List<GameObject> CharacterList {get; private set; }
        public static List<Enemy> EnemyList { get; private set; }

        [SerializeField] private RoomParameters _longRoom;
        [SerializeField] private SaveZoneParameters _smallRoom;
        [SerializeField] private GameObject _indestructibleEnemy;
        [SerializeField] private List<GameObject> _characterList;
        [SerializeField] private List<Enemy> _enemyList;
        public static event Action LoadedGeneralParameters;
        void Start()
        {
            MainDataCollector = new MainDataCollector();
            LongRoom = _longRoom;
            ShortRoom = _smallRoom;
            IndestructibleEnemy = _indestructibleEnemy;
            CharacterList = _characterList;
            EnemyList = _enemyList;

            LoadedGeneralParameters();
        }
        public static void SetMapData(LevelParameters levelParameters)
        {
            LevelParameters = levelParameters;
        }
    }
}
