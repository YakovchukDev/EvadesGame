using System;
using System.Collections.Generic;
using Menu.ScriptableObject.Information;
using TMPro;
using UnityEngine;

namespace Menu.ScriptableObject.Shop
{
    public class ShopController : MonoBehaviour
    {
        [Tooltip("Position for spawn")] [SerializeField]
        private Transform _spawnCharacterPosition;

        [Tooltip("Position for spawn")] [SerializeField]
        private Transform _spawnMoneyPosition;

        [SerializeField] private List<ShopPanel> _shopCharacterPanels;
        [SerializeField] private List<ShopPanel> _shopMoneyPanels;

        [SerializeField] private GameObject _shopPrefab;

        [SerializeField] private TMP_Text _money;

        private void Awake()
        {
            _money.text = PlayerPrefs.GetInt("Money").ToString();
            PanelSpawner(_spawnCharacterPosition, _shopCharacterPanels, _shopPrefab);
        }

        private void Update()
        {
            _money.text = PlayerPrefs.GetInt("Money").ToString();
        }

        private void PanelSpawner(Transform spawnPosition, List<ShopPanel> shopPanels, GameObject prefab)
        {
            foreach (var shopPanel in shopPanels)
            {
                var panel = Instantiate(prefab, spawnPosition);
                panel.GetComponent<ShopObject>().ShopPanel = shopPanel;
            }
        }
    }
}