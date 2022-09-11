using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Map.Coins;

namespace Map.Ending
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField] private List<Image> _listStars;
        [SerializeField] private List<GameObject> _starBackgrounds;
        [SerializeField] private TMP_Text _coinsTMP;
        [SerializeField] private CoinController _coinController;
        [SerializeField] private GameObject _background;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private GameObject _coinsBlock;
        [SerializeField] private GameObject _exitButton;
        [SerializeField] private List<GameObject> _objToHide;

        private void Start()
        {
            _background.transform.localScale = new Vector3(0, 0, 0);
            _background.GetComponent<Image>().color = new Color(
                _background.GetComponent<Image>().color.r,
                _background.GetComponent<Image>().color.g,
                _background.GetComponent<Image>().color.b,
                0);
            _title.color = new Color(_title.color.r, _title.color.g, _title.color.b, 0);
            foreach (GameObject starBackground in _starBackgrounds)
            {
                starBackground.transform.localScale = new Vector3(0, 0, 0);
                starBackground.GetComponent<Image>().color = new Color(
                    starBackground.GetComponent<Image>().color.r,
                    starBackground.GetComponent<Image>().color.g,
                    starBackground.GetComponent<Image>().color.b,
                    0);
            }

            for (int i = 0; i < _listStars.Count; i++)
            {
                _listStars[i].color = new Color(
                    _listStars[i].GetComponent<Image>().color.r,
                    _listStars[i].GetComponent<Image>().color.g,
                    _listStars[i].GetComponent<Image>().color.b,
                    0);
            }

            _coinsBlock.transform.localScale = new Vector3(0, 0, 0);
            _coinsTMP.text = "";
            _exitButton.SetActive(false);
            _exitButton.GetComponent<Image>().color = new Color(
                _exitButton.GetComponent<Image>().color.r,
                _exitButton.GetComponent<Image>().color.g,
                _exitButton.GetComponent<Image>().color.b,
                0);
        }

        public IEnumerator ShowResult()
        {
            foreach (GameObject obj in _objToHide)
            {
                obj.SetActive(false);
            }

            _background.transform.DOScale(new Vector3(1, 1, 1), 0.7f);
            _background.GetComponent<Image>().DOFade(100 / 255f, 0.7f);
            yield return new WaitForSeconds(0.3f);
            _title.DOFade(1f, 0.5f);
            yield return new WaitForSeconds(0.7f);
            foreach (GameObject starBackground in _starBackgrounds)
            {
                starBackground.transform.DOScale(new Vector3(1, 1, 1), 0.4f);
                starBackground.GetComponent<Image>().DOFade(200 / 255f, 0.4f);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.2f);
            if (PlayerPrefs.HasKey($"Level{MapManager.MainDataCollector.GetLevelNumber()}"))
            {
                string stars = PlayerPrefs.GetString($"Level{MapManager.MainDataCollector.GetLevelNumber()}");
                int count = 0;
                for (int i = 1; i < stars.Length; i++)
                {
                    if (stars[i] == '*')
                    {
                        count++;
                    }
                }

                for (int i = 0; i < _listStars.Count; i++)
                {
                    if (count > 0)
                    {
                        _listStars[i].DOFade(1, 0.5f);
                        count--;
                    }
                }
            }

            yield return new WaitForSeconds(0.3f);
            _coinsBlock.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            yield return new WaitForSeconds(0.3f);
            _coinsTMP.text = _coinController.GetCoinsResult().ToString();
            yield return new WaitForSeconds(0.3f);
            _exitButton.SetActive(true);
            _exitButton.GetComponent<Image>().DOFade(1, 1f);
            yield return new WaitForSeconds(1f);
            Time.timeScale = 0;
        }
    }
}