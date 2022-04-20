using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Map
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField] private Image _leftStar;
        [SerializeField] private Image _centralStar;
        [SerializeField] private Image _rightStar;
        [SerializeField] private TMP_Text _coinsTMP;

        public void ShowResult()
        {
            _coinsTMP.text = $"{GeneralParameters.MainDataCollector.Coins}";
            _leftStar.gameObject.SetActive(GeneralParameters.MainDataCollector.Level.LeftStars);
            _centralStar.gameObject.SetActive(GeneralParameters.MainDataCollector.Level.IsComplited);
            _rightStar.gameObject.SetActive(GeneralParameters.MainDataCollector.Level.RightStars);
        }
    }
}
