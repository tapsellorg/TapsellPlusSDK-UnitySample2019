using UnityEngine;
using UnityEngine.UI;

public class NativeBannerScene : MonoBehaviour {
	private const string ZoneID = "5cfaa9deaede570001d5553a";
	private static string _responseId;
	[SerializeField] private RawImage adImage;
	[SerializeField] private Text adHeadline;
	[SerializeField] private Text adCallToAction;
	[SerializeField] private Text adBody;
	public void Request () {
		TapsellPlus.TapsellPlus.RequestNativeBannerAd(ZoneID,

			tapsellPlusAdModel => {
				Debug.Log ("On Response " + tapsellPlusAdModel.responseId);
				_responseId = tapsellPlusAdModel.responseId;
			},
			error => {
				Debug.Log ("Error " + error.message);
			}
		);
    }
	public void Show() {
		TapsellPlus.TapsellPlus.ShowNativeBannerAd(_responseId, this,

			tapsellPlusNativeBannerAd => {
				Debug.Log ("onOpenAd " + tapsellPlusNativeBannerAd.zoneId);
				adHeadline.text = ArabicSupport.ArabicFixer.Fix(tapsellPlusNativeBannerAd.title);
				adCallToAction.text = ArabicSupport.ArabicFixer.Fix(tapsellPlusNativeBannerAd.callToActionText);
				adBody.text = ArabicSupport.ArabicFixer.Fix(tapsellPlusNativeBannerAd.description);
				adImage.texture = tapsellPlusNativeBannerAd.landscapeBannerImage;
        
				tapsellPlusNativeBannerAd.RegisterImageGameObject(adImage.gameObject);
				tapsellPlusNativeBannerAd.RegisterHeadlineTextGameObject(adHeadline.gameObject);
				tapsellPlusNativeBannerAd.RegisterCallToActionGameObject(adCallToAction.gameObject);
				tapsellPlusNativeBannerAd.RegisterBodyTextGameObject(adBody.gameObject);
			},
			error => {
				Debug.Log ("onError " + error.errorMessage);
			}
		);
	}
}