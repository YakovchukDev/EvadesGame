using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class PopUpPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private Image _panel;
    private RectTransform _lastStartElement;

    private void Start()
    {
        _panel.DOFade(0, 0);
        _image.DOFade(0, 0);
        _panel.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
    }
    public void OpenPopUpPanel(RectTransform rectTransform, string text)
    {
        _text.text = text;
        _image.rectTransform.transform.DOMove(rectTransform.transform.position, 0);
        _image.rectTransform.sizeDelta = new Vector2(200f, 200f);

        float showTime = 0.4f;
        _image.gameObject.SetActive(true);
        _panel.gameObject.SetActive(true);
        _panel.DOFade(60f/255, showTime);
        _image.rectTransform.DOSizeDelta(new Vector2(1600, 610), showTime);
        _image.rectTransform.DOAnchorPos(new Vector2(105, 202), showTime);
        _image.DOFade(0.9f, showTime);
        _text.gameObject.SetActive(true);

        _lastStartElement = rectTransform;
    }
    public void ClosePopUpPanel()
    {
        float hideTime = 0.4f;
        _panel.DOFade(0, hideTime);
        _image.rectTransform.transform.DOMove(_lastStartElement.transform.position, hideTime);
        _image.rectTransform.DOSizeDelta(new Vector2(200, 200), hideTime);
        _image.DOFade(0, hideTime);
        StartCoroutine(TimeDelay(hideTime));
        
    }

    private IEnumerator TimeDelay(float time)
    {
        yield return new WaitForSeconds(time);
        _panel.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
    }
}
