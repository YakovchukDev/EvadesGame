using System.Collections.Generic;
using Map.Data;
using Menu.SelectionClass;
using UnityEngine;

namespace Menu.level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelMenuView _levelMenuView;
        [SerializeField] private List<LevelParameters> _levelParameters;
        [SerializeField] private ClassAvailability _classAvailability;
        [SerializeField] private SelectionClassView _selectionClassView;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private Animator _levelsAnimator;
        [SerializeField] private GameObject _selectionClass;
        [SerializeField] private int _countLevel;
        public static int CompleteLevel;
        public static int ChoiceLevel;

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

            for (int i = 1; i <= _countLevel; i++)
            {
                LevelElement element = Instantiate(_levelMenuView.LevelElement, _levelMenuView.ElementGrid);
                element.LevelButton.interactable = i - 1 <= CompleteLevel;
                element.LevelNumberText.text = i.ToString();
                element.SetLevelParametrs(_levelParameters[i - 1]);
                element.LevelNumber = i;
                element.Initialize();
                element.OnLevel += LevelSelected;
                element.SetAchievedTarggets();
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
            _selectionAnimator.SetInteger("Selection", 0);
            _levelsAnimator.SetInteger("Levels", 1);
        }
    }
}