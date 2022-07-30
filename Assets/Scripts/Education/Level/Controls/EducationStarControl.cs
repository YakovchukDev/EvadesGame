using System;
using Audio;
using UnityEngine;

namespace Education.Level
{
    public class EducationStarControl : MonoBehaviour
    {
        private AudioManager _audioManager;
        public static event Action<Vector3> OnGetStar;
        public void Start()
        {
            _audioManager = AudioManager.Instanse;
            transform.Rotate(0, 0, 90);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _audioManager.Play("Star");
                OnGetStar?.Invoke(transform.position);
                Destroy(gameObject);
            }
        }
    }
}