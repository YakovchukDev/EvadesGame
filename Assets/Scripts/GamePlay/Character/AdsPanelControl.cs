using System.Collections;
using Menu.Settings;
using TMPro;
using UnityEngine;

namespace GamePlay.Character
{
    public class AdsPanelControl : MonoBehaviour
    {
        public bool AdsComplete { get; set; }

        [SerializeField] private SelectUIPosition _selectUIPosition;
        [SerializeField] private GameObject _adsAfterDiePanel;
        [SerializeField] private TMP_Text _textTimer;
        private Coroutine _afterDie;
        private int _adsNumber;
        private float _timer = 5;

        private void Start()
        {
            HealthController.OnZeroHp += DieOpened;
        }

        private void DieOpened()
        {
            _afterDie = StartCoroutine(AfterDie());
        }

        private IEnumerator AfterDie()
        {
            if (_adsNumber == 0)
            {
                _adsAfterDiePanel.SetActive(true);
                while (_timer > 0)
                {
                    _textTimer.text = Mathf.Round(_timer).ToString();
                    yield return new WaitForSecondsRealtime(1);
                    _timer--;
                }

                _timer = 5;
                _adsNumber++;
                if (!AdsComplete)
                {
                    print(1);
                    _adsAfterDiePanel.SetActive(false);
                    _selectUIPosition.OpenDiePanel();
                }
                else
                {
                    AdsComplete = false;
                }
            }
            else
            {
                _selectUIPosition.OpenDiePanel();
            }
        }

        public void EndTheWait()
        {
            StopCoroutine(_afterDie);
            _adsAfterDiePanel.SetActive(false);
            _selectUIPosition.OpenDiePanel();
        }

        private void OnDestroy()
        {
            HealthController.OnZeroHp -= DieOpened;
        }
    }
}