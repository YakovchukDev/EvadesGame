using System.Collections.Generic;
using GamePlay.Character.Spell;
using Joystick_Pack.Examples;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;

public class CharacterUpdate : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _abilityLevel;
    private int _numberSpeedUpdate = 1;
    private int _numberMaxManaUpdate = 1;
    private int _numberManaRegenUpdate = 1;

    public static int NumberSpell1Update = 1;
    public static int NumberSpell2Update = 1;

    public static bool CanSpell1Update;
    public static bool CanSpell2Update;

    private void Start()
    {
        if (SelectionClassView.WhatPlaying == "Infinity")
        {
            Destroy(gameObject);
        }
        foreach (var abilityLevel in _abilityLevel)
        {
            abilityLevel.text = "1";
        }
    }

    public void SpeedUpdate()
    {
        if (SelectionClassView.WhatPlaying == "Level")
        {
            if (_numberSpeedUpdate <= 5)
            {
                JoystickPlayerExample.Speed += 2;
                _numberSpeedUpdate++;
                _abilityLevel[0].text = _numberSpeedUpdate.ToString();
            }
        }
    }

    public void MaxManaUpdate()
    {
        if (SelectionClassView.WhatPlaying == "Level")
        {
            if (_numberMaxManaUpdate <= 5)
            {
                ManaController.Mana = 100;
                ManaController.Mana += 20 * _numberMaxManaUpdate;
                _numberMaxManaUpdate++;
                _abilityLevel[1].text = _numberMaxManaUpdate.ToString();
            }
        }
    }

    public void ManaRegenUpdate()
    {
        if (SelectionClassView.WhatPlaying == "Level")
        {
            if (_numberManaRegenUpdate <= 5)
            {
                ManaController.Regen += 0.2f;
                _numberManaRegenUpdate++;
                _abilityLevel[2].text = _numberManaRegenUpdate.ToString();
            }
        }
    }

    public void Spell1Update()
    {
        if (SelectionClassView.WhatPlaying == "Level")
        {
            if (NumberSpell1Update <= 5)
            {
                NumberSpell1Update++;
                CanSpell1Update = true;
                _abilityLevel[3].text = CanSpell1Update.ToString();
            }
        }
    }

    public void Spell2Update()
    {
        if (SelectionClassView.WhatPlaying == "Level")
        {
            if (NumberSpell2Update <= 5)
            {
                NumberSpell2Update++;
                CanSpell2Update = true;
                _abilityLevel[4].text = CanSpell2Update.ToString();
            }
        }
    }
}