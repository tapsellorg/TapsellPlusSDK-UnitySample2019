using System;

namespace TapsellPlusSDK
{
    [Serializable]
    public class TapsellPlusErrorModel : TapsellPlusAdModel
    {
        public string errorMessage;

        public TapsellPlusErrorModel(string responseId, string zoneId, string errorMessage) : base(responseId, zoneId)
        {
            this.errorMessage = errorMessage;
        }
    }
}
