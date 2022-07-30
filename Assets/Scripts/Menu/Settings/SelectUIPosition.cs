using UnityEngine;

namespace Menu.Settings
{
    public class SelectUIPosition : MonoBehaviour
    {
        [SerializeField] private GameObject _leftVariableJoystick;
        [SerializeField] private GameObject _rightVariableJoystick;

        [SerializeField] private GameObject _leftPanelDie;
        [SerializeField] private GameObject _rightPanelDie;

        private void Start()
        {
            SelectJoystickPosition();
        }


        public void SelectJoystickPosition()
        {
            if (PlayerPrefs.GetString("RightOrLeft") == "Right")
            {
                _leftVariableJoystick.gameObject.SetActive(false);
                _rightVariableJoystick.gameObject.SetActive(true);
            }
            else if (PlayerPrefs.GetString("RightOrLeft") == "Left")
            {
                _leftVariableJoystick.gameObject.SetActive(true);
                _rightVariableJoystick.gameObject.SetActive(false);
            }
        }


        public void OpenDiePanel()
        {
            if (PlayerPrefs.GetString("RightOrLeft") == "Right")
            {
                _leftPanelDie.SetActive(false);
                _rightPanelDie.SetActive(false);
                _rightPanelDie.SetActive(true);
            }
            else if (PlayerPrefs.GetString("RightOrLeft") == "Left")
            {
                _leftPanelDie.SetActive(false);
                _leftPanelDie.SetActive(true);
                _rightPanelDie.SetActive(false);
            }
        }
    }
}