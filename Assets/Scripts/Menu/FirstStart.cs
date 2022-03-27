using UnityEngine;

namespace Menu
{
    public class FirstStart : MonoBehaviour
    {
        private void Awake()
        {
            if (!PlayerPrefs.HasKey("Language"))
            {
                PlayerPrefs.SetString("Language", "English");
            }
            if (!PlayerPrefs.HasKey("MasterVolume"))
            {
                PlayerPrefs.SetFloat("MasterVolume", 0);
                print(1);
            }
            if (!PlayerPrefs.HasKey("MusicVolume"))
            {
                PlayerPrefs.SetFloat("MusicVolume", 0);
            }
            if (!PlayerPrefs.HasKey("AllEffectVolume"))
            {
                PlayerPrefs.SetFloat("AllEffectVolume", 0);
            }
        }

        public void RestartButton()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}