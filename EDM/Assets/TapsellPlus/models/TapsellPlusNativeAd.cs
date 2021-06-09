using System;

namespace TapsellPlusSDK
{
    [Serializable]
    public class TapsellPlusNativeAd : TapsellPlusAdModel
    {
        public TapsellNativeAd generalNativeAdModel;

        public TapsellPlusNativeAd(string responseId, string zoneId, TapsellNativeAd generalNativeAdModel) : base(responseId, zoneId)
        {
            this.generalNativeAdModel = generalNativeAdModel;
        }
    }
}