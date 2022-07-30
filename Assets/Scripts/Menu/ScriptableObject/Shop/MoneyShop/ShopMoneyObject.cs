using Audio;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ScriptableObject.Shop.MoneyShop
{
    public class ShopMoneyObject : MonoBehaviour
    {
        private AudioManager _audioManager;
        [SerializeField] private Image _backImage;
        [SerializeField] private TMP_Text _textMoneyCount;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        public ShopMoneyPanel ShopMoneyPanel { get; set; }

        private void Start()
        {
            _backImage.sprite = ShopMoneyPanel.BackImage;
            _textMoneyCount.text = ShopMoneyPanel.MoneyCount.ToString();
            _button.image.sprite = ShopMoneyPanel.BackText;
            _image.sprite = ShopMoneyPanel.Image;
            _text.text = ShopMoneyPanel.MoneyCost + "$";
            _audioManager = AudioManager.Instanse;
            /*if (PlayerPrefs.GetInt($"OpenMoney{ShopPanel.NumberForUnlock}") == 1)
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
            }*/
        }

        public void BuyCharacter()
        {
            _audioManager.Play("PressButton");
            Debug.LogError("Have not mechanic");
            /*if (PlayerPrefs.GetInt("Coins") >= ShopMoneyPanel.CharacterCost)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - ShopMoneyPanel.CharacterCost);
                PlayerPrefs.SetInt($"Open{ShopMoneyPanel.NumberForUnlock}", 1);
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

                PlayerPrefs.SetInt("CompanyOpened", 1);
                _button.interactable = false;
            }*/
        }
    }
}