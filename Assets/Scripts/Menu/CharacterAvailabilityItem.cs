using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Menu.SelectionClass;

public class CharacterAvailabilityItem : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private PopUpPanelController _popUpPanel;
    [SerializeField] private List<string> _whatNeedForOpenText;
    private Translator _whatNeedForOpen;
    private Button _button;

    private void Start()
    {
        _name = $"{_name}Time";
        _button = GetComponent<Button>();
        _whatNeedForOpen = new Translator(_whatNeedForOpenText[0], _whatNeedForOpenText[1], _whatNeedForOpenText[2]);
        CheckOpenCompany();
    }
    private void OnEnable()
    {
        CheckOpenCharacter();
        ForEducationalLevel.OnEducationLevel += SlimesForEducation;
    }
    private void CheckOpenCompany()
    {
        if (PlayerPrefs.HasKey($"Open{_id}") && PlayerPrefs.HasKey(_name))
        {
            if (PlayerPrefs.GetInt($"Open{_id}") == 1 || PlayerPrefs.GetFloat(_name) >= 25)
            {
                PlayerPrefs.SetInt("CompanyOpened", 1);
            }
        }
    }

    private void OnDisable()
    {
        ForEducationalLevel.OnEducationLevel -= SlimesForEducation;

    }

    private void SlimesForEducation()
    {
        _button.interactable = _id == 5;
    }
    private void CheckOpenCharacter()
    {
        if(_button == null)
        {
            _button = GetComponent<Button>();
        }
        if (PlayerPrefs.HasKey($"Open{ _id}"))
        {
            if (PlayerPrefs.GetInt($"Open{_id}") == 1 || _id == 0)
            {
                print($"{_id} open {PlayerPrefs.GetInt($"Open{_id}")}");
                _button.interactable = true;
            }
            else
            {
                print($"{_id} close {PlayerPrefs.GetInt($"Open{_id}")}");
                _button.interactable = false;
            }
        }
        else if(_id != 0)
        {
            print($"{_id} close {PlayerPrefs.GetInt($"Open{_id}")}");
            _button.interactable = false;
        }
    }
    public void CheckOpenButton()
    {
        if(!_button.interactable && _whatNeedForOpen.GetText() != "")
        {
            _popUpPanel.OpenPopUpPanel(GetComponent<RectTransform>(), _whatNeedForOpen.GetText());
        }
    }
}
