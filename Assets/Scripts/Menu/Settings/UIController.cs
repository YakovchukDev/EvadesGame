using System.Collections.Generic;
using UnityEngine;

namespace Menu.Settings
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> _spellButtons;
        private VariableJoystick _variableJoystick;

        private void Start()
        {
            _variableJoystick = FindObjectOfType<VariableJoystick>();
        }

        private void Update()
        {
            UIChange();
        }

        private void UIChange()
        {
            if (_variableJoystick != null)
            {
                _variableJoystick.transform.position = UIFor.Joystick;
            }

            if (_spellButtons != null)
            {
                foreach (var spellButton in _spellButtons)
                {
                    if (PlayerPrefs.GetString("RightOrLeft") == "Right")
                    {
                        spellButton.anchoredPosition=new Vector2(300,200); 
                    }
                    else if(PlayerPrefs.GetString("RightOrLeft") == "Left")
                    {
                        spellButton.anchoredPosition=new Vector2(1620,200);
                    }
                }
            }
        }
    }
}