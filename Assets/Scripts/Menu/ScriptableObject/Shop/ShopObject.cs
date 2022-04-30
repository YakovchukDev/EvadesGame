using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ScriptableObject.Shop
{
    public class ShopObject : MonoBehaviour
    {
        [SerializeField] private Image _backImage;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        public ShopPanel ShopPanel { get; set; }

        private void Start()
        {
            _backImage.sprite = ShopPanel.BackImage;
            _button.image.sprite = ShopPanel.BackText;
            _image.sprite = ShopPanel.Image;
            _text.text = "Price:" + ShopPanel.CharacterCost;

            if (PlayerPrefs.GetInt($"Open{ShopPanel.NumberForUnlock}") == 1)
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
            if (PlayerPrefs.GetInt("Coins") >= ShopPanel.CharacterCost)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - ShopPanel.CharacterCost);
                PlayerPrefs.SetInt($"Open{ShopPanel.NumberForUnlock}", 1);
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
    }
}