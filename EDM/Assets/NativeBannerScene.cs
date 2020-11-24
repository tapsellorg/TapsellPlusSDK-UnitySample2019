using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using TapsellPlusSDK;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NativeBannerScene : MonoBehaviour {
	private readonly string ZONE_ID = "5d123c9968287d00019e1a94";
	// private readonly string ZONE_ID = "5cfaa9deaede570001d5553a";
	public static TapsellPlusNativeBannerAd nativeAd = null;
	
	private bool nativeLoaded = false;
	[SerializeField] RawImage adIcon;
	[SerializeField] RawImage adImage;
	[SerializeField] RawImage adChoices;
	[SerializeField] Text adHeadline;
	[SerializeField] Text adCallToAction;
	[SerializeField] Text adBody;
	[SerializeField] GameObject cube;
	

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

            adHeadline.text = nativeAd.title;
            adCallToAction.text = nativeAd.callToActionText;
            adBody.text = nativeAd.description;
            adIcon.texture = nativeAd.iconImage;
            adImage.texture = nativeAd.landscapeBannerImage;

            nativeAd.RegisterIconImageGameObject(adIcon.gameObject);
            nativeAd.RegisterImageGameObject(adImage.gameObject);
            nativeAd.RegisterHeadlineTextGameObject(adHeadline.gameObject);
            nativeAd.RegisterCallToActionGameObject(adCallToAction.gameObject);
            nativeAd.RegisterBodyTextGameObject(adCallToAction.gameObject);
            nativeAd.Register3DItem(cube);
        }
    }
}