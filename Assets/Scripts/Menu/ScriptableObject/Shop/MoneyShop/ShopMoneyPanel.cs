using UnityEngine;

namespace Menu.ScriptableObject.Shop.MoneyShop
{
    [CreateAssetMenu(fileName = "New Money", menuName = "ScriptableObject/Shop/MoneyComponent")]

    public class ShopMoneyPanel:UnityEngine.ScriptableObject
    {
        [SerializeField] private Sprite _backImage;
        [SerializeField] private Sprite _backText;
        [SerializeField] private Sprite _image;

        [SerializeField] private float _moneyCost;

        [SerializeField] private int _moneyCount;

        public Sprite BackImage => _backImage;
        public Sprite BackText => _backText;
        public Sprite Image => _image;

        public float MoneyCost => _moneyCost;
        
        public int MoneyCount => _moneyCount;
    }
}