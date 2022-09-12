using UnityEngine;

namespace Menu.Settings
{
    public class GameInit : MonoBehaviour
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
                PlayerPrefs.SetFloat("AllEffectVolume", 1);
            }

            if (!PlayerPrefs.HasKey("RightOrLeft"))
            {
                PlayerPrefs.SetString("RightOrLeft", "Right");
            }

            if (!PlayerPrefs.HasKey("Coins"))
            {
                PlayerPrefs.SetInt("Coins", 0);
            }
            if (!PlayerPrefs.HasKey("Education"))
            {
                PlayerPrefs.SetInt("Education", 0);
            }

            if (!PlayerPrefs.HasKey("EducationStarCount"))
            {
                PlayerPrefs.SetInt("EducationStarCount", 0);
            }
        }

        public void RestartButton()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}