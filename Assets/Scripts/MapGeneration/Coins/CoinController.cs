using Map;
using TMPro;
using UnityEngine;

namespace MapGeneration.Coins
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinTMP;

        private void OnEnable()
        {
            CoinControl.GiveCoin += UpdateQuantityCoin;
            GeneralParameters.LoadedGeneralParameters += StartWork;
        }

        private void OnDisable()
        {
            CoinControl.GiveCoin -= UpdateQuantityCoin;
            GeneralParameters.LoadedGeneralParameters -= StartWork;
        }

        private void StartWork()
        {
            _coinTMP.text = $"{GeneralParameters.MainDataCollector.Coins}";
        }

        private void UpdateQuantityCoin()
        {
            GeneralParameters.MainDataCollector.Coins++;
            _coinTMP.text = $"{GeneralParameters.MainDataCollector.Coins}";
        }
    }
}