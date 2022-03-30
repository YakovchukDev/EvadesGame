using System.Collections.Generic;
using GamePlay.Character.Spell;
using Joystick_Pack.Examples;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;

public class CharacterUpdate : MonoBehaviour
{
    [SerializeField] private ManaController _manaController;
    [SerializeField] private List<TMP_Text> _abilityLevel;
    private int _numberSpeedUpdate = 1;
    private int _numberMaxManaUpdate = 1;
    private int _numberManaRegenUpdate = 1;

    public static int NumberSpell1Update = 1;
    public static int NumberSpell2Update = 1;

    public static bool CanSpell1Update;
    public static bool CanSpell2Update;

    private float _allMana;

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

        _numberSpeedUpdate = 1;
        _numberMaxManaUpdate = 1;
        _numberManaRegenUpdate = 1;
        NumberSpell1Update = 1;
        NumberSpell2Update = 1;
        CanSpell1Update = false;
        CanSpell2Update = false;
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
                _allMana = 100;
                _allMana += 20 * _numberMaxManaUpdate;
                _manaController.SetAllMana(_allMana);
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
                _abilityLevel[3].text = NumberSpell1Update.ToString();
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
                _abilityLevel[4].text = NumberSpell2Update.ToString();
            }
        }
    }
}