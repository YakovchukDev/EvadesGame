using System;
using Audio;
using UnityEngine;

namespace Map.Stars
{
    public class StarController : MonoBehaviour
    {
        private StarSide _valueSide;
        private AudioManager _audioManager;
        public static event Action<Vector3> OnParticleAfterStar;
        public static event Action OnUpdateStarAmount;
        public void Start()
        {
            _audioManager=AudioManager.Instanse;
            transform.Rotate(0, 0, 90);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                switch (_valueSide)
                {
                    case StarSide.Up:
                    {
                        MapManager.MainDataCollector.Level.UpStar = true;
                        _audioManager.Play("Star");

                        Destroy(gameObject);
                        break;
                    }
                    case StarSide.Down:
                    {
                        MapManager.MainDataCollector.Level.DownStar = true;
                        _audioManager.Play("Star");

                        Destroy(gameObject);
                        break;
                    }
                    case StarSide.Middle:
                    {
                        MapManager.MainDataCollector.Level.MiddleStar = true;
                        _audioManager.Play("Star");

                        Destroy(gameObject);
                        break;
                    }
                }
                OnParticleAfterStar?.Invoke(transform.position);
                OnUpdateStarAmount?.Invoke();
            }
        }
        public void SetValueSide(StarSide valueSide)
        {
            this._valueSide = valueSide;
        }
    }
}