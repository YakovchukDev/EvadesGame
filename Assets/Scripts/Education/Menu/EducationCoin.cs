using GamePlay.Map.Coins;
using UnityEngine;

public class EducationCoin : MonoBehaviour
{
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
        _coin.SetQuantityAddCoins(1);
    }
}
