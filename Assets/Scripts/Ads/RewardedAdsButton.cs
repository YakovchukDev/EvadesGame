using System;
using GamePlay.Character;
using Map;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Ads
{
    public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private AdsPanelControl _adsPanelControl;
        [SerializeField] private GameObject _adsAfterDiePanel;
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private EntitiesGenerator _entitiesGenerator;
        [SerializeField] private Button _rewardedAdButton;
        private const string AndroidAdUnitId = "Rewarded_Android";
        private const string IOSAdUnitId = "Rewarded_iOS";
        private string _adUnitId;
        public static event Action<int> HealthPanelUpdate;

        private void Awake()
        {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = AndroidAdUnitId;
#endif
        }


        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);

            if (adUnitId.Equals(_adUnitId))
            {
                _rewardedAdButton.onClick.AddListener(ShowAd);
            }
        }

        public void ShowAd()
        {
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                Advertisement.Load(_adUnitId, this);

                HealthPanelUpdate?.Invoke(1);

                if (_characterSpawner != null)
                {
                    _characterSpawner.Character.GetComponent<HealthController>().HpNumber++;
                }


                _audioMixer.audioMixer.SetFloat("EffectVolume",
                    Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectVolume")));
                _adsPanelControl.AdsComplete = true;
                _adsAfterDiePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string adUnitId)
        {
        }

        public void OnUnityAdsShowClick(string adUnitId)
        {
        }

        private void OnDestroy()
        {
            _rewardedAdButton.onClick.RemoveAllListeners();
        }
    }
}