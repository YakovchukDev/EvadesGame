using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu.SelectionClass
{
    public class SelectionClassView : MonoBehaviour
    {
        public static int CharacterType { get; private set; }
        public static string WhatPlaying { get; private set; }
        [SerializeField] private List<Animator> _buttonSelectionAnimator;
        [SerializeField] private List<Button> _buttonSelection;
        private int _selectNumber;

        private void Start()
        {
            ChoiceTypeOfCharacter(PlayerPrefs.GetInt("SelectionNumber"));
        }

        private void Update()
        {
            if (_selectNumber != PlayerPrefs.GetInt("SelectionNumber") &&
                _buttonSelection[_selectNumber].interactable)
            {
                _buttonSelectionAnimator[_selectNumber].SetTrigger("Normal");
                _selectNumber++;
                if (_selectNumber == _buttonSelectionAnimator.Count)
                {
                    _selectNumber = 0;
                }
            }
            else if(_selectNumber == PlayerPrefs.GetInt("SelectionNumber")&&
                    _buttonSelection[_selectNumber].interactable)
            {
                _buttonSelectionAnimator[_selectNumber].SetTrigger("Selected");
                _selectNumber++;
                if (_selectNumber == _buttonSelectionAnimator.Count)
                {
                    _selectNumber = 0;
                }
            }
            else
            {
                _buttonSelectionAnimator[_selectNumber].SetTrigger("Disabled");
            }
        }

        public void ChoiceTypeOfCharacter(int characterType)
        {
            CharacterType = characterType;
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