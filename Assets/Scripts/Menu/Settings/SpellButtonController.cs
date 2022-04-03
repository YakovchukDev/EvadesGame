using System.Collections.Generic;
using UnityEngine;

namespace Menu.Settings
{
    public class SpellButtonController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _leftSpellButtons;
        [SerializeField] private List<GameObject> _rightSpellButtons;

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
            }        }

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
    }
}