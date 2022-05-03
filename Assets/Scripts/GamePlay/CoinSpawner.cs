using MapGeneration.Coins;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    private GameObject _coin;
    [SerializeField] private TMP_Text _coinTMP;


    private void Start()
    {
        _coin = Instantiate(_coinPrefab, new Vector3(Random.Range(17, -17), 1, Random.Range(-17, 17)),
            Quaternion.identity);
        CoinControl.Survive += CoinReloadOnSurvive;
        _coinTMP.text = "0";
        //CoinControl.GiveCoin += UpdateQuantityCoin;
        //  GeneralParameters.LoadedGeneralParameters += StartWork;
    }

    private void CoinReloadOnSurvive()
    {
        _coin.transform.position = new Vector3(Random.Range(17, -17), 1, Random.Range(-17, 17));
        _coin.SetActive(true);
    }

    /*private void StartWork()
    {
        _coinTMP.text = $"{GeneralParameters.MainDataCollector.Coins}";
    }

    private void UpdateQuantityCoin()
    {
        //GeneralParameters.MainDataCollector.Coins++;
        _coinTMP.text = $"{GeneralParameters.MainDataCollector.Coins}";
    }*/

    private void OnDestroy()
    {
        CoinControl.Survive -= CoinReloadOnSurvive;
       // CoinControl.GiveCoin -= UpdateQuantityCoin;
        //GeneralParameters.LoadedGeneralParameters -= StartWork;
    }
}