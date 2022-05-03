using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings
{
    public class UIFor : MonoBehaviour
    {
        private string _rightOrLeft;
        [SerializeField] private SelectUIPosition _selectUIPosition;
        private ButtonSpellPosition _buttonSpellPosition;
        [SerializeField] private TMP_Text _forWhoText;

        [SerializeField] private Image _thisImage;

        [SerializeField] private Sprite _forRightHanded;
        [SerializeField] private Sprite _forLeftHanded;

        private void Start()
        {
            StartSet();
        }

        private void StartSet()
        {
            _rightOrLeft = PlayerPrefs.GetString("RightOrLeft");
            if (_rightOrLeft == "Right")
            {
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _forWhoText.text = "For left handed";
                        break;
                    case "Russian":
                        _forWhoText.text = "Для левшей";

                        break;
                    case "Ukrainian":
                        _forWhoText.text = "Для лівшів";
                        break;
                }

                _thisImage.sprite = _forLeftHanded;
            }
            else if (_rightOrLeft == "Left")
            {
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _forWhoText.text = "For right handed";
                        break;
                    case "Russian":
                        _forWhoText.text = "Для правшей";

                        break;
                    case "Ukrainian":
                        _forWhoText.text = "Для правшів";
                        break;
                }

                _thisImage.sprite = _forRightHanded;
            }
        }

        public void RightOrLeftHanded()
        {
            _rightOrLeft = PlayerPrefs.GetString("RightOrLeft");


            if (_rightOrLeft == "Right")
            {
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _forWhoText.text = "For right handed";
                        break;
                    case "Russian":
                        _forWhoText.text = "Для правшей";

                        break;
                    case "Ukrainian":
                        _forWhoText.text = "Для правшів";
                        break;
                }

                _thisImage.sprite = _forRightHanded;
                PlayerPrefs.SetString("RightOrLeft", "Left");
            }
            else if (_rightOrLeft == "Left")
            {
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _forWhoText.text = "For left handed";
                        break;
                    case "Russian":
                        _forWhoText.text = "Для левшей";

                        break;
                    case "Ukrainian":
                        _forWhoText.text = "Для лівшів";
                        break;
                }

                _thisImage.sprite = _forLeftHanded;
                PlayerPrefs.SetString("RightOrLeft", "Right");
            }

            _selectUIPosition.SelectJoystickPosition();
            _buttonSpellPosition = FindObjectOfType<ButtonSpellPosition>();
            if (_buttonSpellPosition != null)
            {
                _buttonSpellPosition.SelectButtonPosition();
            }
        }
    }
}