using MapGeneration.Data;
using UnityEngine;

namespace Menu.level
{
    public class LevelElementController : MonoBehaviour
    {
        [SerializeField] private int _levelNumber;
        [SerializeField] private LevelParameters _levelParameters;
        public static bool OnView;

        public void ButtonPlay()
        {
            OnView = true;
        }

        public void SetIdLevel()
        {
            Map.GeneralParameters.MainDataCollector.GiveDataAboutLevel(_levelNumber);
        }

        public void SetLevelParametrs()
        {
            Map.GeneralParameters.SetMapData(_levelParameters);
        }

        public void SetLevelParametrs(LevelParameters levelParameters)
        {
            _levelParameters = levelParameters;
        }
    }
}