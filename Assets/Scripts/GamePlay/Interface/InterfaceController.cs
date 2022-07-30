using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace GamePlay.Interface
{
    public class InterfaceController: MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private GameObject _pauseButton;

        public void OnPause()
        {
            Time.timeScale = 0;
            _pauseButton.SetActive(false);
            _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
        }

        public void OffPause()
        {
            Time.timeScale = 1;
            _pauseButton.SetActive(true);
            _audioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectVolume")));
        }
        public virtual void ExitAndSave()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Menu");
        }

        public virtual void Restart()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}