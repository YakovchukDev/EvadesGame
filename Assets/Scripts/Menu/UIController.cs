using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> _spellButtons;
        private VariableJoystick _variableJoystick;

        private void Start()
        {
            _variableJoystick = FindObjectOfType<VariableJoystick>();
         
            if (_variableJoystick != null)
            {
                _variableJoystick.transform.position = UIFor.Joystick;
            }

            if (_spellButtons != null)
            {
                foreach (var spellButton in _spellButtons)
                {
                    spellButton.anchoredPosition = UIFor.SpellButtons;
                }
            }
        }
    }
}