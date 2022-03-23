using System;
using System.Collections.Generic;
using Menu.SelectionClass;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateSystem : SelectionClassView

{
    /*private string[] _characterType = {"Just", "Necro", "Morfe", "Neo", "Invulnerable", "Nexus"};

    [SerializeField] private int[] _speedLvl = new int[6];
    [SerializeField] private int[] _maxManaLvl = new int[4];
    [SerializeField] private int[] _manaRegenLvl = new int[4];
    [SerializeField] private int[] _spell1Lvl = new int[4];
    [SerializeField] private int[] _spell2Lvl = new int[4];
    [SerializeField] private List<TMP_Text> _allLevel;

    private void Start()
    {
        ResetSkills();
    }

    public void ResetSkills()
    {
        for (int i = 0; i < 5; i++)
        {
            _speedLvl[i] = PlayerPrefs.GetInt($"speedLvl{i}");
            _maxManaLvl[i] = 0;
            _manaRegenLvl[i] = 0;
            _spell1Lvl[i] = 0;
            _spell2Lvl[i] = 0;
        }
    }

    public void SetAbilities(int characterNumber)
    {
        _allLevel[0].text = _speedLvl[characterNumber].ToString();
    }

    public void Speed()
    {
        _speedLvl[CharacterType]++;
        PlayerPrefs.SetInt($"speedLvl{CharacterType}",_speedLvl[CharacterType]);
        _allLevel[0].text = _speedLvl[CharacterType].ToString();
    }

    public void Mana()
    {
        _maxManaLvl[CharacterType]++;
        _allLevel[1].text = _maxManaLvl[CharacterType].ToString();
    }

    public void ManaRegen()
    {
        _manaRegenLvl[CharacterType]++;
        _allLevel[2].text = _manaRegenLvl[CharacterType].ToString();
    }

    public void Spell1()
    {
        _spell1Lvl[CharacterType]++;
        _allLevel[3].text = _spell1Lvl[CharacterType].ToString();
    }

    public void Spell2()
    {
        _spell2Lvl[CharacterType]++;
        _allLevel[4].text = _spell2Lvl[CharacterType].ToString();
    }*/
}