using Menu.SelectionClass;
using UnityEngine;

namespace Menu.level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelMenuView _levelMenuView;
        [SerializeField] private ClassAvailability _classAvailability;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private Animator _levelsAnimator;
        [SerializeField] private GameObject _selectionClassView;
        [SerializeField] private int _countLevel;
        public static int CompleteLevel = 1;


        private void Start()
        {
            for (int i = 1; i <= _countLevel; i++)
            {
                _levelMenuView.LevelElementView.LevelButton.interactable = i - 1 <= CompleteLevel;
                _levelMenuView.LevelElementView.LevelNumber.text = i.ToString();
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