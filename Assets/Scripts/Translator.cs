using UnityEngine;
using TMPro;
public class Translator : MonoBehaviour
{
    private TMP_Text _text;
    [TextArea]
    [SerializeField] private string _englishText;
    
    [TextArea]
    [SerializeField] private string _russianText;
    
    [TextArea]
    [SerializeField] private string _ukrainianText;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        switch (PlayerPrefs.GetString("Language"))
        {
            case "English":
                _text.text = _englishText;
                break;
            case "Russian":
                _text.text = _russianText;
                break;
            case "Ukrainian":
                _text.text = _ukrainianText;
                break;
        }
    }
}

