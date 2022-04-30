using System.Collections.Generic;
using Menu.level;
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
        public static string WhatPlaying { get; private set; }
        [SerializeField] private List<Button> _buttonSelection;
        [SerializeField] private TMP_Text _infoTime;


        private void Start()
        {
            ChoiceTypeOfCharacter(PlayerPrefs.GetInt("SelectionNumber"));
        }

        public void ChoiceTypeOfCharacter(int characterType)
        {
            CharacterType = characterType;
            PlayerPrefs.SetInt("SelectionNumber", characterType);
            foreach (var buttonSelection in _buttonSelection)
            {
                buttonSelection.transform.localScale = new Vector3(1, 1, 1);
            }

            _buttonSelection[CharacterType].transform.localScale = new Vector3(1.2f, 1.2f, 1);

            switch (PlayerPrefs.GetString("Language"))
            {
                case "English":
                    _infoTime.text = "Record: " + Mathf.Round(_classAvailability.RecordTime[characterType]);
                    break;
                case "Russian":
                    _infoTime.text = "Рекорд: " + Mathf.Round(_classAvailability.RecordTime[characterType]);
                    break;
                case "Ukrainian":
                    _infoTime.text = "Рекорд: " + Mathf.Round(_classAvailability.RecordTime[characterType]);
                    break;
            }
        }

        public void SetWhatPlaying(string whatPlaying)
        {
            WhatPlaying = whatPlaying;
        }

        public void CheckWhatPlaying()
        {
            if (WhatPlaying == "Level")
            {
                SceneManager.LoadScene("MapGeneratorBeta");
            }
            else if (WhatPlaying == "Infinity")
            {
                SceneManager.LoadScene("InfinityGame");
            }
        }
    }
}