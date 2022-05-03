using System;
using UnityEngine;

namespace MapGeneration.Coins
{
    public class CoinControl : MonoBehaviour
    {
        public static event Action GiveCoin;
        public static event Action Survive;

        public bool _isUse;
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
            if(other.gameObject.CompareTag("Player"))
            {
                GiveCoin?.Invoke();
                gameObject.SetActive(false);
                _isUse = true;
                Survive?.Invoke();
            }
        }
    }
}
