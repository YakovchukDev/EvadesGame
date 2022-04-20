using UnityEngine;

namespace Menu.level
{
    public class LevelElementController : MonoBehaviour
    {
        public static bool OnView;
        [SerializeField] private int _levelNumber;
        [SerializeField] private Map.LevelParameters _levelParameters;
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
        public void SetLevelParametrs(Map.LevelParameters levelParameters)
        {
            _levelParameters = levelParameters;
        }
    }
}
