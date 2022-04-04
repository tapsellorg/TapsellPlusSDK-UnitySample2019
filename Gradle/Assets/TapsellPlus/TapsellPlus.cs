using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace TapsellPlusSDK
{
    public static class TapsellPlus
    {
        private static Action<string> _successInitializeCallback;
        private static Action<TapsellPlusAdNetworkError> _failedInitializeCallback;

        private static readonly Dictionary<string, Action<TapsellPlusAdModel>> RequestResponseCallbackPool
            = new Dictionary<string, Action<TapsellPlusAdModel>>();

        private static readonly Dictionary<string, Action<TapsellPlusRequestError>> RequestErrorCallbackPool
            = new Dictionary<string, Action<TapsellPlusRequestError>>();

        private static readonly Dictionary<string, Action<TapsellPlusAdModel>> OpenAdCallbackPool
            = new Dictionary<string, Action<TapsellPlusAdModel>>();

        private static readonly Dictionary<string, Action<TapsellPlusAdModel>> CloseAdCallbackPool
            = new Dictionary<string, Action<TapsellPlusAdModel>>();

        private static readonly Dictionary<string, Action<TapsellPlusAdModel>> RewardAdCallbackPool
            = new Dictionary<string, Action<TapsellPlusAdModel>>();

        private static readonly Dictionary<string, Action<TapsellPlusErrorModel>> ShowErrorAdCallbackPool
            = new Dictionary<string, Action<TapsellPlusErrorModel>>();

        private static readonly Dictionary<string, Action<TapsellPlusNativeBannerAd>> OpenNativeAdCallbackPool
            = new Dictionary<string, Action<TapsellPlusNativeBannerAd>>();

        private static MonoBehaviour _mMonoBehaviour;
        private static GameObject _tapsellPlusManager;
        private static TapsellPlusPlugin _plugin;

        private static string _adMobNativeAdResponseId;
        private static string _adMobNativeAdNetworkZoneId;
        private static string _adMobNativeAdZoneId;
        private static TapsellPlusNativeBannerAd _adMobNativeBannerAd;

        public static void Initialize(string key, Action<string> onInitializeSuccess,
            Action<TapsellPlusAdNetworkError> onInitializeFailed)
        {
            if (_tapsellPlusManager != null) return;
            _tapsellPlusManager = new GameObject("TapsellPlusManager");
            Object.DontDestroyOnLoad(_tapsellPlusManager);
            _tapsellPlusManager.AddComponent<TapsellPlusMessageHandler>();

            _plugin = new TapsellPlusPlugin();
#if UNITY_ANDROID && !UNITY_EDITOR
				_plugin = new TapsellPlusAndroidPlugin ();
#endif

            _successInitializeCallback = onInitializeSuccess;
            _failedInitializeCallback = onInitializeFailed;
            _plugin.Initialize(key);
        }

        public static void RequestRewardedVideoAd(string zoneId, Action<TapsellPlusAdModel> onRequestResponse,
            Action<TapsellPlusRequestError> onRequestError)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AddToPool(RequestResponseCallbackPool, zoneId, onRequestResponse);
            AddToPool(RequestErrorCallbackPool, zoneId, onRequestError);

            _plugin.RequestRewardedVideoAd(zoneId);
#endif
        }

        public static void ShowRewardedVideoAd(string responseId, Action<TapsellPlusAdModel> onAdOpened,
            Action<TapsellPlusAdModel> onAdRewarded, Action<TapsellPlusAdModel> onAdClosed,
            Action<TapsellPlusErrorModel> onShowError)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AddToPool(OpenAdCallbackPool, responseId, onAdOpened);
            AddToPool(RewardAdCallbackPool, responseId, onAdRewarded);
            AddToPool(CloseAdCallbackPool, responseId, onAdClosed);
            AddToPool(ShowErrorAdCallbackPool, responseId, onShowError);

            _plugin.ShowRewardedVideoAd(responseId);
#endif
        }

        public static void RequestInterstitialAd(string zoneId, Action<TapsellPlusAdModel> onRequestResponse,
            Action<TapsellPlusRequestError> onRequestError)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AddToPool(RequestResponseCallbackPool, zoneId, onRequestResponse);
            AddToPool(RequestErrorCallbackPool, zoneId, onRequestError);

            _plugin.RequestInterstitialAd(zoneId);
#endif
        }

        public static void ShowInterstitialAd(string responseId, Action<TapsellPlusAdModel> onAdOpened,
            Action<TapsellPlusAdModel> onAdClosed, Action<TapsellPlusErrorModel> onShowError)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AddToPool(OpenAdCallbackPool, responseId, onAdOpened);
            AddToPool(CloseAdCallbackPool, responseId, onAdClosed);
            AddToPool(ShowErrorAdCallbackPool, responseId, onShowError);

            _plugin.ShowInterstitialAd(responseId);
#endif
        }

        public static void RequestStandardBannerAd(string zoneId, int bannerSize,
            Action<TapsellPlusAdModel> onRequestResponse, Action<TapsellPlusRequestError> onRequestError)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AddToPool(RequestResponseCallbackPool, zoneId, onRequestResponse);
            AddToPool(RequestErrorCallbackPool, zoneId, onRequestError);

            _plugin.RequestStandardBannerAd(zoneId, bannerSize);
#endif
        }

        public static void ShowStandardBannerAd(string responseId, int horizontalGravity, int verticalGravity,
            Action<TapsellPlusAdModel> onAdOpened, Action<TapsellPlusErrorModel> onShowError)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AddToPool(OpenAdCallbackPool, responseId, onAdOpened);
            AddToPool(ShowErrorAdCallbackPool, responseId, onShowError);

            _plugin.ShowStandardBannerAd(responseId, horizontalGravity, verticalGravity);
#endif
        }

        public static void DestroyStandardBannerAd(string responseId)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _plugin.DestroyStandardBannerAd(responseId);
#endif
        }

        public static void HideStandardBannerAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _plugin.HideStandardBannerAd();
#endif
        }

        public static void DisplayStandardBannerAd()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _plugin.DisplayStandardBannerAd();
#endif
        }

        public static void RequestNativeBannerAd(string zoneId, Action<TapsellPlusAdModel> onRequestResponse,
            Action<TapsellPlusRequestError> onRequestError)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AddToPool(RequestResponseCallbackPool, zoneId, onRequestResponse);
            AddToPool(RequestErrorCallbackPool, zoneId, onRequestError);

            _plugin.RequestNativeBannerAd(zoneId);
#endif
        }

        public static void ShowNativeBannerAd(string responseId, MonoBehaviour monoBehaviour,
            Action<TapsellPlusNativeBannerAd> onAdOpened, Action<TapsellPlusErrorModel> onShowError)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AddToPool(OpenNativeAdCallbackPool, responseId, onAdOpened);
            AddToPool(ShowErrorAdCallbackPool, responseId, onShowError);
            _mMonoBehaviour = monoBehaviour;

            if (responseId.Equals(_adMobNativeAdResponseId))
            {
                Debug.Log(_adMobNativeAdNetworkZoneId);
                SendAdMobNativeAdShowStart(_adMobNativeAdResponseId, _adMobNativeAdNetworkZoneId);
                CallIfAvailable(OpenNativeAdCallbackPool, _adMobNativeAdResponseId, _adMobNativeBannerAd);
                SendAdMobNativeAdWin(_adMobNativeAdResponseId, _adMobNativeAdNetworkZoneId);
            }
            else
            {
                _plugin.ShowNativeBannerAd(responseId);
            }
#endif
        }

        internal static void OnTapsellNativeAdOpened(TapsellPlusNativeAd tapsellPlusNativeAd)
        {
            if (tapsellPlusNativeAd.generalNativeAdModel != null)
            {
                if (_mMonoBehaviour != null && _mMonoBehaviour.isActiveAndEnabled)
                {
                    if (tapsellPlusNativeAd.generalNativeAdModel.adNetwork.Equals("TAPSELL",
                        StringComparison.InvariantCultureIgnoreCase))
                        _mMonoBehaviour.StartCoroutine(LoadTapsellNativeAdComponents(tapsellPlusNativeAd));
                }
                else
                {
                    Debug.Log("Invalid MonoBehaviour Object");
                    OnAdShowError(new TapsellPlusErrorModel(tapsellPlusNativeAd.responseId, tapsellPlusNativeAd.zoneId,
                        "Invalid MonoBehaviour Object"));
                }
            }
            else
            {
                Debug.Log("Invalid Native Banner Ad Result");
                OnAdShowError(new TapsellPlusErrorModel(tapsellPlusNativeAd.responseId, tapsellPlusNativeAd.zoneId,
                    "Invalid Native Banner Ad Result"));
            }
        }

        private static IEnumerator LoadTapsellNativeAdComponents(TapsellPlusNativeAd tapsellPlusNativeAd)
        {
            var tapsellNativeAd = tapsellPlusNativeAd.generalNativeAdModel;
            Texture2D iconImage = null;
            Texture2D portraitBannerImage = null;
            Texture2D landscapeBannerImage = null;
            if (tapsellNativeAd.iconUrl != null && !tapsellNativeAd.iconUrl.Equals(""))
            {
                var wwwIcon = UnityWebRequestTexture.GetTexture(tapsellNativeAd.iconUrl);
                yield return wwwIcon.SendWebRequest();
                if (wwwIcon.result == UnityWebRequest.Result.ConnectionError ||
                    wwwIcon.result == UnityWebRequest.Result.ProtocolError)
                    Debug.Log(wwwIcon.error);
                else
                    iconImage = ((DownloadHandlerTexture) wwwIcon.downloadHandler).texture;
            }

            if (tapsellNativeAd.portraitStaticImageUrl != null && !tapsellNativeAd.portraitStaticImageUrl.Equals(""))
            {
                var wwwPortrait = UnityWebRequestTexture.GetTexture(tapsellNativeAd.portraitStaticImageUrl);
                yield return wwwPortrait.SendWebRequest();
                if (wwwPortrait.result == UnityWebRequest.Result.ConnectionError ||
                    wwwPortrait.result == UnityWebRequest.Result.ProtocolError)
                    Debug.Log(wwwPortrait.error);
                else
                    portraitBannerImage = ((DownloadHandlerTexture) wwwPortrait.downloadHandler).texture;
            }

            if (tapsellNativeAd.landscapeStaticImageUrl != null && !tapsellNativeAd.landscapeStaticImageUrl.Equals(""))
            {
                var wwwLandscape = UnityWebRequestTexture.GetTexture(tapsellNativeAd.landscapeStaticImageUrl);
                yield return wwwLandscape.SendWebRequest();
                if (wwwLandscape.result == UnityWebRequest.Result.ConnectionError ||
                    wwwLandscape.result == UnityWebRequest.Result.ProtocolError)
                    Debug.Log(wwwLandscape.error);
                else
                    landscapeBannerImage = ((DownloadHandlerTexture) wwwLandscape.downloadHandler).texture;
            }

            var tapsellPlusNativeBannerAd
                = new TapsellPlusNativeBannerAd(tapsellPlusNativeAd.responseId, tapsellPlusNativeAd.zoneId,
                    "Tapsell", tapsellNativeAd.title,
                    tapsellNativeAd.description, tapsellNativeAd.callToActionText,
                    portraitBannerImage, landscapeBannerImage,
                    iconImage, null);
            CallIfAvailable(OpenNativeAdCallbackPool, tapsellPlusNativeAd.responseId, tapsellPlusNativeBannerAd);
        }

        public static void OnAdMobNativeAdRequest(AdMobNativeAd adMobNativeAd)
        {
            _adMobNativeAdZoneId = adMobNativeAd.zoneId;
            _adMobNativeAdResponseId = adMobNativeAd.responseId;
            _adMobNativeAdNetworkZoneId = adMobNativeAd.adNetworkZoneId;
            var adLoader = new AdLoader.Builder(_adMobNativeAdNetworkZoneId).ForUnifiedNativeAd().Build();
            adLoader.OnUnifiedNativeAdLoaded += HandleOnUnifiedNativeAdLoaded;
            adLoader.OnAdFailedToLoad += HandleNativeAdFailedToLoad;

            adLoader.LoadAd(AdRequestBuild());
        }

        private static void HandleOnUnifiedNativeAdLoaded(object sender, UnifiedNativeAdEventArgs args)
        {
            var iconTexture = args.nativeAd.GetIconTexture();
            Texture2D landscapeBannerImage = null;

            if (args.nativeAd.GetImageTextures().Count > 0)
            {
                var goList = args.nativeAd.GetImageTextures();
                landscapeBannerImage = goList[0];
            }

            SendAdMobNativeAdSuccessReport(_adMobNativeAdResponseId, _adMobNativeAdNetworkZoneId);
            var tapsellPlusNativeBannerAd =
                new TapsellPlusNativeBannerAd(_adMobNativeAdResponseId, _adMobNativeAdZoneId,
                    "admob", args.nativeAd.GetHeadlineText(),
                    args.nativeAd.GetBodyText(), args.nativeAd.GetCallToActionText(),
                    null, landscapeBannerImage,
                    iconTexture, args.nativeAd);
            _adMobNativeBannerAd = tapsellPlusNativeBannerAd;
            OnRequestResponse(new TapsellPlusAdModel(_adMobNativeAdResponseId, _adMobNativeAdZoneId));
        }

        private static void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            var adNetworkShowErrorModel =
                new AdNetworkShowErrorModel(_adMobNativeAdNetworkZoneId, "admob", args.Message);
            SendAdMobNativeAdFailedReport(_adMobNativeAdResponseId, _adMobNativeAdZoneId, adNetworkShowErrorModel);
            _adMobNativeAdZoneId = null;
            _adMobNativeAdResponseId = null;
            _adMobNativeAdNetworkZoneId = null;
        }

        private static void SendAdMobNativeAdSuccessReport(string responseId, string adNetworkZoneId)
        {
            _plugin.SendAdMobNativeAdSuccessReport(responseId, adNetworkZoneId);
        }

        private static void SendAdMobNativeAdFailedReport(string responseId, string zoneId,
            AdNetworkShowErrorModel adNetworkShowErrorModel)
        {
            _plugin.SendAdMobNativeAdFailedReport(zoneId, responseId, JsonUtility.ToJson(adNetworkShowErrorModel));
        }

        private static void SendAdMobNativeAdWin(string responseId, string adNetworkZoneId)
        {
            _plugin.SendAdMobNativeAdWin(responseId, adNetworkZoneId);
        }

        private static void SendAdMobNativeAdShowStart(string responseId, string adNetworkZoneId)
        {
            _plugin.SendAdMobNativeAdShowStart(responseId, adNetworkZoneId);
        }

        internal static GameObject GetTapsellPlusManager()
        {
            // Used in iOS Plugin
            return _tapsellPlusManager;
        }

        private static void AddToPool<T>(IDictionary<string, Action<T>> pool, string key, Action<T> item)
        {
            if (item == null) return;
            RemoveFromPool(pool, key);
            pool.Add(key, item);
        }

        private static void RemoveFromPool<T>(IDictionary<string, Action<T>> pool, string key)
        {
            if (pool.ContainsKey(key)) pool.Remove(key);
        }

        private static void CallIfAvailable<T>(IDictionary<string, Action<T>> pool, string key, T input)
        {
            if (pool != null && pool.ContainsKey(key)) pool[key](input);
        }

        public static void SetDebugMode(int logLevel)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _plugin.SetDebugMode(logLevel);
#endif
        }

        public static void SetGdprConsent(bool consent)
        {
#if UNITY_ANDROID && !UNITY_EDITOR            
            _plugin.SetGdprConsent(consent);
#endif
        }

        internal static void NativeBannerAdClicked(string responseId)
        {
            _plugin.NativeBannerAdClicked(responseId);
        }

        private static AdRequest AdRequestBuild()
        {
            return new AdRequest.Builder().Build();
        }

        internal static void OnInitializeSuccess(string adNetworkName)
        {
            // To Call _successInitializeCallback when it is not null
            _successInitializeCallback?.Invoke(adNetworkName);
        }

        internal static void OnInitializeFailed(TapsellPlusAdNetworkError tapsellPlusAdNetworkError)
        {
            // To Call _failedInitializeCallback when it is not null
            _failedInitializeCallback?.Invoke(tapsellPlusAdNetworkError);
        }

        internal static void OnRequestResponse(TapsellPlusAdModel tapsellPlusAdModel)
        {
            CallIfAvailable(RequestResponseCallbackPool, tapsellPlusAdModel.zoneId, tapsellPlusAdModel);
        }

        internal static void OnRequestError(TapsellPlusRequestError tapsellPlusRequestError)
        {
            CallIfAvailable(RequestErrorCallbackPool, tapsellPlusRequestError.zoneId, tapsellPlusRequestError);
        }

        internal static void OnAdOpened(TapsellPlusAdModel tapsellPlusAdModel)
        {
            CallIfAvailable(OpenAdCallbackPool, tapsellPlusAdModel.responseId, tapsellPlusAdModel);
        }

        internal static void OnAdClosed(TapsellPlusAdModel tapsellPlusAdModel)
        {
            CallIfAvailable(CloseAdCallbackPool, tapsellPlusAdModel.responseId, tapsellPlusAdModel);
            RemoveFromPool(CloseAdCallbackPool, tapsellPlusAdModel.responseId);
        }

        internal static void OnAdRewarded(TapsellPlusAdModel tapsellPlusAdModel)
        {
            CallIfAvailable(RewardAdCallbackPool, tapsellPlusAdModel.responseId, tapsellPlusAdModel);
        }

        internal static void OnAdShowError(TapsellPlusErrorModel tapsellPlusErrorModel)
        {
            CallIfAvailable(ShowErrorAdCallbackPool, tapsellPlusErrorModel.responseId, tapsellPlusErrorModel);
        }
    }
}