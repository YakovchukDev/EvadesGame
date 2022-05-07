using UnityEngine;
using Map.Data;

namespace Menu.level
{
    public class LevelElementController : MonoBehaviour
    {
        [SerializeField] private int _levelNumber;
        [SerializeField] private Map.LevelParameters _levelParameters;
        public static bool OnView;

        public void ButtonPlay()
        {
            OnView = true;
        }

        public void SetIdLevel()
        {
            PlayerPrefs.SetInt("LevelNumber", _levelNumber);
        }

        public void SetLevelParametrs()
        {
            GeneralParameters.SetMapData(_levelParameters);
        }

        public void SetLevelParametrs(Map.LevelParameters levelParameters)
        {
            _levelParameters = levelParameters;
        }
        public void SetLevelNumber(int levelNumber) 
        {
            _levelNumber = levelNumber;
        }
    }
}