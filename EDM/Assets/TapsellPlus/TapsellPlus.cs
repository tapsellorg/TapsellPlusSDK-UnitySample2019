using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Networking;

namespace TapsellPlusSDK
{
    public class TapsellPlus
    {
        private static readonly Dictionary<string, Action<string>> requestResponsePool = new Dictionary<string, Action<string>>();
        private static readonly Dictionary<string, Action<TapsellPlusNativeBannerAd>> nativeBannerResponsePool = new Dictionary<string, Action<TapsellPlusNativeBannerAd>>();
        private static readonly Dictionary<string, Action<TapsellError>> requestErrorPool = new Dictionary<string, Action<TapsellError>>();

        private static readonly Dictionary<string, Action<string>> openAdPool = new Dictionary<string, Action<string>>();
        private static readonly Dictionary<string, Action<string>> closeAdPool = new Dictionary<string, Action<string>>();
        private static readonly Dictionary<string, Action<string>> rewardPool = new Dictionary<string, Action<string>>();
        private static readonly Dictionary<string, Action<TapsellError>> errorPool = new Dictionary<string, Action<TapsellError>>();

        private static MonoBehaviour mMonoBehaviour = null;
        private static GameObject tapsellPlusManager = null;
        private static TapsellPlusPlugin plugin = null;

        private static string zoneId;
        private static string adNetworkZoneId;

        // private static UnifiedNativeAd unifiedNativeAd;

        public static void initialize(string key)
        {
            if (tapsellPlusManager != null) return;
            tapsellPlusManager = new GameObject("TapsellPlusManager");
            UnityEngine.Object.DontDestroyOnLoad(tapsellPlusManager);
            tapsellPlusManager.AddComponent<TapsellPlusMessageHandler>();

            plugin = new TapsellPlusPlugin();
#if UNITY_ANDROID && !UNITY_EDITOR
				plugin = new TapsellPlusAndroidPlugin ();
#endif

#if UNITY_IOS && !UNITY_EDITOR
				plugin = new TapsellPlusIOSPlugin ();
#endif

            plugin.initialize(key);
        }

        internal static GameObject getTapsellPlusManager()
        {
            // Used in iOS Plugin
            return tapsellPlusManager;
        }

        private static void AddToPool<T>(IDictionary<string, Action<T>> pool, string zoneId, Action<T> item)
        {
            if (pool.ContainsKey(zoneId))
            {
                pool.Remove(zoneId);
            }
            pool.Add(zoneId, item);
        }

        private static void CallIfAvailable<T>(IDictionary<string, Action<T>> pool, string zoneId, T input)
        {
            if (pool != null && pool.ContainsKey(zoneId))
            {
                pool[zoneId](input);
            }
        }

        public static void addFacebookTestDevice(string hash)
        {
            plugin.addFacebookTestDevice(hash);
        }

        public static void setDebugMode(int logLevel)
        {
            plugin.setDebugMode(logLevel);
        }

        public static void setGDPRConsent(bool consent)
        {
            plugin.setGDPRConsent(consent);
        }

        public static void requestRewardedVideo(
            string zoneId, Action<string> onRequestResponse, Action<TapsellError> onRequestError)
        {
            AddToPool(requestResponsePool, zoneId, onRequestResponse);
            AddToPool(requestErrorPool, zoneId, onRequestError);

            plugin.requestRewardedVideo(zoneId);
        }

        public static void requestInterstitial(
            string zoneId, Action<string> onRequestResponse, Action<TapsellError> onRequestError)
        {
            AddToPool(requestResponsePool, zoneId, onRequestResponse);
            AddToPool(requestErrorPool, zoneId, onRequestError);

            plugin.requestInterstitial(zoneId);
        }

        public static void showAd(
            string zoneId, Action<string> onShowAd, Action<string> onCloseAd,
            Action<string> onReward, Action<TapsellError> onError)
        {

            AddToPool(openAdPool, zoneId, onShowAd);
            AddToPool(closeAdPool, zoneId, onCloseAd);
            AddToPool(rewardPool, zoneId, onReward);
            AddToPool(errorPool, zoneId, onError);

            plugin.showAd(zoneId);
        }

        public static void showBannerAd(
            string zoneId, int bannerType, int horizontalGravity, int verticalGravity,
            Action<string> onRequestResponse, Action<TapsellError> onRequestError)
        {

            AddToPool(requestResponsePool, zoneId, onRequestResponse);
            AddToPool(requestErrorPool, zoneId, onRequestError);

            plugin.showBannerAd(zoneId, bannerType, horizontalGravity, verticalGravity);
        }

        public static void hideBanner()
        {
            plugin.hideBanner();
        }
        public static void displayBanner()
        {
            plugin.displayBanner();
        }

        public static void requestNativeBanner(
            MonoBehaviour monoBehaviour, string zoneId, Action<TapsellPlusNativeBannerAd> onRequestResponse, Action<TapsellError> onRequestError)
        {
            Debug.Log("added to pool");
            AddToPool(nativeBannerResponsePool, zoneId, onRequestResponse);
            AddToPool(requestErrorPool, zoneId, onRequestError);
            mMonoBehaviour = monoBehaviour;

            plugin.requestNativeBanner(zoneId);
        }

        internal static void nativeBannerAdClicked(string zoneId, string adId)
        {
            plugin.nativeBannerAdClicked(zoneId, adId);
        }

        internal static void onNativeRequestResponse(TapsellPlusNativeBannerAd result)
        {
            if (result != null)
            {
                if (mMonoBehaviour != null && mMonoBehaviour.isActiveAndEnabled)
                {

                    if (result.adNetwork.Equals("TAPSELL"))
                    {

                        mMonoBehaviour.StartCoroutine(loadTapsellNativeBannerAdImages(result));

                    }
                    else if (result.adNetwork.Equals("AD_MOB"))
                    {

                        loadNativeBannerAdImages(result);

                    }
                }
                else
                {
                    Debug.Log("Invalid MonoBehaviour Object");
                    onRequestError(new TapsellError(result.zoneId, "Invalid MonoBehaviour Object"));
                }

            }
            else
            {
                Debug.Log("Invalid Native Banner Ad Result");
                onRequestError(new TapsellError(result.zoneId, "Invalid Native Banner Ad Result"));
            }
        }

        private static void loadNativeBannerAdImages(TapsellPlusNativeBannerAd result)
        {

            Debug.Log("loadNativeBannerAdImages " + result.zoneId);

            zoneId = result.zoneId;
            adNetworkZoneId = result.adNetworkZoneId;

            var adLoader = new AdLoader.Builder(result.adNetworkZoneId).ForUnifiedNativeAd().Build();
            adLoader.OnUnifiedNativeAdLoaded += HandleOnUnifiedNativeAdLoaded;
            adLoader.OnAdFailedToLoad += HandleNativeAdFailedToLoad;

            adLoader.LoadAd(AdRequestBuild());
        }

        private static IEnumerator loadTapsellNativeBannerAdImages(TapsellPlusNativeBannerAd result)
        {
            if (result.iconUrl != null && !result.iconUrl.Equals(""))
            {
                var wwwIcon = UnityWebRequestTexture.GetTexture(result.iconUrl);
                yield return wwwIcon.SendWebRequest();
                if (wwwIcon.isNetworkError || wwwIcon.isHttpError)
                {
                    Debug.Log(wwwIcon.error);
                }
                else
                {
                    result.iconImage = ((DownloadHandlerTexture)wwwIcon.downloadHandler).texture;
                }
            }

            if (result.portraitStaticImageUrl != null && !result.portraitStaticImageUrl.Equals(""))
            {
                var wwwPortrait = UnityWebRequestTexture.GetTexture(result.portraitStaticImageUrl);
                yield return wwwPortrait.SendWebRequest();
                if (wwwPortrait.isNetworkError || wwwPortrait.isHttpError)
                {
                    Debug.Log(wwwPortrait.error);
                }
                else
                {
                    result.portraitBannerImage = ((DownloadHandlerTexture)wwwPortrait.downloadHandler).texture;
                }
            }

            if (result.landscapeStaticImageUrl != null && !result.landscapeStaticImageUrl.Equals(""))
            {
                var wwwLandscape = UnityWebRequestTexture.GetTexture(result.landscapeStaticImageUrl);
                yield return wwwLandscape.SendWebRequest();
                if (wwwLandscape.isNetworkError || wwwLandscape.isHttpError)
                {
                    Debug.Log(wwwLandscape.error);
                }
                else
                {
                    result.landscapeBannerImage = ((DownloadHandlerTexture)wwwLandscape.downloadHandler).texture;
                }
            }

            CallIfAvailable(nativeBannerResponsePool, result.zoneId, result);
        }

        private static void HandleOnUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
        {

            Debug.Log("HandleOnUnifiedNativeAdLoaded" + zoneId);
            
            var tapsellPlusNativeBannerAd = new TapsellPlusNativeBannerAd
            {
                UnifiedNativeAd = args.nativeAd
            };

            var iconTexture = args.nativeAd.GetIconTexture();

            if (args.nativeAd.GetImageTextures().Count > 0)
            {

                var goList = args.nativeAd.GetImageTextures();
                tapsellPlusNativeBannerAd.landscapeBannerImage = goList[0];

            }

            tapsellPlusNativeBannerAd.description= args.nativeAd.GetBodyText();
            tapsellPlusNativeBannerAd.title = args.nativeAd.GetHeadlineText();
            tapsellPlusNativeBannerAd.callToActionText = args.nativeAd.GetCallToActionText();
            
            plugin.sendResponseReportToAndroid(zoneId,  "AD_MOB");
            plugin.sendWinReportToAndroid(zoneId, adNetworkZoneId,"AD_MOB");

            CallIfAvailable(nativeBannerResponsePool, zoneId, tapsellPlusNativeBannerAd);

        }

        private static void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            plugin.sendErrorReportToAndroid(zoneId, "AD_MOB", args.Message);
            Debug.Log("Native ad failed to load: " + args.Message);
        }

        private static AdRequest AdRequestBuild()
        {
            return new AdRequest.Builder().Build();
        }

        internal static void onRequestResponse(string zoneId)
        {
            CallIfAvailable(requestResponsePool, zoneId, zoneId);
        }

        internal static void onRequestError(TapsellError error)
        {
            CallIfAvailable(requestErrorPool, error.zoneId, error);
        }

        internal static void onOpenAd(string zoneId)
        {
            CallIfAvailable(openAdPool, zoneId, zoneId);
        }

        internal static void onCloseAd(string zoneId)
        {
            CallIfAvailable(closeAdPool, zoneId, zoneId);
        }

        internal static void onReward(string zoneId)
        {
            CallIfAvailable(rewardPool, zoneId, zoneId);
        }

        internal static void onError(TapsellError error)
        {
            CallIfAvailable(errorPool, error.zoneId, error);
        }
    }
}