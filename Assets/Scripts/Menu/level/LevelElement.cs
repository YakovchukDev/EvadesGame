using System;
using System.Collections.Generic;
using MapGeneration.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Map;
using Map.Data;

namespace Menu.level
{
    public class LevelElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private Button _levelButton;
        [SerializeField] private List<Image> _stars;
        [SerializeField] private LevelParameters _levelParameters;
        [SerializeField] private int _levelNumber;
        public TMP_Text LevelNumberText => _levelNumberText;
        public Button LevelButton => _levelButton;
        public List<Image> Stars => _stars;

        public int LevelNumber { get; set; }
    

        public event Action OnLevel;

        public void Initialize()
        {
            _levelButton.onClick.AddListener(Increased);
        }

        private void Increased()
        {
            LevelController.ChoiceLevel = LevelNumber;
            OnLevel?.Invoke();
        }

        private void OnDestroy()
        {
            _levelButton.onClick.RemoveListener(Increased);
        }

        public void SetIdLevel()
        {
            PlayerPrefs.SetInt("LevelNumber", _levelNumber);
        }

        public void SetLevelParametrs()
        {
            GeneralParameters.SetMapData(_levelParameters);
        }

        public void SetLevelParametrs(LevelParameters levelParameters)
        {
            _levelParameters = levelParameters;
        }
        public void SetLevelNumber(int levelNumber) 
        {
            _levelNumber = levelNumber;
        }
    }
}