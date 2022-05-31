using System;
using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ScriptableObject.Info
{
    public class InfoElement : MonoBehaviour
    {
        [HideInInspector] public string _socialNetworkURL1;
        [HideInInspector] public string _socialNetworkURL2;
        private AudioManager _audioManager;
        [SerializeField] private Button _infoButton;
        [SerializeField] private GameObject _socialNetworkButton1;
        [SerializeField] private GameObject _socialNetworkButton2;
        [SerializeField] private Image _childSocialNetworkImage1;
        [SerializeField] private Image _childSocialNetworkImage2;
        [SerializeField] private Image _picture;
        [SerializeField] private TMP_Text _tmpText1;
        [SerializeField] private TMP_Text _tmpText2;
        [SerializeField] private GameObject _panel;

        public Button InfoButton => _infoButton;
        public GameObject SocialNetworkButton1 => _socialNetworkButton1;
        public GameObject SocialNetworkButton2 => _socialNetworkButton2;
        public Image ChildSocialNetworkImage1 => _childSocialNetworkImage1;
        public Image ChildSocialNetworkImage2 => _childSocialNetworkImage2;

        public string SocialNetworkURL1
        {
            get => _socialNetworkURL1;
            set => _socialNetworkURL1 = value;
        }

        public string SocialNetworkURL2
        {
            get => _socialNetworkURL2;
            set => _socialNetworkURL2 = value;
        }

        public Image Picture => _picture;
        public TMP_Text TmpText1 => _tmpText1;
        public TMP_Text TmpText2 => _tmpText2;
        public GameObject Panel => _panel;

        public event Action OnInfo;

        private void Start()
        {
            _audioManager = AudioManager.Instanse;
        }

        public void Initialize()
        {
            _infoButton.onClick.AddListener(ChangeInfoElement);
            _socialNetworkButton1.GetComponent<Button>().onClick.AddListener(OpenGmailURL);
            _socialNetworkButton2.GetComponent<Button>().onClick.AddListener(OpenTelegramURL);
        }

        private void ChangeInfoElement()
        {
            _audioManager.Play("PressButton");
            OnInfo?.Invoke();
        }

        private void OpenGmailURL()
        {
            Application.OpenURL(_socialNetworkURL1);
        }

        private void OpenTelegramURL()
        {
            Application.OpenURL(_socialNetworkURL2);
        }


        private void OnDestroy()
        {
            _infoButton.onClick.RemoveListener(ChangeInfoElement);
            _socialNetworkButton1.GetComponent<Button>().onClick.RemoveListener(OpenGmailURL);
            _socialNetworkButton2.GetComponent<Button>().onClick.RemoveListener(OpenTelegramURL);
        }
    }
}