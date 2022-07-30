using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ScriptableObject.Shop.CharacterShop
{
    public class ShopCharacterObject : MonoBehaviour
    {
        private AudioManager _audioManager;
        [SerializeField] private Image _backImage;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        public ShopCharacterPanel ShopCharacterPanel { get; set; }

        private void Start()
        {
            _backImage.sprite = ShopCharacterPanel.BackImage;
            _button.image.sprite = ShopCharacterPanel.BackText;
            _image.sprite = ShopCharacterPanel.Image;
            _text.text = "Price:" + ShopCharacterPanel.CharacterCost;
            _audioManager=AudioManager.Instanse;
            if (PlayerPrefs.GetInt($"OpenCharacter{ShopCharacterPanel.NumberForUnlock}") == 1)
            {
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _text.text = "Bought";
                        break;
                    case "Russian":
                        _text.text = "Куплено";
                        break;
                    case "Ukrainian":
                        _text.text = "Куплено";
                        break;
                }

                _button.interactable = false;
            }
        }

        public void BuyCharacter()
        {
            _audioManager.Play("PressButton");
            if (PlayerPrefs.GetInt("Coins") >= ShopCharacterPanel.CharacterCost)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - ShopCharacterPanel.CharacterCost);
                PlayerPrefs.SetInt($"OpenCharacter{ShopCharacterPanel.NumberForUnlock}", 1);
                switch (PlayerPrefs.GetString("Language"))
                {
                    case "English":
                        _text.text = "Bought";
                        break;
                    case "Russian":
                        _text.text = "Куплено";
                        break;
                    case "Ukrainian":
                        _text.text = "Куплено";
                        break;
                }

                PlayerPrefs.SetInt("CompanyOpened",1);
                _button.interactable = false;
            }
        }
    }
}