using System.Collections.Generic;
using UnityEngine;

namespace Menu.Settings
{
    public class SelectUIPosition : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _leftSpellButtons;
        [SerializeField] private List<GameObject> _rightSpellButtons;
        [SerializeField] private GameObject _leftPanelDie;
        [SerializeField] private GameObject _rightPanelDie;

        private void Start()
        {
            if (PlayerPrefs.GetString("RightOrLeft") == "Right")
            {
                foreach (var spellButton in _rightSpellButtons)
                {
                    spellButton.SetActive(true);
                }

                foreach (var spellButton in _leftSpellButtons)
                {
                    spellButton.SetActive(false);
                }
            }
            else if (PlayerPrefs.GetString("RightOrLeft") == "Left")
            {
                foreach (var spellButton in _rightSpellButtons)
                {
                    spellButton.SetActive(false);
                }

                foreach (var spellButton in _leftSpellButtons)
                {
                    spellButton.SetActive(true);
                }
            }
        }

        public void SelectButtonPosition()
        {
            if (PlayerPrefs.GetString("RightOrLeft") == "Right")
            {
                foreach (var spellButton in _rightSpellButtons)
                {
                    spellButton.SetActive(false);
                }

                foreach (var spellButton in _leftSpellButtons)
                {
                    spellButton.SetActive(true);
                }
            }
            else if (PlayerPrefs.GetString("RightOrLeft") == "Left")
            {
                foreach (var spellButton in _rightSpellButtons)
                {
                    spellButton.SetActive(true);
                }

                foreach (var spellButton in _leftSpellButtons)
                {
                    spellButton.SetActive(false);
                }
            }
        }

        public void SelectDiePanelPosition()
        {
            if (PlayerPrefs.GetString("RightOrLeft") == "Right")
            {
                _leftPanelDie.SetActive(true);
                _rightPanelDie.SetActive(false);
            }
            else if (PlayerPrefs.GetString("RightOrLeft") == "Left")
            {
                _leftPanelDie.SetActive(false);
                _rightPanelDie.SetActive(true);
            }
        }
    }
}