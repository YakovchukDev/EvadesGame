using UnityEngine;

namespace Map
{
    public class CoinControl : MonoBehaviour
    {
        public delegate void GiveExpirience();
        public static event GiveExpirience GiveCoin;

        public bool IsUse = false;
        [SerializeField] private int _speed;
        private int _value = 1;
        private void Start()
        {
            transform.Rotate(0, 0, 90f);
        }
        public void SetQuantityAddCoins(int value)
        {
            _value = value;
        }
        private void FixedUpdate()
        {
            transform.Rotate(_speed * Time.deltaTime, 0, 0);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                GiveCoin();
                this.gameObject.SetActive(false);
                IsUse = true;
            }
        }
    }
}
