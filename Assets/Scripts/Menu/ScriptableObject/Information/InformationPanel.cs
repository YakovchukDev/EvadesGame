using UnityEngine;

namespace Menu.ScriptableObject.Information
{
    [CreateAssetMenu(fileName = "New Information", menuName = "ScriptableObject/InformationComponent")]
    public class InformationPanel : UnityEngine.ScriptableObject
    {
        [SerializeField] private Sprite _backImage;
        [SerializeField] private Sprite _backText;
        [SerializeField] private Sprite _image;
        
        [TextArea] [SerializeField] private string _englishText;

        [TextArea] [SerializeField] private string _russianText;

        [TextArea] [SerializeField] private string _ukrainianText;
        [SerializeField] private float _transformScaleX;
    
        public Sprite BackImage => _backImage;
        public Sprite BackText => _backText;
        public Sprite Image => _image;
        public string EnglishText => _englishText;

        public string RussianText => _russianText;

        public string UkrainianText => _ukrainianText;

        public float TransformScaleX => _transformScaleX;
    }
}