using System;
using UnityEngine;

namespace Map.Coins
{
    public class CoinControl : MonoBehaviour
    {
        public static event Action<int> GiveCoin;
        public static event Action SetNewCoinOnSurvive;
        public static event Action PlaySound;
        [SerializeField] private int _speed;
        private int _value;

        private void Start()
        {
            Initialize();
            transform.Rotate(0, 0, 90f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                GiveCoin?.Invoke(_value);
                SetNewCoinOnSurvive?.Invoke();
                PlaySound();
                Destroy(this.gameObject);
            }
        }
        public void SetQuantityAddCoins(int value)
        {
            _value = value;
        }
        public void Initialize()
        {
            this.gameObject.SetActive(true);
        }
        private void FixedUpdate()
        {
            transform.Rotate(_speed * Time.deltaTime, 0, 0);
        }
    }
}
