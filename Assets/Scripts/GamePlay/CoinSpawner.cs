using GamePlay.Map.Coins;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private CoinControl _coinPrefab;
        private CoinControl _coin;

        private void OnEnable()
        {
            CoinControl.SetNewCoinOnSurvive += CoinReloadOnSurvive;
        }
        private void OnDisable()
        {
            CoinControl.SetNewCoinOnSurvive -= CoinReloadOnSurvive;
        }
        private void Start()
        {
            CoinReloadOnSurvive();
        }
        private void CoinReloadOnSurvive()
        {
            _coin = Instantiate(_coinPrefab, new Vector3(Random.Range(17, -17), 1, Random.Range(-17, 17)), Quaternion.identity); 
            _coin.SetQuantityAddCoins(1);
        }
    }
}