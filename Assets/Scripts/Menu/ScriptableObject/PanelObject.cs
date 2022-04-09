using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ScriptableObject
{
    public class PanelObject : MonoBehaviour
    {
        [SerializeField] private InfoPanel _infoPanel;
        [SerializeField] private Image _backImage;
        [SerializeField] private Image _backText;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        public InfoPanel InfoPanel { get; set; }

        private void Start()
        {
            _backImage.sprite = InfoPanel.BackImage;
            _backText.sprite = InfoPanel.BackText;
            _image.sprite = InfoPanel.Image;
            switch (PlayerPrefs.GetString("Language"))
            {
                case "English":
                    _text.text = InfoPanel.EnglishText;
                    break;
                case "Russian":
                    _text.text = InfoPanel.RussianText;
                    break;
                case "Ukrainian":
                    _text.text = InfoPanel.UkrainianText;
                    break;
            }
        }
    }
}