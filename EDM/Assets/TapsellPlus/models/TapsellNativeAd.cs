using System;

namespace TapsellPlusSDK
{
    [Serializable]
    public class TapsellNativeAd
    {
        public string adId;
        public string callToActionText;
        public string description;
        public string iconUrl;
        public string landscapeStaticImageUrl;
        public string portraitStaticImageUrl;
        public string title;
        public string adNetwork;
        public string adNetworkZoneId;
    }
}