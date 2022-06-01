using System;
using Audio;
using UnityEngine;

namespace Map.Stars
{
    public class StarController : MonoBehaviour
    {
        [SerializeField] private int _speed;
        private StarSide _valueSide;
        private AudioManager _audioManager;

        public void Start()
        {
            _audioManager=AudioManager.Instanse;
            transform.Rotate(0, 0, 90);
        }
        private void FixedUpdate()
        {
            transform.Rotate(_speed * Time.deltaTime, _speed * Time.deltaTime, _speed * Time.deltaTime);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                switch(_valueSide)
                {
                    case StarSide.Up:
                    {
                        MapManager.MainDataCollector.Level.UpStars = true;
                        _audioManager.Play("Star");

                        Destroy(gameObject);
                        break;
                    }
                    case StarSide.Down:
                    {
                        MapManager.MainDataCollector.Level.DownStars = true;
                        _audioManager.Play("Star");

                        Destroy(gameObject);
                        break;
                    }
                }
            }
        }
        public void SetValueSide(StarSide valueSide)
        {
            this._valueSide = valueSide;
        }
    }
}