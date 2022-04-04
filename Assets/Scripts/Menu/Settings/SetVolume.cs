using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menu.Settings
{
    public class SetVolume : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private Toggle _soundsToggle;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _effectSlider;

        private void Start()
        {
            _audioMixer.audioMixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            _soundsToggle.isOn = PlayerPrefs.GetFloat("MasterVolume") == 0;

            ChangeMusicsVolume(PlayerPrefs.GetFloat("MusicVolume"));
            _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

            ChangeEffectsVolume(PlayerPrefs.GetFloat("AllEffectVolume"));
            _effectSlider.value = PlayerPrefs.GetFloat("AllEffectVolume");

            if (!PlayerPrefs.HasKey("MasterVolume"))
            {
                PlayerPrefs.SetFloat("MasterVolume", 0);
                _audioMixer.audioMixer.SetFloat("MasterVolume", 0);
            }

            if (!PlayerPrefs.HasKey("MusicVolume"))
            {
                PlayerPrefs.SetFloat("MusicVolume", 0);
                _audioMixer.audioMixer.SetFloat("MusicVolume", 0);
            }

            if (!PlayerPrefs.HasKey("ImportantVolume"))
            {
                PlayerPrefs.SetFloat("ImportantVolume", 0);
                _audioMixer.audioMixer.SetFloat("ImportantVolume", 0);
            }

            if (!PlayerPrefs.HasKey("EffectVolume"))
            {
                PlayerPrefs.SetFloat("EffectVolume", 0);
                _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
            }
        }

        public void ToggleSounds(bool enable)
        {
            if (enable)
            {
                _audioMixer.audioMixer.SetFloat("MasterVolume", 0);
                PlayerPrefs.SetFloat("MasterVolume", 0);
            }
            else
            {
                _audioMixer.audioMixer.SetFloat("MasterVolume", -80);
                PlayerPrefs.SetFloat("MasterVolume", -80);
            }
        }

        public void ChangeMusicsVolume(float volume)
        {
            _audioMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
            PlayerPrefs.SetFloat("MusicVolume", volume);
        }

        public void ChangeEffectsVolume(float volume)
        {
            _audioMixer.audioMixer.SetFloat("ImportantVolume", Mathf.Lerp(-80, 0, volume));
            _audioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, volume));
            PlayerPrefs.SetFloat("ImportantVolume", volume);
            PlayerPrefs.SetFloat("EffectVolume", volume);
            PlayerPrefs.SetFloat("AllEffectVolume", volume);
        }
    }
}