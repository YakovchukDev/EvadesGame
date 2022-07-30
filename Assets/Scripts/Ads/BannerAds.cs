using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class BannerAds : MonoBehaviour
    {
        [SerializeField] private BannerPosition _bannerPosition;

        private const string AndroidID = "Banner_Android";
        private const string IOSId = "Banner_iOS";

        private string _adId;

        private void Awake()
        {
            _adId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSId : AndroidID;
        }

        private void Start()
        {
            Advertisement.Banner.SetPosition(_bannerPosition);
            StartCoroutine(LoadAdBanner());
        }

        private IEnumerator LoadAdBanner()
        {
            yield return new WaitForSeconds(1f);
            LoadBanner();
        }

        public void LoadBanner()
        {
            BannerLoadOptions options = new BannerLoadOptions
            {
                loadCallback = OnBannerLoaded,
                errorCallback = OnBannerError
            };

            Advertisement.Banner.Load(_adId, options);
        }

        private void OnBannerLoaded()
        {
            Debug.Log("Banner loaded");
            ShowBannerAd();
        }

        private void OnBannerError(string message)
        {
            Debug.Log($"Banner Error: {message}");
        }

        public void ShowBannerAd()
        {
            BannerOptions options = new BannerOptions
            {
                clickCallback = OnBannerClicked,
                hideCallback = OnBannerHidden,
                showCallback = OnBannerShown
            };
            Advertisement.Banner.Show(_adId, options);
        }

        private void OnBannerClicked()
        {
        }

        private void OnBannerShown()
        {
        }

        private void OnBannerHidden()
        {
        }
    }
}