using System.Collections.Generic;
using UnityEngine;

public class ButtonSpellPosition : MonoBehaviour
{
    [SerializeField] private List<GameObject> _leftSpellButtons;
    [SerializeField] private List<GameObject> _rightSpellButtons;

    private void Start()
    {
        SelectButtonPosition();
    }

    public void SelectButtonPosition()
    {
        if (PlayerPrefs.GetString("RightOrLeft") == "Right")
        {
            foreach (var rightSpellButton in _rightSpellButtons)
            {
                rightSpellButton.SetActive(false);
            }

            foreach (var leftSpellButton in _leftSpellButtons)
            {
                leftSpellButton.SetActive(true);
            }
        }
        else if (PlayerPrefs.GetString("RightOrLeft") == "Left")
        {
            foreach (var rightSpellButton in _rightSpellButtons)
            {
                rightSpellButton.SetActive(true);
            }

            foreach (var leftSpellButton in _leftSpellButtons)
            {
                leftSpellButton.SetActive(false);
            }
        }
    }
}