using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu.Settings
{
    public class SetLanguage : MonoBehaviour
    {
        [SerializeField] private GameObject _settings;
        [SerializeField] private Animator _settingsAnimator;
        
        private void Start()
        {
            if (PlayerPrefs.GetInt("newLanguage")==1)
            {
                _settings.SetActive(true);
                _settingsAnimator.SetInteger("Settings", 2);
                PlayerPrefs.SetInt("newLanguage",0);
            }
            
        }
        public void Set(string language)
        {
           PlayerPrefs.SetInt("newLanguage",1);
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
}
