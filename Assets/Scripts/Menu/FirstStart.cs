using UnityEngine;

public class FirstStart : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            PlayerPrefs.SetString("Language", "English");
        }
    }
}