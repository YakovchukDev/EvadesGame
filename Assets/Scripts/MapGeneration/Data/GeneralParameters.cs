using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    //TDO class, в подальшому хочу розділити ланшафт карти і ворогів, цей клас потрібен, щоб не повторяти одні і тіж данні
    public class GeneralParameters : MonoBehaviour
    {
        public static MainDataCollector MainDataCollector;

        public delegate void Loaded();
        public static event Loaded LoadedGeneralParameters;

        public static RoomParameters LongRoom { get; private set; }
        public static SaveZoneParameters ShortRoom { get; private set; }
        public static GameObject EmptyWall { get; private set; }
        public static StarController Star { get; private set; }
        public static LevelParameters LevelParameters { get; private set;}
        [SerializeField] private RoomParameters _longRoom;
        [SerializeField] private SaveZoneParameters _smallRoom;
        [SerializeField] private GameObject _emptyWall;
        [SerializeField] private StarController _star;
        public static List<GameObject> CharacterList;

        [SerializeField] private List<GameObject> _characterList;
        void Start()
        {
            
            /*
            LevelParameters = new LevelParameters();
            LevelParameters.Difficulty = 10;
            LevelParameters.Length = 5;
            LevelParameters.Branchs = 5;
            LevelParameters.SizeChank = 5;
            */
            CharacterList = _characterList;
            MainDataCollector = new MainDataCollector();
            LongRoom = _longRoom;
            ShortRoom = _smallRoom;
            EmptyWall = _emptyWall;
            Star = _star;
            LoadedGeneralParameters();
        }
        public static void SetMapData(LevelParameters levelParameters)
        {
            LevelParameters = levelParameters;
        }
    }
}
