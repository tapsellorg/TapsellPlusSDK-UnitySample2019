using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapsellPlusSDK
{
    [Serializable]
    public class TapsellError
    {
        public string zoneId;
        public string message;
        public TapsellError() { }

        public TapsellError(string zoneId, string message)
        {
            this.zoneId = zoneId;
            this.message = message;
        }
    }
}
