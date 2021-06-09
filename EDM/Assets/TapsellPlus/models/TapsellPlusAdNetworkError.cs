using System;

namespace TapsellPlusSDK
{
    [Serializable]
    public class TapsellPlusAdNetworkError
    {
        public string ErrorDomain;
        public string AdNetworks;
        public string ErrorMessage;

        public override string ToString()
        {
            return "onInitializeFailed" + "ad network: " + AdNetworks + ", error: " + ErrorMessage;
        }
    }
}