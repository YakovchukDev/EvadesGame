using System.Collections.Generic;
using UnityEngine;

namespace Menu.Settings
{
    public class JoystickController : MonoBehaviour
    {
        [SerializeField] private GameObject _leftVariableJoystick;
        [SerializeField] private GameObject _rightVariableJoystick;

        private void Start()
        {
            if (PlayerPrefs.GetString("RightOrLeft") == "Right")
            {
                _rightVariableJoystick.gameObject.SetActive(false);
                _leftVariableJoystick.gameObject.SetActive(true);
            }
            else if (PlayerPrefs.GetString("RightOrLeft") == "Left")
            {
                _rightVariableJoystick.gameObject.SetActive(true);
                _leftVariableJoystick.gameObject.SetActive(false);
            }
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

       
    }
}