using UnityEngine;

namespace Map
{
    public class StarController : MonoBehaviour
    {
        [SerializeField] private int _speed;
        private string _valueSide; 
        
        void Start()
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
                if(_valueSide == "left")
                {
                    GeneralParameters.MainDataCollector.Level.LeftStars = true;
                }
                else if(_valueSide == "right")
                {
                    GeneralParameters.MainDataCollector.Level.RightStars = true;
                }
                Destroy(gameObject);
            }
        }
        public void SetValueSide(string valueSide)
        {
            this._valueSide = valueSide;
        }
    }
}