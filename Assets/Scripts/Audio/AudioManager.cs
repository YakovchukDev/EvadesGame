using System;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] _sounds;
        public static AudioManager Instanse;

        private void Awake()
        {
            if (Instanse == null)
            {
                Instanse = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            foreach (Sound sound in _sounds)
            {
                sound._source = gameObject.AddComponent<AudioSource>();
                sound._source.clip = sound._clip;
                sound._source.outputAudioMixerGroup = sound._mixerGroup;
                sound._source.volume = sound._volume;
                sound._source.pitch = sound._pitch;
                sound._source.spatialBlend = sound._spatialBlend;
                sound._source.loop = sound._loop;
            }
        }

        private void Start()
        {
            Play("MenuMusic");
        }

        public void Play(string name)
        {
            if (PlayerPrefs.GetInt("Sound", 0) != 0)
                return;
            Sound sound = Array.Find(_sounds, sound => sound._name == name);
            if (sound == null)
            {
                Debug.LogWarning("Wrong name of the sound");
                return;
            }

            sound._source.Play();
        }

        public void Stop(string name)
        {
            Sound sound = Array.Find(_sounds, sound => sound._name == name);
            if (sound == null)
            {
                Debug.LogWarning("Wrong name of the sound");
                return;
            }

            sound._source.Stop();
        }

        public bool IsPlaying(string name)
        {
            Sound sound = Array.Find(_sounds, sound => sound._name == name);
            if (sound == null)
            {
                Debug.LogWarning("Wrong name of the sound");
                return false;
            }
            return sound._source.isPlaying;
        }
    }
}