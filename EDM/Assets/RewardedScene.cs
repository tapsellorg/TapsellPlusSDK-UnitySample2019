using UnityEngine;
using TapsellPlusSDK;

public class RewardedScene : MonoBehaviour {
	
	private const string ZoneID = "5cfaa802e8d17f0001ffb28e";
	private static string _responseId;

	public void Request () {
		TapsellPlus.RequestRewardedVideoAd (ZoneID,

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
		TapsellPlus.ShowRewardedVideoAd(_responseId,

			tapsellPlusAdModel => {
				Debug.Log ("onOpenAd " + tapsellPlusAdModel.zoneId);
			},
			tapsellPlusAdModel => {
				Debug.Log ("onReward " + tapsellPlusAdModel.zoneId);
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