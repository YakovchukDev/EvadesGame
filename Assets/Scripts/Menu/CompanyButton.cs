using System;
using System.Collections;
using Audio;
using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class CompanyButton : MonoBehaviour
    {
        private AudioManager _audioManager;
        [SerializeField] private MenuAnimatorController _menuAnimatorController;
        [SerializeField] private SelectionClassView _selectionClassView;
        [SerializeField] private Canvas _canvasMainMenu;
        [SerializeField] private GameObject _companyInfo;
        [SerializeField] private GameObject _companyButton;
        [SerializeField] private GameObject _companyMenuView;
        [SerializeField] private Animator _animatorButton;
        private bool _openClose;
        private Image _companyImage;

        public static event Action<int> OnCompanyUnlocked;

        private void Start()
        {
            _companyImage = _companyButton.GetComponent<Image>();
            Initialize();
            _audioManager = AudioManager.Instanse;
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
                _companyImage.color = new Color(1, 1, 1);
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
                _canvasMainMenu.enabled = false;
            }
            else
            {
                ClickButton(0);
                _openClose = !_openClose;
                if (_openClose)
                {
                    _companyInfo.SetActive(true);
                    _menuAnimatorController.CompanyLocked(0);
                    StartCoroutine(CloseCompanyInfo());
                }
                else
                {
                    _menuAnimatorController.CompanyLocked(1);
                }
            }
        }

        private IEnumerator CloseCompanyInfo()
        {
            yield return new WaitForSeconds(15);
            _menuAnimatorController.CompanyLocked(1);
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