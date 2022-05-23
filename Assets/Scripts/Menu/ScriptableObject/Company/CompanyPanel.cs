using UnityEngine;

namespace Menu.ScriptableObject.Company
{
    [CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObject/LevelComponent")]
    public class CompanyPanel : UnityEngine.ScriptableObject
    {
        [SerializeField] private Sprite _mainSprite1;
        [SerializeField] private Sprite _mainSprite2;
        [SerializeField] private Sprite _mainSprite3;
        [SerializeField] private Sprite _mainSprite4;
        [SerializeField] private Sprite _openStarSprite;
        [SerializeField] private Sprite _closeStarSprite;
        [SerializeField] private Vector3 _textTransformPosition;
        [SerializeField] private Vector3 _levelTransformPosition;

        [SerializeField] private Color _color;
        public Sprite MainSprite1 => _mainSprite1;
        public Sprite MainSprite2 => _mainSprite2;
        public Sprite MainSprite3 => _mainSprite3;
        public Sprite MainSprite4 => _mainSprite4;
        public Sprite OpenStarSprite => _openStarSprite;
        public Sprite CloseStarSprite => _closeStarSprite;

        public Vector3 TextTransformPosition => _textTransformPosition;

        public Vector3 LevelTransformPosition => _levelTransformPosition;

        public Color Color => _color;
    }
}