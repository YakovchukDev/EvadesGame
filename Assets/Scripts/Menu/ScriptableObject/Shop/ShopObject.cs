using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.ScriptableObject.Shop
{
    public class ShopObject : MonoBehaviour
    {
        [SerializeField] private ShopPanel _shopPanel;
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
        }

        public void BuyCharacter()
        {
            _text.text = "Bought";
            _button.interactable = false;
        }
    }
}