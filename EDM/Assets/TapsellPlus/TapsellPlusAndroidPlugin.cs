using UnityEngine;

namespace TapsellPlusSDK
{
    public class TapsellPlusAndroidPlugin : TapsellPlusPlugin
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaClass tapsellPlus;

        private void setJavaObject()
        {
            tapsellPlus = new AndroidJavaClass("ir.tapsell.plus.TapsellPlus");
        }

        private void initializeSDK(string key)
        {
            tapsellPlus?.CallStatic("initialize", key);
        }

        public override void Initialize(string key)
        {
            setJavaObject();
            initializeSDK(key);
        }

        public override void SetDebugMode(int logLevel)
        {
            tapsellPlus?.CallStatic("setDebugMode", logLevel);
        }

        public override void SetGdprConsent(bool consent)
        {
            tapsellPlus?.CallStatic("setGDPRConsent", consent);
        }

        public override void RequestRewardedVideoAd(string zoneId)
        {
            tapsellPlus?.CallStatic("requestRewardedVideoAd", zoneId);
        }

        public override void ShowRewardedVideoAd(string responseId)
        {
            tapsellPlus?.CallStatic("showRewardedVideoAd", responseId);
        }

        public override void RequestInterstitialAd(string zoneId)
        {
            tapsellPlus?.CallStatic("requestInterstitialAd", zoneId);
        }

        public override void ShowInterstitialAd(string responseId)
        {
            tapsellPlus?.CallStatic("showInterstitialAd", responseId);
        }
        
        public override void RequestStandardBannerAd(string zoneId, int bannerSize)
        {
            tapsellPlus?.CallStatic("requestStandardBannerAd", zoneId, bannerSize);
        }
        public override void ShowStandardBannerAd(string responseId, int horizontalGravity, int verticalGravity)
        {
            tapsellPlus?.CallStatic("showStandardBannerAd", responseId, horizontalGravity, verticalGravity);
        }
        public override void DestroyStandardBannerAd(string responseId)
        {
            tapsellPlus?.CallStatic("destroyStandardBannerAd", responseId);
        }
        public override void DisplayStandardBannerAd()
        {
            tapsellPlus?.CallStatic("displayStandardBannerAd");
        }
        public override void HideStandardBannerAd()
        {
            tapsellPlus?.CallStatic("hideStandardBannerAd");
        }
        public override void RequestNativeBannerAd(string zoneId)
        {
            tapsellPlus?.CallStatic("requestNativeBannerAd", zoneId);
        }
        public override void ShowNativeBannerAd(string zoneId)
        {
            tapsellPlus?.CallStatic("showNativeBannerAd", zoneId);
        }
        public override void NativeBannerAdClicked(string responseId)
        {
            tapsellPlus?.CallStatic("nativeBannerAdClicked", responseId);
        }
        public override void SendAdMobNativeAdSuccessReport(string responseId, string adNetworkZoneId)
        {
            tapsellPlus?.CallStatic("sendAdMobNativeAdSuccessReport", responseId, adNetworkZoneId);
        }
        public override void SendAdMobNativeAdWin(string responseId, string adNetworkZoneId)
        {
            tapsellPlus?.CallStatic("sendAdMobNativeAdWin", responseId, adNetworkZoneId);
        }
        public override void SendAdMobNativeAdShowStart(string responseId, string adNetworkZoneId)
        {
            tapsellPlus?.CallStatic("sendAdMobNativeAdShowStart", responseId, adNetworkZoneId);
        }
        public override void SendAdMobNativeAdFailedReport(string zoneId, string responseId, string json)
        {
            tapsellPlus?.CallStatic("sendAdMobNativeAdFailedReport", zoneId, responseId, json);
        }
#endif
    }
}