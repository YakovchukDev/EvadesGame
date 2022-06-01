using System;
using Audio;
using UnityEngine;

namespace Map.Coins
{
    public class CoinControl : MonoBehaviour
    {
        public static event Action<int> GiveCoin;
        public static event Action SetNewCoinOnSurvive;
            //public static event Action PlaySound;
        [SerializeField] private int _speed;
        private AudioManager _audioManager;

        private int _value;

        private void Start()
        {
            _audioManager=AudioManager.Instanse;
            Initialize();
            transform.Rotate(0, 0, 90f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                GiveCoin?.Invoke(_value);
                SetNewCoinOnSurvive?.Invoke();
                //PlaySound();
                _audioManager.Play("Coin");
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
