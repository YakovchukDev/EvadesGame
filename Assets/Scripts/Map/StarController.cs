using UnityEngine;
using Map.Data;

namespace Map
{
    public class StarController : MonoBehaviour
    {
        [SerializeField] private int _speed;
        private StarSide _valueSide;
        
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
                        Destroy(gameObject);
                        break;
                    }
                    case StarSide.Down:
                    {
                        MapManager.MainDataCollector.Level.DownStars = true;
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