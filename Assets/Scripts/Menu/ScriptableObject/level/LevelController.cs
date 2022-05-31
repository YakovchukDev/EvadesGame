using System.Collections.Generic;
using MapGeneration.Data;
using Menu.ScriptableObject.Company;
using Menu.SelectionClass;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Menu.ScriptableObject.level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelMenuView _levelMenuView;
        [SerializeField] private List<LevelParameters> _levelParameters;
        [SerializeField] private ClassAvailability _classAvailability;
        [SerializeField] private SelectionClassView _selectionClassView;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private GameObject _selectionClass;
        [SerializeField] private int _countLevel;
        [SerializeField] private List<CompanyPanel> _companyPanels;
        [Header("VerticalLevelHeight")]
        [SerializeField] private int _minVerticalLevelHeight;
        [SerializeField] private int _maxVerticalLevelHeight;

        private LevelElement _levelElement;
        public static int CompleteLevel;
        public static int ChoiceLevel;
        private int _newTypeLevels = 4;
        private int _scriptableNumber;

        private void Start()
        {
            if (PlayerPrefs.HasKey("CompleteLevel"))
            {
                CompleteLevel = PlayerPrefs.GetInt("CompleteLevel");
            }
            else
            {
                CompleteLevel = 0;
                PlayerPrefs.SetInt("CompleteLevel", 0);
            }

            CompleteLevel = 30;

            CompanyButton.OnCompanyUnlocked += SpawnLevelElement;
        }


        private void SpawnLevelElement(int parametr)
        {
            for (int i = 0, textNumber = 1; i <= _countLevel - 1; i++, textNumber++)
            {
                _levelMenuView.LevelElement.LevelButton.interactable = i <= CompleteLevel;
                _levelMenuView.LevelElement.LevelNumberText.text = textNumber.ToString();
                _levelMenuView.LevelElement.SetLevelParametrs(_levelParameters[i]);
                _levelElement = Instantiate(_levelMenuView.LevelElement, _levelMenuView.ElementGrid);
                InitializeLevelElement(textNumber, i);
            }

            /*Canvas.ForceUpdateCanvases();
            _horizontalLayoutGroup.enabled = false;*/


            CompanyButton.OnCompanyUnlocked -= SpawnLevelElement;
        }

        private void InitializeLevelElement(int textNumber, int i)
        {
            _levelElement.LevelNumber = textNumber;
            _levelElement.Initialize();
            _levelElement.OnLevel += LevelSelected;
            if (i <= _newTypeLevels)
            {
                foreach (var star in _levelElement.Stars)
                {
                    star.sprite = _companyPanels[_scriptableNumber].CloseStarSprite;
                }

                _levelElement.LevelButton.image.sprite = _companyPanels[_scriptableNumber].MainSprite1;
                _levelElement.LevelNumberText.rectTransform.anchoredPosition =
                    _companyPanels[_scriptableNumber].TextTransformPosition;
            }

            _levelElement.Transform.localPosition =
                new Vector2(transform.position.x, Random.Range(_minVerticalLevelHeight,_maxVerticalLevelHeight));
            if (i == _newTypeLevels)
            {
                _scriptableNumber++;
                _newTypeLevels += 5;
            }
        }

        private void LevelSelected()
        {
            switch (PlayerPrefs.GetString("Language"))
            {
                case "English":
                    _selectionClassView.InfoTime.text = "Level: " + ChoiceLevel;
                    break;
                case "Russian":
                    _selectionClassView.InfoTime.text = "Уровень: " + ChoiceLevel;
                    break;
                case "Ukrainian":
                    _selectionClassView.InfoTime.text = "Рівень: " + ChoiceLevel;
                    break;
            }

            _classAvailability.CheckClassForLevel();
            _selectionClass.SetActive(true);
            _selectionAnimator.SetInteger("Information", 0);
        }
    }
}