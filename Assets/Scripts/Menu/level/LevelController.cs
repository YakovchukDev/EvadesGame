using System.Collections.Generic;
using MapGeneration.Data;
using Menu.SelectionClass;
using UnityEngine;

namespace Menu.level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelMenuView _levelMenuView;
        [SerializeField] private List<LevelParameters> _levelParameters;
        [SerializeField] private ClassAvailability _classAvailability;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private Animator _levelsAnimator;
        [SerializeField] private GameObject _selectionClassView;
        [SerializeField] private int _countLevel;
        public static int CompleteLevel;

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
                _levelMenuView.LevelElementView.LevelButton.interactable = i - 1 <= CompleteLevel;
                _levelMenuView.LevelElementView.LevelNumber.text = i.ToString();
                _levelMenuView.LevelElementController.SetLevelParametrs(_levelParameters[i - 1]);
                Instantiate(_levelMenuView.LevelElementView, _levelMenuView.ElementGrid);
            }
        }

        public void Update()
        {
            if (LevelElementController.OnView)
            {
                _classAvailability.CheckClassForLevel();
                _selectionClassView.SetActive(true);
                _selectionAnimator.SetInteger("Selection", 0);
                _levelsAnimator.SetInteger("Levels", 1);
                LevelElementController.OnView = false;
            }
        }
    }
}