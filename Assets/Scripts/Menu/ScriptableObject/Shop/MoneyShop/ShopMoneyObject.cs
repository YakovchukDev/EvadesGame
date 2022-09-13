using Audio;
using IAP;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
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
            /*_backImage.sprite = ShopMoneyPanel.BackImage;
            _textMoneyCount.text = ShopMoneyPanel.MoneyCount.ToString();
            _button.image.sprite = ShopMoneyPanel.BackText;
            _image.sprite = ShopMoneyPanel.Image;
            _text.text = ShopMoneyPanel.MoneyCost + "$";*/
            _audioManager = AudioManager.Instanse;
        }

        public void BuyMoney()
        {
            _audioManager.Play("PressButton");
        }
    }
}