using System.Collections.Generic;
using UnityEngine;

namespace Menu.ScriptableObject.Information
{
    public class InformationController : MonoBehaviour
    {
        [Tooltip("Position for spawn")] [SerializeField]
        private Transform _spawnEnemyPosition;

        [Tooltip("Position for spawn")] [SerializeField]
        private Transform _spawnFieldEnemyPosition;

        [Tooltip("Position for spawn")] [SerializeField]
        private Transform _spawnCharacterPosition;


        [SerializeField] private List<InfoPanel> _infoEnemyPanels;
        [SerializeField] private List<InfoPanel> _infoFieldEnemyPanels;
        [SerializeField] private List<InfoPanel> _infoCharacterPanels;
        [SerializeField] private GameObject _infoPrefab;


        private void Awake()
        {
            PanelSpawner(_spawnEnemyPosition, _infoEnemyPanels, _infoPrefab);
            PanelSpawner(_spawnFieldEnemyPosition, _infoFieldEnemyPanels, _infoPrefab);
            PanelSpawner(_spawnCharacterPosition, _infoCharacterPanels, _infoPrefab);
        }

        private void PanelSpawner(Transform spawnPosition, List<InfoPanel> infoPanels, GameObject prefab)
        {
            foreach (var infoPanel in infoPanels)
            {
                var panel = Instantiate(prefab, spawnPosition);
                panel.GetComponent<PanelObject>().InfoPanel = infoPanel;
            }
        }
    }
}