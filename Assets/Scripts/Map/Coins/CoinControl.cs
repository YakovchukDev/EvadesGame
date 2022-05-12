using System;
using UnityEngine;

namespace Map.Coins
{
    public class CoinControl : MonoBehaviour
    {
        public static event Action GiveCoin;
        public static event Action Survive;
        public bool IsUse;
        [SerializeField] private int _speed;
        private int _value;

        private void Start()
        {
            IsUse = false;
            _value = 1;
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
            if(other.gameObject.CompareTag("Player"))
            {
                GiveCoin?.Invoke();
                this.gameObject.SetActive(false);
                IsUse = true;
                Survive?.Invoke();
            }
        }
    }
}
