using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ScriptableObject.Info
{
    public class InfoController : MonoBehaviour
    {
        
        [SerializeField] private Vector2[] _positions;

        [SerializeField] private List<InfoPanel> _infoPanels;
        [SerializeField] private InfoElement _elementGameObject;
        [SerializeField] private Transform _grid;
        [SerializeField] [Min(0.01f)] private float _scrollSpeed;
        private List<InfoElement> _personsElements;
        private List<RectTransform> _rectTransforms;
        public int _turnOffComponentNumber = 3;
        private int _forFirstSibling;
        private int _forLastSibling = 2;

        private void Start()
        {
            _personsElements = new List<InfoElement>();
            SpawnInfoElement();
            _personsElements[1].transform.SetAsFirstSibling();
            for (int i = 0; i < _positions.Length; i++)
            {
                _personsElements[i].Panel.SetActive(true);
                _personsElements[i].GetComponent<RectTransform>().anchoredPosition = _positions[i];
                _personsElements[i].Initialize();
                _personsElements[i].OnInfo += ChangePosition;
            }

            _personsElements[_turnOffComponentNumber].Panel.SetActive(false);

            _turnOffComponentNumber--;
            if (_turnOffComponentNumber < 0)
            {
                _turnOffComponentNumber = 3;
            }

            _rectTransforms = new List<RectTransform>();
            foreach (var personsElements in _personsElements)
            {
                _rectTransforms.Add(personsElements.GetComponent<RectTransform>());
            }
        }

        private void SpawnInfoElement()
        {
            for (int i = 0; i < _infoPanels.Count; i++)
            {
                _personsElements.Add(Instantiate(_elementGameObject, _grid));

                _personsElements[i].Picture.sprite = _infoPanels[i].Picture;
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _personsElements[i].TmpText1.text = _infoPanels[i].EnglishText1;
                        _personsElements[i].TmpText2.text = _infoPanels[i].EnglishText2;
                        break;
                    case "Russian":
                        _personsElements[i].TmpText1.text = _infoPanels[i].RussianText1;
                        _personsElements[i].TmpText2.text = _infoPanels[i].RussianText2;
                        break;
                    case "Ukrainian":
                        _personsElements[i].TmpText1.text = _infoPanels[i].UkrainianText1;
                        _personsElements[i].TmpText2.text = _infoPanels[i].UkrainianText2;
                        break;
                }

                _personsElements[i].ChildSocialNetworkImage1.sprite =
                    _infoPanels[i].SocialNetworkSprite1;
                _personsElements[i].ChildSocialNetworkImage2.sprite =
                    _infoPanels[i].SocialNetworkSprite2;

                _personsElements[i].SocialNetworkButton1.GetComponent<Image>().color =
                    _infoPanels[i].SocialNetworkColor1;
                _personsElements[i].SocialNetworkButton2.GetComponent<Image>().color =
                    _infoPanels[i].SocialNetworkColor2;

                _personsElements[i].SocialNetworkURL1 = _infoPanels[i].URL1;
                _personsElements[i].SocialNetworkURL2 = _infoPanels[i].URL2;
            }
        }

        private void ChangePosition()
        {
            var firstAsLastPosition = _positions[0];
            for (int i = 0; i < _positions.Length; i++)
            {
                if (i + 1 < _positions.Length)
                {
                    _positions[i] = _positions[i + 1];
                }
                else
                {
                    _positions[i] = firstAsLastPosition;
                }

                StartCoroutine(SmoothMovement(i));
            }

            SetElseSibling();
            PanelColor();
        }

        private IEnumerator SmoothMovement(int i)
        {
            _personsElements[i].InfoButton.enabled = false;

            while (Vector2.Distance(_rectTransforms[i].anchoredPosition, _positions[i]) > 1)
            {
                _rectTransforms[i].anchoredPosition = Vector2.Lerp(_rectTransforms[i].anchoredPosition,
                    _positions[i], _scrollSpeed);
                if (Vector2.Distance(_rectTransforms[i].anchoredPosition, _positions[i]) > 10)
                {
                    _personsElements[i].InfoButton.enabled = true;
                }

                yield return new WaitForEndOfFrame();
            }
        }

        private void PanelColor()
        {
            foreach (var panelComponent in _personsElements)
            {
                panelComponent.Panel.SetActive(true);
                _personsElements[_turnOffComponentNumber].Panel.SetActive(false);
            }

            _turnOffComponentNumber--;
            if (_turnOffComponentNumber < 0)
            {
                _turnOffComponentNumber = 3;
            }
        }

        private void SetElseSibling()
        {
            _personsElements[_forFirstSibling].transform.SetAsFirstSibling();
            _personsElements[_forLastSibling].transform.SetAsLastSibling();
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
}