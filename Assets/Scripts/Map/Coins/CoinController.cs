using UnityEngine;
using TMPro;
using Map.Data;

namespace Map.Coins
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinTMP;

        void OnEnable()
        {
            CoinControl.GiveCoin += UpdateQuantityCoin;
            GeneralParameters.LoadedGeneralParameters += StartWork;
        }
        void OnDisable()
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
        private void SurvivalUpdateQuantityCoin()
        {
            GeneralParameters.MainDataCollector.Coins++;
            _coinTMP.text = $"{GeneralParameters.MainDataCollector.Coins}";
        }    
}
}
