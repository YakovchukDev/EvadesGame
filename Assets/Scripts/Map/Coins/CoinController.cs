using UnityEngine;
using TMPro;

namespace Map
{
    public class CoinController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinTMP;

        void OnEnable()
        {
            CoinControl.GiveCoin += updateQuantityCoin;
            GeneralParameters.LoadedGeneralParameters += StartWork;
        }
        void OnDisable()
        {
            CoinControl.GiveCoin -= updateQuantityCoin;
            GeneralParameters.LoadedGeneralParameters -= StartWork;
        }
        private void StartWork()
        {
            _coinTMP.text = $"{GeneralParameters.MainDataCollector.Coins}";
        }

        private void updateQuantityCoin()
        {
            GeneralParameters.MainDataCollector.Coins++;
            _coinTMP.text = $"{GeneralParameters.MainDataCollector.Coins}";
        }        
}
}
