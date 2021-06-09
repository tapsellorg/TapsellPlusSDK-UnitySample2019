namespace TapsellPlusSDK
{
    public class TapsellPlusPlugin
    {
        public virtual void Initialize(string key)
        {
        }

        public virtual void SetDebugMode(int logLevel)
        {
        }

        public virtual void SetGdprConsent(bool consent)
        {
        }

        public virtual void RequestRewardedVideoAd(string responseId)
        {
        }

        public virtual void ShowRewardedVideoAd(string responseId)
        {
        }

        public virtual void RequestInterstitialAd(string zoneId)
        {
        }

        public virtual void ShowInterstitialAd(string responseId)
        {
        }

        public virtual void RequestStandardBannerAd(string responseId, int bannerSize)
        {
        }

        public virtual void ShowStandardBannerAd(string zoneId, int horizontalGravity, int verticalGravity)
        {
        }

        public virtual void DestroyStandardBannerAd(string responseId)
        {
        }

        public virtual void DisplayStandardBannerAd()
        {
        }

        public virtual void HideStandardBannerAd()
        {
        }

        public virtual void RequestNativeBannerAd(string zoneId)
        {
        }

        public virtual void ShowNativeBannerAd(string responseId)
        {
        }

        public virtual void NativeBannerAdClicked(string responseId)
        {
        }

        public virtual void SendAdMobNativeAdSuccessReport(string responseId, string adNetworkZoneId)
        {
        }

        public virtual void SendAdMobNativeAdWin(string responseId, string adNetworkZoneId)
        {
        }

        public virtual void SendAdMobNativeAdShowStart(string responseId, string adNetworkZoneId)
        {
        }

        public virtual void SendAdMobNativeAdFailedReport(string zoneId, string responseId, string json)
        {
        }
    }
}