using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class DownGroundForCompany : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransformContent;
    private RectTransform _rectTransform;
    private int _numberPlusLenght;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private List<GameObject> _daughtersObject;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        if (_moveSpeed == 0)
        {
            _moveSpeed = 1;
        }
    }

    private void Update()
    {
        foreach (var daughterObject in _daughtersObject)
        {
            daughterObject.GetComponent<Image>().color = gameObject.GetComponent<Image>().color;
        }
        Move();
    }

    private void Move()
    {
        _rectTransform.anchoredPosition = new Vector2(
            (_rectTransformContent.anchoredPosition.x + _numberPlusLenght) / _moveSpeed,
            _rectTransform.anchoredPosition.y);
        if (_rectTransform.anchoredPosition.x <= -1910)
        {
            _numberPlusLenght += 1920;
        }
        else if (_rectTransform.anchoredPosition.x >= 1910)
        {
            _numberPlusLenght -= 1920;
        }
    }
}