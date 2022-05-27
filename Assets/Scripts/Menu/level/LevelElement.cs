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
        public TMP_Text LevelNumberText => _levelNumberText;
        public Button LevelButton => _levelButton;
        [SerializeField] private List<Image> _starsList => _stars;

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
            PlayerPrefs.SetInt("LevelNumber", LevelNumber);
        }

        public void SetLevelParametrs()
        {
            Map.MapManager.LevelParameters = _levelParameters;
        }

        public void SetLevelParametrs(LevelParameters levelParameters)
        {
            _levelParameters = levelParameters;
        }
        public void SetAchievedTarggets()
        {
            if(PlayerPrefs.HasKey($"Level{LevelNumber}"))
            {
                string stars = PlayerPrefs.GetString($"Level{LevelNumber}");
                for(int i = 0; i < stars.Length && i < _starsList.Count; i++)
                {
                    _starsList[i].gameObject.SetActive(stars[i] == '*' ? true : false);
                }
            }
        }
    }
}