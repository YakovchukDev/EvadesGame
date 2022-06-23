using System;
using System.Collections;
using Audio;
using UnityEngine;

namespace Map.Coins
{
    public class CoinControl : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        public static event Action<int> GiveCoin;
        public static event Action SetNewCoinOnSurvive;
        [SerializeField] private int _speed;
        private AudioManager _audioManager;
        private Material _material;
        private int _value;
        float _dissolveAmountValue;

        private void Start()
        {
            _audioManager = AudioManager.Instanse;
            Initialize();
            _material = GetComponent<Renderer>().material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                GiveCoin?.Invoke(_value);
                SetNewCoinOnSurvive?.Invoke();
                _audioManager.Play("Coin");
                StartCoroutine(TakeCoin());
            }
        }

        private IEnumerator TakeCoin()
        {
            _collider.enabled = false;
            while (_material.GetFloat("_DissolveAmount") <= 1)
            {
                _dissolveAmountValue +=0.03f;
                _material.SetFloat("_DissolveAmount", _dissolveAmountValue);

                yield return new WaitForEndOfFrame();
            }
            Destroy(gameObject);
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
            transform.Rotate(0, _speed * Time.deltaTime, 0);
        }
    }
}