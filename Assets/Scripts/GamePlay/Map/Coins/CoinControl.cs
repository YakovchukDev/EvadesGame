using System;
using System.Collections;
using Audio;
using UnityEngine;

namespace GamePlay.Map.Coins
{
    public class CoinControl : MonoBehaviour
    {
        private BoxCollider _collider;
        private Material _material;
        private float _dissolveAmountValue;
        private AudioManager _audioManager;
        private int _value = 1;
        public static event Action<int> OnGiveCoin;
        public static event Action SetNewCoinOnSurvive;

        private void Start()
        {
            _audioManager = AudioManager.Instanse;
            Initialize();
            _collider = GetComponent<BoxCollider>();
            _material = GetComponent<Renderer>().material;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                OnGiveCoin?.Invoke(_value);
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
                _dissolveAmountValue += 0.03f;
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
    }
}