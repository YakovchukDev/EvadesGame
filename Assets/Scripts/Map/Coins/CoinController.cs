using UnityEngine;
using TMPro;

namespace Map.Coins
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinTMP;
        private int _coinCount = 0;

        private void OnEnable()
        {
            CoinControl.GiveCoin += UpdateQuantityCoin;
        }
        private void OnDisable()
        {
            CoinControl.GiveCoin -= UpdateQuantityCoin;
        }
        public void Start()
        {
            _coinTMP.text = $"0";
        }
        public int GetCoinsResult() => _coinCount;
        private void UpdateQuantityCoin(int value)
        {
            _coinCount += value;
            _coinTMP.text = $"{_coinCount}";
        } 
    }
}
