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

        private void Start()
        {
            _backImage.sprite = _infoPanel.BackImage;
            _backText.sprite = _infoPanel.BackText;
            _image.sprite = _infoPanel.Image;
            switch (PlayerPrefs.GetString("Language"))
            {
                case "English":
                    _text.text = _infoPanel.EnglishText;
                    break;
                case "Russian":
                    _text.text = _infoPanel.RussianText;
                    break;
                case "Ukrainian":
                    _text.text = _infoPanel.UkrainianText;
                    break;
            }
        }
    }
}