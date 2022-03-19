using UnityEngine;

namespace Menu.level
{
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private LevelElementView _levelElementView;
        [SerializeField] private Transform _elementGrid;

        public LevelElementView LevelElementView => _levelElementView;
        public Transform ElementGrid => _elementGrid;
    }
}
