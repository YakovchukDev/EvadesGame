using System;
using System.Collections;
using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class CompanyButton : MonoBehaviour
    {
        [SerializeField] private MenuAnimatorController _menuAnimatorController;
        [SerializeField] private SelectionClassView _selectionClassView;
        [SerializeField] private GameObject _companyInfo;
        [SerializeField] private GameObject _companyButton;
        [SerializeField] private GameObject _companyMenuView;
        [SerializeField] private AudioSource _pressButton;
        [SerializeField] private Animator _animatorButton;
        private bool _openClose;
        
        public static event Action<int> OnCompanyUnlocked;

        private void Start()
        {
            Initialize();
            if (PlayerPrefs.GetInt("CompanyOpened") == 0)
            {
                _companyButton.GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
            }

            OnCompanyUnlocked += ClickButton;
        }

        private void Update()
        {
            if (PlayerPrefs.GetInt("CompanyOpened") == 1)
            {
                _companyButton.GetComponent<Image>().color = new Color(1, 1, 1);
            }
        }

        private void Initialize()
        {
            _companyButton.GetComponent<Button>().onClick.AddListener(TryOpenCompanyPanel);
        }

        private void TryOpenCompanyPanel()
        {
            if (PlayerPrefs.GetInt("CompanyOpened") == 1)
            {
                _companyMenuView.SetActive(true);
                OnCompanyUnlocked?.Invoke(0);
                _selectionClassView.SetWhatPlaying("Level");
            }
            else
            {
                ClickButton(0);
                _openClose = !_openClose;
                if (_openClose)
                {
                    _companyInfo.SetActive(true);
                    _menuAnimatorController.CompanyLocked(0);
                }
                else
                {
                    _menuAnimatorController.CompanyLocked(1);
                }
            }
        }

        private void ClickButton(int index)
        {
            _pressButton.Play();
            _animatorButton.Play(index);
        }


        private void OnDestroy()
        {
            _companyButton.GetComponent<Button>().onClick.RemoveListener(TryOpenCompanyPanel);
            OnCompanyUnlocked -= ClickButton;
        }
    }
}