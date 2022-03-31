using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.SelectionClass
{
    public class SelectionClassView : MonoBehaviour
    {
        protected static int CharacterType { get; private set; }
        public static string WhatPlaying { get; private set; }

        private void Start()
        {
            ChoiceTypeOfCharacter(PlayerPrefs.GetInt("SelectionNumber"));
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