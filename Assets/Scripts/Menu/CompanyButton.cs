using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class CompanyButton : MonoBehaviour
    {
        private AudioManager _audioManager;
        [SerializeField] private SelectionClassView _selectionClassView;
        [SerializeField] private Canvas _canvasMainMenu;
        [SerializeField] private GameObject _companyButton;
        [SerializeField] private GameObject _companyMenuView;
        [SerializeField] private Animator _animatorButton;

        public static event Action<int> OnCompanyUnlocked;
        public static event Action<int> OnEducation;

        private void Start()
        {
            Initialize();
            _audioManager = AudioManager.Instanse;


            OnCompanyUnlocked += ClickButton;
        }

        private void Initialize()
        {
            _companyButton.GetComponent<Button>().onClick.AddListener(TryOpenCompanyPanel);
        }

        private void TryOpenCompanyPanel()
        {
            _companyMenuView.SetActive(true);
            OnCompanyUnlocked?.Invoke(0);
            OnEducation?.Invoke(1);
            _selectionClassView.SetWhatPlaying("Level");
            _canvasMainMenu.enabled = false;
        }

        

        private void ClickButton(int index)
        {
            _audioManager.Play("PressButton");
            _animatorButton.Play(index);
        }


        private void OnDestroy()
        {
            _companyButton.GetComponent<Button>().onClick.RemoveListener(TryOpenCompanyPanel);
            OnCompanyUnlocked -= ClickButton;
        }
    }
}