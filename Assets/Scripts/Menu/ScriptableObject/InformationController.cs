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

        [SerializeField] private List<InfoPanel> _infoEnemyPanels;
        [SerializeField] private List<InfoPanel> _infoFieldEnemyPanels;
        [SerializeField] private List<InfoPanel> _infoCharacterPanels;
        [SerializeField] private GameObject _infoPrefab;

        private void Awake()
        {
            foreach (var infoEnemyPanel in _infoEnemyPanels)
            {
                var infoPanel= Instantiate(_infoPrefab, _spawnEnemyPosition);
                infoPanel.GetComponent<PanelObject>().InfoPanel = infoEnemyPanel;
            }

            foreach (var infoFieldEnemyPanel in _infoFieldEnemyPanels)
            {
                var infoPanel= Instantiate(_infoPrefab, _spawnFieldEnemyPosition);
                infoPanel.GetComponent<PanelObject>().InfoPanel = infoFieldEnemyPanel;
            }

            foreach (var infoCharacterPanel in _infoCharacterPanels)
            {
                var infoPanel= Instantiate(_infoPrefab, _spawnCharacterPosition);
                infoPanel.GetComponent<PanelObject>().InfoPanel = infoCharacterPanel;
            }
        }
    }
}