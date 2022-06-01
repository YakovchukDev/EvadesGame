using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Map
{
    public class InterfaceController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _pausePanel;

        public void OnPause()
        {
            Time.timeScale = 0;
            _pauseButton.SetActive(false);
            _pausePanel.SetActive(true);
            _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
            _audioMixer.audioMixer.SetFloat("ImportantVolume", -80);
        }

        public void OffPause()
        {
            Time.timeScale = 1;
            _pauseButton.SetActive(true);
            _pausePanel.SetActive(false);
            _audioMixer.audioMixer.SetFloat("ImportantVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("ImportantVolume")));
            _audioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectVolume")));
        }

        public void ExitAndSave()
        {
            Time.timeScale = 1;
            MapManager.MainDataCollector.SaveCoins();
            SceneManager.LoadScene(0);
        }

        public void FinishedExit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}