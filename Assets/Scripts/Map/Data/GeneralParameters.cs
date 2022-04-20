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
        public static SaveZoneParameters SmallRoom { get; private set; }
        public static StarController Star { get; private set; }
        public static LevelParameters LevelParameters { get; private set;}
        [SerializeField] private RoomParameters _longRoom;
        [SerializeField] private SaveZoneParameters _smallRoom;
        [SerializeField] private StarController _star;

        void Start()
        {
            LevelParameters = new LevelParameters();
            LevelParameters.Difficulty = 10;
            LevelParameters.Length = 5;
            LevelParameters.Branchs = 5;
            LevelParameters.SizeChank = 5;

            MainDataCollector = new MainDataCollector();
            LongRoom = _longRoom;
            SmallRoom = _smallRoom;
            Star = _star;
            LoadedGeneralParameters();
        }
        public static void SetMapData(LevelParameters levelParameters)
        {
            LevelParameters = levelParameters;
        }
    }
}
