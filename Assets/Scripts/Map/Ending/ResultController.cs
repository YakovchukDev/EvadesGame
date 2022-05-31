using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Map.Coins;

namespace Map.Ending
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField] private Image _leftStar;
        [SerializeField] private Image _centralStar;
        [SerializeField] private Image _rightStar;
        [SerializeField] private TMP_Text _coinsTMP;
        [SerializeField] private CoinController _coinController;
        
        public void ShowResult()
        {
            _coinsTMP.text = _coinController.GetCoinsResult().ToString();
            _leftStar.gameObject.SetActive(MapManager.MainDataCollector.Level.UpStars);
            _centralStar.gameObject.SetActive(MapManager.MainDataCollector.Level.IsComplited);
            _rightStar.gameObject.SetActive(MapManager.MainDataCollector.Level.DownStars);
        }
    }
}
