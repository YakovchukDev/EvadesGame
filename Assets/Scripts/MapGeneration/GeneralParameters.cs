using UnityEngine;

namespace MapGeneration
{
    //TDO class, в подальшому хочу розділити ланшафт карти і ворогів, цей клас потрібен, щоб не повторяти одні і тіж данні
    public class GeneralParameters : MonoBehaviour
    {
        [SerializeField]
        private _RoomParameters _longRoom;
        [SerializeField]
        private _SaveZoneParameters _smallRoom;
        [SerializeField]
        private int _difficulty;
        [SerializeField]
        private int _length;
        [SerializeField]
        private int _branchs;
        [SerializeField]
        private int _sizeChank;
        public static _RoomParameters LongRoom { get; private set; }
        public static _SaveZoneParameters SmallRoom { get; private set; }
        public static int Difficulty { get; private set; }
        //public int[] DifficultyFromTo = new int[2] { get; private set; }
        public static int Length { get; private set; }
        public static int Branchs { get; private set; }
        public static int SizeChank { get; private set; }

        void Start()
        {
            LongRoom = _longRoom;
            SmallRoom = _smallRoom;
            Difficulty = _difficulty;
            Length = _length;
            Branchs = _branchs;
            SizeChank = _sizeChank;

            //defence from idiot
            if(Difficulty < 0)
            {
                Difficulty = 0;
            }
            else if(Difficulty > 10)
            {
                Difficulty = 10;
            }
            if(SizeChank < 5)
            {
                SizeChank = 5;
            }
            if(Branchs < 0)
           {
                Branchs = 0;
            }
            if(Length < SizeChank)
            {
                Length = SizeChank;
            }
        }
    }
}
