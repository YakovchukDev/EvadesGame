using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const string AndroidGameID = "4844285";
        private const string IOSGameID = "4844284";

        [SerializeField] private bool _testMode = true;
        [SerializeField] private RewardedAdsButton _rewardedAdsButton;
        [SerializeField] private InterstitialAds _interstitialAds;

        private void Awake()
        {
            InitializeAds();
        }

        private void InitializeAds()
        {
            var gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSGameID : AndroidGameID;
            Advertisement.Initialize(gameId, _testMode, this);
        }


        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete");
            _interstitialAds.LoadAd();
            _rewardedAdsButton.LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed:{error.ToString()}-{message}");
        }
    }
}