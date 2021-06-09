using UnityEngine;
using TapsellPlusSDK;

public class InterstitialScene : MonoBehaviour {
	private const string ZoneID = "5cfaa942e8d17f0001ffb292";
	private static string _responseId;
	
	public void Request () {
		TapsellPlus.RequestInterstitialAd(ZoneID,

			tapsellPlusAdModel => {
				Debug.Log ("on response " + tapsellPlusAdModel.responseId);
				_responseId = tapsellPlusAdModel.responseId;
			},
			error => {
				Debug.Log ("Error " + error.message);
			}
		);
	}
	public void Show () {
		TapsellPlus.ShowInterstitialAd(_responseId,

			tapsellPlusAdModel => {
				Debug.Log ("onOpenAd " + tapsellPlusAdModel.zoneId);
			},
			tapsellPlusAdModel => {
				Debug.Log ("onCloseAd " + tapsellPlusAdModel.zoneId);
			},
			error => {
				Debug.Log ("onError " + error.errorMessage);
			}
		);
	}
}