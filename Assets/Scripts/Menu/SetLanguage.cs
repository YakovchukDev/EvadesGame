using UnityEngine;
using UnityEngine.SceneManagement;

public class SetLanguage : MonoBehaviour
{
    public void Set(string language)
    {
        switch (language)
        {
            case "English":
                PlayerPrefs.SetString("Language", "English");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Russian":
                PlayerPrefs.SetString("Language", "Russian");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Ukrainian":
                PlayerPrefs.SetString("Language", "Ukrainian");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }
}
