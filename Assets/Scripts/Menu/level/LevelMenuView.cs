using UnityEngine;

namespace Menu.level
{
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private LevelElementView _levelElementView;
        [SerializeField] private Transform _elementGrid;
        [SerializeField] private GameObject _selectionClass;

        public LevelElementView LevelElementView => _levelElementView;
        public Transform ElementGrid => _elementGrid;
        
        public void ButtonPlay()
        {
            _selectionClass.SetActive(true);
        }
    }
}
