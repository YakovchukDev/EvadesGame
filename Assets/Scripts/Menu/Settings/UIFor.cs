using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Settings
{
    public class UIFor : MonoBehaviour
    {
        public static Vector2 Joystick;
        private string _rightOrLeft;
        [SerializeField] private TMP_Text _forWhoText;
        
        [SerializeField] private Image _thisImage;

        [SerializeField] private Sprite _forRightHanded;
        [SerializeField] private Sprite _forLeftHanded;

        private void Start()
        {
            RightOrLeftHanded();
            RightOrLeftHanded();
        }

        public void RightOrLeftHanded()
        {
            _rightOrLeft = PlayerPrefs.GetString("RightOrLeft");
            if (_rightOrLeft == "Right")
            {
                Joystick = new Vector3(840, 0, 0);
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
                _thisImage.sprite= _forRightHanded;
                PlayerPrefs.SetString("RightOrLeft", "Left");
            }
            else if (_rightOrLeft == "Left")
            {
                Joystick = new Vector3(-120, 0, 0);
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
                _thisImage.sprite= _forLeftHanded;
                PlayerPrefs.SetString("RightOrLeft", "Right");
            }
        }
    }
}