using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

namespace TapsellPlusSDK
{
    public class TapsellPlusNativeBannerAd : TapsellPlusAdModel
    {
        public readonly string adNetwork;
        public readonly string title;
        public readonly string description;
        public readonly string callToActionText;
        public readonly Texture2D portraitBannerImage;
        public readonly Texture2D landscapeBannerImage;
        public readonly Texture2D iconImage;
        private readonly UnifiedNativeAd _unifiedNativeAd;

        public TapsellPlusNativeBannerAd(string responseId, string zoneId,
            string adNetwork, string title,
            string description, string callToActionText,
            Texture2D portraitBannerImage, Texture2D landscapeBannerImage,
            Texture2D iconImage, UnifiedNativeAd unifiedNativeAd) : base(responseId, zoneId)
        {
            this.adNetwork = adNetwork;
            this.title = title;
            this.description = description;
            this.callToActionText = callToActionText;
            this.portraitBannerImage = portraitBannerImage;
            this.landscapeBannerImage = landscapeBannerImage;
            this.iconImage = iconImage;
            _unifiedNativeAd = unifiedNativeAd;
        }

        private void Clicked()
        {
            TapsellPlus.NativeBannerAdClicked(responseId);
        }

        public void RegisterBodyTextGameObject(GameObject gameObject)
        {
            if (CallTapsellRegister(gameObject)) return;
            _unifiedNativeAd.RegisterBodyTextGameObject(gameObject);
        }
        public void RegisterCallToActionGameObject(GameObject gameObject)
        {
            if (CallTapsellRegister(gameObject)) return;
            _unifiedNativeAd.RegisterCallToActionGameObject(gameObject);
        }
        public void RegisterHeadlineTextGameObject(GameObject gameObject)
        {
            if (CallTapsellRegister(gameObject)) return;
            _unifiedNativeAd.RegisterHeadlineTextGameObject(gameObject);
        }
        public void RegisterIconImageGameObject(GameObject gameObject)
        {
            if (CallTapsellRegister(gameObject)) return;
            _unifiedNativeAd.RegisterIconImageGameObject(gameObject);
        }
        public void RegisterImageGameObject(GameObject gameObject)
        {
            if (CallTapsellRegister(gameObject)) return;
            var list = new List<GameObject> {gameObject.gameObject};
            _unifiedNativeAd.RegisterImageGameObjects(list);
        }
        public void Register3DItem(GameObject gameObject)
        {
            RegisterTapsellComponent(gameObject);
            
        }
        private bool CallTapsellRegister(GameObject gameObject)
        {
            UnRegisterTapsellComponent(gameObject);
            if (_unifiedNativeAd != null) return false;
            RegisterTapsellComponent(gameObject);
                
            return true;

        }
        private static void UnRegisterTapsellComponent(GameObject gameObject)
        {
            if (!(gameObject.transform as RectTransform)) return;
            var component = gameObject.GetComponent<Button>();
            if (component == null)
            {
                component = gameObject.AddComponent<Button>();
            }
            component.onClick.RemoveAllListeners();
        }
        private void RegisterTapsellComponent(GameObject gameObject)
        {
            
            if (gameObject.transform as RectTransform)
            {
                
                var component = gameObject.GetComponent<Button>();
                
                if (component == null)
                {
                    
                    component = gameObject.AddComponent<Button>();
                }
                
                component.onClick.AddListener((Clicked));
            }
            else
            {

                var collider = gameObject.GetComponent<Collider>(); 
                
                if (collider == null)
                {
                
                    collider = gameObject.AddComponent<Collider>();
                }

                collider.gameObject.AddComponent<ClickHandler>().ONClick += Clicked;
            }
        }
    }
}
