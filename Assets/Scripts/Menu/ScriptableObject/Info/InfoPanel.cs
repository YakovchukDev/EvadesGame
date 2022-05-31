using UnityEngine;

namespace Menu.ScriptableObject.Info
{
    [CreateAssetMenu(fileName = "New Info", menuName = "ScriptableObject/InfoComponent")]
    public class InfoPanel : UnityEngine.ScriptableObject
    {
        [SerializeField] private Sprite _picture;
        [SerializeField] private Sprite _socialNetworkSprite1;
        [SerializeField] private Sprite _socialNetworkSprite2;
        [SerializeField] private Color _socialNetworkColor1;
        [SerializeField] private Color _socialNetworkColor2;
        [SerializeField] private string _url1;
        [SerializeField] private string _url2;

        [TextArea] [SerializeField] private string _englishText1;
        [TextArea] [SerializeField] private string _russianText1;
        [TextArea] [SerializeField] private string _ukrainianText1;

        [TextArea] [SerializeField] private string _englishText2;
        [TextArea] [SerializeField] private string _russianText2;
        [TextArea] [SerializeField] private string _ukrainianText2;

        public Sprite Picture => _picture;
        public Sprite SocialNetworkSprite1 => _socialNetworkSprite1;
        public Sprite SocialNetworkSprite2 => _socialNetworkSprite2;
        public Color SocialNetworkColor1 => _socialNetworkColor1;
        public Color SocialNetworkColor2 => _socialNetworkColor2;
        public string URL1 => _url1;
        public string URL2 => _url2;

        public string EnglishText1 => _englishText1;
        public string RussianText1 => _russianText1;
        public string UkrainianText1 => _ukrainianText1;

        public string EnglishText2 => _englishText2;
        public string RussianText2 => _russianText2;
        public string UkrainianText2 => _ukrainianText2;
    }
}