using TapsellPlusSDK;
using UnityEngine;
using UnityEngine.UI;

public class NativeBannerScene : MonoBehaviour {
	private readonly string ZONE_ID = "5cfaa9deaede570001d5553a";
	public static TapsellPlusNativeBannerAd nativeAd = null;
	
	private bool nativeLoaded = false;
	[SerializeField] RawImage adImage;
	[SerializeField] Text adHeadline;
	[SerializeField] Text adCallToAction;
	[SerializeField] Text adBody;
	

	public void Request () {

		TapsellPlus.requestNativeBanner(this, ZONE_ID,

            (TapsellPlusNativeBannerAd result) =>
            {
                Debug.Log("on response");
                nativeLoaded = true;
                nativeAd = result;
            },

            (TapsellError error) =>
            {
                Debug.Log("Error " + error.message);
            }
        );
    }

	void OnGUI () {

        if (nativeAd != null && nativeLoaded)
        {

            nativeLoaded = false;

            adHeadline.text = ArabicSupport.ArabicFixer.Fix(nativeAd.title);
            adCallToAction.text = ArabicSupport.ArabicFixer.Fix(nativeAd.callToActionText);
            adBody.text = ArabicSupport.ArabicFixer.Fix(nativeAd.description);
            adImage.texture = nativeAd.landscapeBannerImage;

            nativeAd.RegisterImageGameObject(adImage.gameObject);
            nativeAd.RegisterHeadlineTextGameObject(adHeadline.gameObject);
            nativeAd.RegisterCallToActionGameObject(adCallToAction.gameObject);
            nativeAd.RegisterBodyTextGameObject(adBody.gameObject);
        }
    }
}