using UnityEngine;

namespace TapsellPlusSDK {
	/*
	 * To handle callbacks that call in Android
	 */
	public class TapsellPlusMessageHandler : MonoBehaviour {
		public void NotifyOnInitializeSuccess(string adNetworkName)
		{
			Debug.Log ("NotifyOnInitializeSuccess() Called.");
			TapsellPlus.OnInitializeSuccess(adNetworkName);
		}
		public void NotifyOnInitializeFailed(string json)
		{
			Debug.Log ("NotifyOnInitializeFailed() Called.");
			var tapsellPlusAdNetworkError = JsonUtility.FromJson<TapsellPlusAdNetworkError>(json);
			TapsellPlus.OnInitializeFailed(tapsellPlusAdNetworkError);
		}
		public void NotifyOnRequestResponse(string json) {
			Debug.Log ("NotifyOnRequestResponse() Called.");
			var tapsellPlusAdModel = JsonUtility.FromJson<TapsellPlusAdModel> (json);
			TapsellPlus.OnRequestResponse(tapsellPlusAdModel);
		}
		public void NotifyOnRequestError(string json) {
			Debug.Log ("NotifyOnRequestError() Called.");
			var tapsellPlusRequestError = JsonUtility.FromJson<TapsellPlusRequestError> (json);
			TapsellPlus.OnRequestError(tapsellPlusRequestError);
		}
		public void NotifyOnAdOpened(string json) {
			Debug.Log ("NotifyOnAdOpened() Called.");
			var tapsellPlusAdModel = JsonUtility.FromJson<TapsellPlusAdModel> (json);
			TapsellPlus.OnAdOpened(tapsellPlusAdModel);
		}
		public void NotifyOnAdClosed(string json) {
			Debug.Log ("NotifyOnAdClosed() Called.");
			var tapsellPlusAdModel = JsonUtility.FromJson<TapsellPlusAdModel> (json);
			TapsellPlus.OnAdClosed(tapsellPlusAdModel);
		}
		public void NotifyOnAdRewarded(string json) {
			Debug.Log ("NotifyOnAdRewarded() Called.");
			var tapsellPlusAdModel = JsonUtility.FromJson<TapsellPlusAdModel> (json);
			TapsellPlus.OnAdRewarded(tapsellPlusAdModel);
		}
		public void NotifyOnAdShowError(string json) {
			Debug.Log ("NotifyOnAdShowError() Called.");
			var tapsellPlusErrorModel = JsonUtility.FromJson<TapsellPlusErrorModel> (json);
			TapsellPlus.OnAdShowError(tapsellPlusErrorModel);
		}
		public void NotifyTapsellNativeAdOpened(string json)
		{
			Debug.Log ("NotifyTapsellNativeAdOpened() Called.");
			var tapsellPlusNativeAd = JsonUtility.FromJson<TapsellPlusNativeAd> (json);
			TapsellPlus.OnTapsellNativeAdOpened(tapsellPlusNativeAd);
		}
		public void NotifyAdMobNativeAdRequestResponse(string json)
		{
			Debug.Log ("NotifyAdMobNativeAdRequestResponse() Called.");
			var adMobNativeAd = JsonUtility.FromJson<AdMobNativeAd> (json);
			TapsellPlus.OnAdMobNativeAdRequest(adMobNativeAd);	
		}
	}
}