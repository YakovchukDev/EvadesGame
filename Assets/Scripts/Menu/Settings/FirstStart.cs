using UnityEngine;

namespace Menu.Settings
{
    public class FirstStart : MonoBehaviour
    {
        private void Awake()
        {
            if (!PlayerPrefs.HasKey("newLanguage"))
            {
                PlayerPrefs.SetInt("newLanguage", 0);
            }

            if (!PlayerPrefs.HasKey("SelectionNumber"))
            {
                PlayerPrefs.SetInt("SelectionNumber", 0);
            }
            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetString("Language", "English");
            }
            
            if (!PlayerPrefs.HasKey("MasterVolume"))
            {
                PlayerPrefs.SetFloat("MasterVolume", 0);
            }
            if (!PlayerPrefs.HasKey("MusicVolume"))
            {
                PlayerPrefs.SetFloat("MusicVolume", 1);
            }
            if (!PlayerPrefs.HasKey("AllEffectVolume"))
            {
                PlayerPrefs.SetFloat("AllEffectVolume", 0);
            }

            if (!PlayerPrefs.HasKey("RightOrLeft"))
            {
                PlayerPrefs.SetString("RightOrLeft","Left");
            }
        }

        public void RestartButton()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}