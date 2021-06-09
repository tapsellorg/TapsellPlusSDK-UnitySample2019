using System;

namespace TapsellPlusSDK
{
    [Serializable]
    public class AdNetworkShowErrorModel
    {
        public string adNetworkZoneId;
        public string adNetworkEnum;
        public string errorMessage;

        public AdNetworkShowErrorModel(string adNetworkZoneId, string adNetworkEnum, string errorMessage)
        {
            this.adNetworkZoneId = adNetworkZoneId;
            this.adNetworkEnum = adNetworkEnum;
            this.errorMessage = errorMessage;
        }
    }
}