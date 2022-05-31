using System;
using UnityEngine;

namespace Map.Stars
{
    public class StarController : MonoBehaviour
    {
        [SerializeField] private int _speed;
        private StarSide _valueSide;
        public static event Action PlaySound;
        
        public void Start()
        {
            transform.Rotate(0, 0, 90);
        }
        private void FixedUpdate()
        {
            transform.Rotate(_speed * Time.deltaTime, 0, 0);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                switch(_valueSide)
                {
                    case StarSide.Up:
                    {
                        MapManager.MainDataCollector.Level.UpStars = true;
                        PlaySound();
                        Destroy(gameObject);
                        break;
                    }
                    case StarSide.Down:
                    {
                        MapManager.MainDataCollector.Level.DownStars = true;
                        PlaySound();
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