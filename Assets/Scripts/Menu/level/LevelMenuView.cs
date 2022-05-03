using UnityEngine;

namespace Menu.level
{
    public class LevelMenuView : MonoBehaviour
    {
        [SerializeField] private LevelElement _levelElement;
        [SerializeField] private Transform _elementGrid;
        public LevelElement LevelElement => _levelElement;
        public Transform ElementGrid => _elementGrid;
    }
}