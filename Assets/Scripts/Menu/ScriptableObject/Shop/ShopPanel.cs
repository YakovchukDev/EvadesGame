using UnityEngine;

namespace Menu.ScriptableObject.Shop
{
    [CreateAssetMenu(fileName = "New Shop", menuName = "ScriptableObject/ShopComponent")]
    public class ShopPanel : UnityEngine.ScriptableObject
    {
        [SerializeField] private Sprite _backImage;
        [SerializeField] private Sprite _backText;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _characterCost;
        [SerializeField] private int _numberForUnlock;

        public Sprite BackImage => _backImage;
        public Sprite BackText => _backText;
        public Sprite Image => _image;

        public int CharacterCost => _characterCost;

        public int NumberForUnlock => _numberForUnlock;
    }
}

