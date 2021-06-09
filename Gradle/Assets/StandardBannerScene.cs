using TapsellPlusSDK;
using UnityEngine;

public class StandardBannerScene : MonoBehaviour {
    private const string ZoneID = "5cfaaa30e8d17f0001ffb294";
    private static string _responseId;

    public void Request () {
        TapsellPlus.RequestStandardBannerAd(ZoneID, BannerType.Banner320X50,
            
            tapsellPlusAdModel => {
                Debug.Log ("on response " + tapsellPlusAdModel.responseId);
                _responseId = tapsellPlusAdModel.responseId;
            },
            error => {
                Debug.Log ("Error " + error.message);
            }
        );
    }

    public void Show()
    {
        TapsellPlus.ShowStandardBannerAd(_responseId, Gravity.Bottom, Gravity.Center,

            tapsellPlusAdModel => {
                Debug.Log ("onOpenAd " + tapsellPlusAdModel.zoneId);
            },
            error => {
                Debug.Log ("onError " + error.errorMessage);
            }
        );
    }

    public void Hide () {
        TapsellPlus.HideStandardBannerAd();
    }

    public void Display () {
        TapsellPlus.DisplayStandardBannerAd();
    }

    public void Destroy()
    {
        TapsellPlus.DestroyStandardBannerAd(_responseId);
    }
}