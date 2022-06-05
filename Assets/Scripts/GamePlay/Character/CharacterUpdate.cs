using System;
using System.Collections.Generic;
using GamePlay.Character.Spell;
using Joystick_Pack.Examples;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Map.Expirience;
using Map;

namespace GamePlay.Character
{
    public class CharacterUpdate : MonoBehaviour
    {
        [SerializeField] private GameObject _simplifiedBoostPanel;
        [SerializeField] private GameObject _fullBoostPanel;
        [SerializeField] private ManaController _manaController;
        [SerializeField] private List<TMP_Text> _abilityLevel;
        [SerializeField] private List<Button> _updateButtons;
        private int _numberSpeedUpdate = 1;
        private int _numberMaxManaUpdate = 1;
        private int _numberManaRegenUpdate = 1;

        public static int NumberSpell1Update = 1;
        public static int NumberSpell2Update = 1;

        public static bool CanSpell1Update;
        public static bool CanSpell2Update;

        private float _allMana;
        private GameObject _levelValue;
        private TMP_Text _quantityTokensTMP;

        private void Start()
        {
            if (SelectionClassView.CharacterType == 0 || SelectionClassView.CharacterType == 1)
            {
                for (int i = 1; i < _updateButtons.Count; i++)
                {
                    _updateButtons[i].interactable = false;
                }
            }
            else if(SelectionClassView.CharacterType == 5)
            {
                _updateButtons[4].interactable = false;
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
            _levelValue = GameObject.Find("LevelValue");
            _quantityTokensTMP = _levelValue.GetComponent<TMP_Text>();
        }
        private void OnEnable()
        {
            ExpirienceControl.OpenBoostMenu += SetActiveBoostMenu;
            EntitiesGenerator.HandOverManaController += SetManaController;
            
        } 
        private void OnDisable()
        {
            ExpirienceControl.OpenBoostMenu -= SetActiveBoostMenu;
            EntitiesGenerator.HandOverManaController -= SetManaController;
        }

        private void SetManaController(ManaController manaController)
        {
            _manaController = manaController;
        }

        public void SpeedUpdate()
        {
            if (Convert.ToInt32(_quantityTokensTMP.text) > 0)
            {
                if (SelectionClassView.WhatPlaying == "Level")
                {
                    if (_numberSpeedUpdate <= 5)
                    {
                        JoystickPlayerExample.Speed += 2;
                        _numberSpeedUpdate++;
                        _abilityLevel[0].text = _numberSpeedUpdate.ToString();
                        _abilityLevel[5].text = _numberSpeedUpdate.ToString();
                        int value = Convert.ToInt32(_quantityTokensTMP.text);
                        _quantityTokensTMP.text = $"{--value}";
                    }
                }
            }
        }


        public void MaxManaUpdate()
        {
            if (Convert.ToInt32(_quantityTokensTMP.text) > 0)
            {
                if (SelectionClassView.WhatPlaying == "Level")
                {
                    if (_numberMaxManaUpdate <= 5)
                    {
                        _allMana = 100;
                        _allMana += 20 * _numberMaxManaUpdate;
                        ManaController.AllMana = _allMana;
                        _numberMaxManaUpdate++;
                        _abilityLevel[1].text = _numberMaxManaUpdate.ToString();
                        int value = Convert.ToInt32(_quantityTokensTMP.text);
                        _quantityTokensTMP.text = $"{--value}";
                    }
                }
            }
        }

        public void ManaRegenUpdate()
        {
            if (Convert.ToInt32(_quantityTokensTMP.text) > 0)
            {
                if (SelectionClassView.WhatPlaying == "Level")
                {
                    if (_numberManaRegenUpdate <= 5)
                    {
                        ManaController.Regen += 0.2f;
                        _numberManaRegenUpdate++;
                        _abilityLevel[2].text = _numberManaRegenUpdate.ToString();
                        int value = Convert.ToInt32(_quantityTokensTMP.text);
                        _quantityTokensTMP.text = $"{--value}";
                    }
                }
            }
        }

        public void Spell1Update()
        {
            if (Convert.ToInt32(_quantityTokensTMP.text) > 0)
            {
                if (SelectionClassView.WhatPlaying == "Level")
                {
                    if (NumberSpell1Update <= 5)
                    {
                        NumberSpell1Update++;
                        CanSpell1Update = true;
                        _abilityLevel[3].text = NumberSpell1Update.ToString();
                        int value = Convert.ToInt32(_quantityTokensTMP.text);
                        _quantityTokensTMP.text = $"{--value}";
                    }
                }
            }
        }

        public void Spell2Update()
        {
            if (Convert.ToInt32(_quantityTokensTMP.text) > 0)
            {
                if (SelectionClassView.WhatPlaying == "Level")
                {
                    if (NumberSpell2Update <= 5)
                    {
                        NumberSpell2Update++;
                        CanSpell2Update = true;
                        _abilityLevel[4].text = NumberSpell2Update.ToString();
                        int value = Convert.ToInt32(_quantityTokensTMP.text);
                        _quantityTokensTMP.text = $"{--value}";
                    }
                }
            }
        }
        private void SetActiveBoostMenu(bool isEnter)
        {
            if(_manaController != null)
            {
                try
                {
                    _fullBoostPanel.gameObject.SetActive(isEnter);   
                }catch{}
            }
            else
            {
                try
                {
                    _simplifiedBoostPanel.gameObject.SetActive(isEnter);  
                }catch{}
            }
        }
    }
}