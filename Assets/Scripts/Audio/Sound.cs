using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [System.Serializable]
    public class Sound
    {
        public string _name;

        [HideInInspector] public AudioSource _source;
        public AudioClip _clip;
        public AudioMixerGroup _mixerGroup;
        [Range(0f, 1f)] public float _volume;
        [Range(0.1f, 3f)] public float _pitch;
        [Range(0, 1)] public int _spatialBlend;
        public bool _loop;
    }
}