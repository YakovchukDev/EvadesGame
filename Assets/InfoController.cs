using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    [SerializeField] private List<RectTransform> _positions;
    [SerializeField] private List<GameObject> _personsElement;
    private int _forFirstSibling = 1;
    private int _forLastSibling = 3;

    private void Start()
    {
        for (int i = 0; i < _positions.Count; i++)
        {
            _personsElement[i].GetComponent<RectTransform>().anchoredPosition = _positions[i].anchoredPosition;
        }
    }

    public void ChangePosition()
    {
        var firstAsLastPosition = _positions[0];

        for (int i = 0; i < _positions.Count; i++)
        {
            if (i + 1 < _positions.Count)
            {
                _positions[i] = _positions[i + 1];
            }
            else
            {
                _positions[i] = firstAsLastPosition;
            }

            _personsElement[i].GetComponent<RectTransform>().anchoredPosition = _positions[i].anchoredPosition;
        }
        SetElseSibling();
    }

    private void SetElseSibling()
    {
        _personsElement[_forFirstSibling].transform.SetAsFirstSibling();
        _personsElement[_forLastSibling].transform.SetAsLastSibling();
        _forFirstSibling--;
        _forLastSibling--;
        if (_forFirstSibling < 0)
        {
            _forFirstSibling = 3;
        }

        if (_forLastSibling < 0)
        {
            _forLastSibling = 3;
        }
    }
}