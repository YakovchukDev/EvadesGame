using TMPro;
using UnityEngine;

namespace Menu
{
    public class UIFor : MonoBehaviour
    {
        public static Vector2 SpellButtons;
        public static Vector2 Joystick;
        private string _rightOrLeft;
        [SerializeField] private TMP_Text _forWho;

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
                SpellButtons = new Vector2(300, 300);
                _forWho.text = "RightHanded";
                PlayerPrefs.SetString("RightOrLeft", "Left");
            }
            else if (_rightOrLeft == "Left")
            {
                Joystick = new Vector3(-120, 0, 0);
                SpellButtons = new Vector2(1620, 300);
                _forWho.text = "LeftHanded";
                PlayerPrefs.SetString("RightOrLeft", "Right");
            }
        }
    }
}