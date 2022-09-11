using System.Collections.Generic;
using Map.Data;
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
        //[SerializeField] private ClassAvailability _classAvailability;
        [SerializeField] private SelectionClassView _selectionClassView;
        [SerializeField] private CharacterInfo _characterInfo;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private GameObject _selectionClass;
        [SerializeField] private int _countLevel;
        [SerializeField] private List<CompanyPanel> _companyPanels;

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

            CompanyButton.OnCompanyUnlocked += SpawnLevelElement;
        }

        private void SpawnLevelElement(int parametr)
        {
            for (int i = 0, textNumber = 1; i <= _countLevel - 1; i++, textNumber++)
            {
                _levelElement = Instantiate(_levelMenuView.LevelElement, _levelMenuView.ElementGrid);

                _levelElement.LevelButton.interactable = i <= CompleteLevel;
                if (i == 0 && PlayerPrefs.GetInt("Education") == 0)
                {
                    _levelElement.LevelButton.interactable = false;
                }

                _levelElement.LevelNumberText.text = textNumber.ToString();
                _levelElement.SetLevelParameters(_levelParameters[i]);
                InitializeLevelElement(textNumber, i);
                _levelElement.SetAchievedTargets();
            }

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
                new Vector2(transform.position.x, Random.Range(_minVerticalLevelHeight, _maxVerticalLevelHeight));
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

            _selectionClass.SetActive(true);
            _selectionAnimator.SetInteger("Information", 0);
            _selectionClassView.ChoiceTypeOfCharacter(PlayerPrefs.GetInt("SelectionNumber"));
            _characterInfo.SetAbilitiesAndClass(PlayerPrefs.GetInt("SelectionNumber"));
        }

        private void OnDisable()
        {
            CompanyButton.OnCompanyUnlocked -= SpawnLevelElement;
        }
    }
}