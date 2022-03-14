using UnityEngine;

namespace Menu.level
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private LevelMenuView _levelMenuView;
        [SerializeField] private ClassAvailability _classAvailability;
        [SerializeField] private GameObject _selectionClassView;
        [SerializeField] private GameObject _levelView;
        [SerializeField] private int _countLevel;
        public static int _completeLevel = 10;


        private void Start()
        {
            for (int i = 1; i <= _countLevel; i++)
            {
                if (i - 1 <= _completeLevel)
                {
                    _levelMenuView.LevelElementView.LevelButton.interactable = true;
                }
                else
                {
                    _levelMenuView.LevelElementView.LevelButton.interactable = false;
                }

                _levelMenuView.LevelElementView.LevelNumber.text = i.ToString();
                Instantiate(_levelMenuView.LevelElementView, _levelMenuView.ElementGrid);
            }
        }

        public void Update()
        {
            if (LevelElementController._onView)
            {
                _classAvailability.CheckClassForLevel();
                _selectionClassView.SetActive(true);
                _levelView.SetActive(false);
                LevelElementController._onView = false;
            }
        }
    }
}