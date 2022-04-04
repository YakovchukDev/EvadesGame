using System.Collections.Generic;
using UnityEngine;

namespace Menu.ScriptableObject
{
    public class InformationController : MonoBehaviour
    {
        [Tooltip("Место для спавна")] [SerializeField]
        private Transform _spawnEnemyPosition;
        [Tooltip("Место для спавна")] [SerializeField]
        private Transform _spawnFieldEnemyPosition;
        [Tooltip("Место для спавна")] [SerializeField]
        private Transform _spawnCharacterPosition;

        [SerializeField] private List<GameObject> _infoEnemyPrefab;
        [SerializeField] private List<InfoPanel> _infoEnemyPanels;

        [SerializeField] private List<GameObject> _infoFieldEnemyPrefab;
        [SerializeField] private List<InfoPanel> _infoFieldEnemyPanels;

        [SerializeField] private List<GameObject> _infoCharacterPrefab;
        [SerializeField] private List<InfoPanel> _infoCharacterPanels;


        private void Awake()
        {
            for (int i = 0; i < _infoEnemyPanels.Count && i < _infoEnemyPrefab.Count; i++)
            {
                Instantiate(_infoEnemyPrefab[i], _spawnEnemyPosition);
            }
            for (int i = 0; i < _infoFieldEnemyPanels.Count && i < _infoFieldEnemyPrefab.Count; i++)
            {
                Instantiate(_infoFieldEnemyPrefab[i], _spawnFieldEnemyPosition);
            }
            for (int i = 0; i < _infoCharacterPanels.Count && i < _infoCharacterPrefab.Count; i++)
            {
                Instantiate(_infoCharacterPrefab[i], _spawnCharacterPosition);
            }
        }
    }
}