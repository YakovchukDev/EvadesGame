using UnityEngine;

namespace Menu.level
{
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private LevelElementController _levelElementController;
        [SerializeField] private LevelElementView _levelElementView;
        [SerializeField] private Transform _elementGrid;

        public LevelElementController LevelElementController => _levelElementController;
        public LevelElementView LevelElementView => _levelElementView;
        public Transform ElementGrid => _elementGrid;
    }
}
