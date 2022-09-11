using System;
using System.Collections.Generic;
using Audio;
using Map.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ScriptableObject.level
{
    public class LevelElement : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private Button _levelButton;
        [SerializeField] private List<Image> _stars;
        [SerializeField] private Transform _transform;
        [SerializeField] private Sprite _openStarSprite;
        private LevelParameters _levelParameters;
        private AudioManager _audioManager;
        public TMP_Text LevelNumberText => _levelNumberText;
        public Button LevelButton => _levelButton;
        public List<Image> Stars => _stars;
        public Transform Transform => _transform;

        public int LevelNumber { get; set; }


        public event Action OnLevel;

        private void Start()
        {
            _audioManager = AudioManager.Instanse;
            if (_levelButton.interactable == false)
            {
                foreach (var star in _stars)
                {
                    star.color = new Color(0.7f, 0.7f, 0.7f);
                }
            }
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
            PlayerPrefs.SetInt("LevelNumber", LevelNumber);
        }

        public void SetLevelParameters()
        {
            Map.MapManager.LevelParameters = _levelParameters;
        }

        public void SetLevelParameters(LevelParameters levelParameters)
        {
            _levelParameters = levelParameters;
        }

        public void SetAchievedTargets()
        {
            if (PlayerPrefs.HasKey($"Level{LevelNumber}"))
            {
                string stars = PlayerPrefs.GetString($"Level{LevelNumber}");
                if (stars[0] == '*')
                {
                    _levelButton.interactable = true;
                    int count = 0;
                    for (int i = 1; i < stars.Length; i++)
                    {
                        if (stars[i] == '*')
                        {
                            count++;
                        }
                    }

                    for (int i = 0; i < _stars.Count; i++)
                    {
                        if (count > 0)
                        {
                            _stars[i].sprite = _openStarSprite;
                            count--;
                        }
                    }
                }
                else
                {
                    _levelButton.interactable = false;
                }
            }
        }
    }
}