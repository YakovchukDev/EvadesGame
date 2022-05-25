using System;
using System.Collections.Generic;
using Audio;
using MapGeneration.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.level
{
    public class LevelElement : MonoBehaviour
    {
        private AudioManager _audioManager;
        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private Button _levelButton;
        [SerializeField] private List<Image> _stars;
        [SerializeField] private LevelParameters _levelParameters;
        [SerializeField] private Transform _transform;
        [SerializeField] private int _levelNumber;
        public TMP_Text LevelNumberText => _levelNumberText;
        public Button LevelButton => _levelButton;
        public List<Image> Stars => _stars;
        public Transform Transform => _transform;


        public int LevelNumber { get; set; }


        public event Action OnLevel;

        private void Start()
        {
            _audioManager = AudioManager.Instanse;
        }

        public void Initialize()
        {
            _levelButton.onClick.AddListener(SetLevel);
        }

        private void SetLevel()
        {
            LevelController.ChoiceLevel = LevelNumber;
            _audioManager.Play("PressButton");
            OnLevel?.Invoke();
        }

        private void OnDestroy()
        {
            _levelButton.onClick.RemoveListener(SetLevel);
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