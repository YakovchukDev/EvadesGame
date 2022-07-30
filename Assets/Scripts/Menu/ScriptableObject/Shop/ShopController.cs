using System.Collections.Generic;
using Menu.ScriptableObject.Shop.CharacterShop;
using Menu.ScriptableObject.Shop.MoneyShop;
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

        [SerializeField] private List<ShopCharacterPanel> _shopCharacterPanels;
         [SerializeField] private List<ShopMoneyPanel> _shopMoneyPanels;

        [SerializeField] private GameObject _shopCharacterPrefab;
        [SerializeField] private GameObject _shopMoneyPrefab;

        [SerializeField] private TMP_Text _money;

        private void Awake()
        {
            _money.text = PlayerPrefs.GetInt("Coins").ToString();
            PanelCharacterSpawner(_spawnCharacterPosition, _shopCharacterPanels);
            PanelMoneySpawner(_spawnMoneyPosition, _shopMoneyPanels);
        }

        private void Update()
        {
            _money.text = PlayerPrefs.GetInt("Coins").ToString();
        }

        private void PanelCharacterSpawner(Transform spawnPosition, List<ShopCharacterPanel> shopPanels)
        {
            foreach (var shopPanel in shopPanels)
            {
                var panel = Instantiate(_shopCharacterPrefab, spawnPosition);
                panel.GetComponent<ShopCharacterObject>().ShopCharacterPanel = shopPanel;
            }
        }

        private void PanelMoneySpawner(Transform spawnPosition, List<ShopMoneyPanel> shopPanels)
        {
            foreach (var shopPanel in shopPanels)
            {
                var panel = Instantiate(_shopMoneyPrefab, spawnPosition);
                panel.GetComponent<ShopMoneyObject>().ShopMoneyPanel = shopPanel;
            }
        }
    }
}