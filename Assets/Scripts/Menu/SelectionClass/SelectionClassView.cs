using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu.SelectionClass
{
    public class SelectionClassView : MonoBehaviour
    {
        [SerializeField] private ClassAvailability _classAvailability;
        public static int CharacterType { get; private set; }
        public static string WhatPlaying { get; set; }
        public List<Button> _buttonSelection;
        [SerializeField] private TMP_Text _infoTime;
        [SerializeField] private Image _progressLine;
        [SerializeField] private Animator _slime;
        private string[] _playerStr;

        public TMP_Text InfoTime => _infoTime;


        private void Start()
        {
            _playerStr = new string[]
            {
                "WeakTime",
                "NecroTime",
                "ShooterTime",
                "NeoTime",
                "TankTime",
                "NecromusTime"
            };
            ChoiceTypeOfCharacter(PlayerPrefs.GetInt("SelectionNumber"));
        }

        public void ChoiceTypeOfCharacter(int characterType)
        {
            CharacterType = characterType;
            PlayerPrefs.SetInt("SelectionNumber", characterType);
            foreach (var buttonSelection in _buttonSelection)
            {
                buttonSelection.GetComponent<RectTransform>().sizeDelta = new Vector2(250, 250);
            }

            if (_buttonSelection[CharacterType].interactable)
            {
                _buttonSelection[CharacterType].GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            }
            else
            {
                _buttonSelection[0].GetComponent<RectTransform>().sizeDelta = new Vector2(300, 300);
            }

            if (WhatPlaying == "Infinity")
            {
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _infoTime.text = "Record: " + Mathf.Round(PlayerPrefs.GetFloat(_playerStr[characterType]));
                        break;
                    case "Russian":
                        _infoTime.text = "Рекорд: " + Mathf.Round(PlayerPrefs.GetFloat(_playerStr[characterType]));
                        break;
                    case "Ukrainian":
                        _infoTime.text = "Рекорд: " + Mathf.Round(PlayerPrefs.GetFloat(_playerStr[characterType]));
                        break;
                }
            }
        }
        public void SetWhatPlaying(string whatPlaying)
        {
            WhatPlaying = whatPlaying;
            CharacterType = PlayerPrefs.GetInt("SelectionNumber");
            if (WhatPlaying == "Infinity")
            {
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _infoTime.text = "Record: " + Mathf.Round(PlayerPrefs.GetFloat(_playerStr[CharacterType]));
                        break;
                    case "Russian":
                        _infoTime.text = "Рекорд: " + Mathf.Round(PlayerPrefs.GetFloat(_playerStr[CharacterType]));
                        break;
                    case "Ukrainian":
                        _infoTime.text = "Рекорд: " + Mathf.Round(PlayerPrefs.GetFloat(_playerStr[CharacterType]));
                        break;
                }
            }
        }

        public void CheckWhatPlaying()
        {
            if (WhatPlaying == "Level")
            {
                StartCoroutine(AsyncLoadScene("Company"));
            }
            else if (WhatPlaying == "Infinity")
            {
                StartCoroutine(AsyncLoadScene("InfinityGame"));
            }

            else
            {
                Debug.LogError("Mode not selected");
            }
        }

        private IEnumerator AsyncLoadScene(string sceneName)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            _slime.Play(0);
            while (!operation.isDone)
            {
                _progressLine.fillAmount = operation.progress;
                yield return null;
            }
        }
    }
}