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
        public static int _completeLevel = 50;


        private void Start()
        {
            for (int i = 1; i <= _countLevel; i++)
            {
                _levelMenuView.LevelElementView.LevelButton.interactable = i - 1 <= _completeLevel;
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
                _levelView.SetActive(false);
                LevelElementController.OnView = false;
            }
        }
    }
}