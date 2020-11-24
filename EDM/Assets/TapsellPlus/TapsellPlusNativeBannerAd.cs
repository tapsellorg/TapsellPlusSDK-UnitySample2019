using GoogleMobileAds.Api;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TapsellPlusSDK
{
    [Serializable]
    public class TapsellPlusNativeBannerAd
    {
        public string zoneId;
        public string adId;
        public string adNetwork;
        public string title;
        public string description;
        public string callToActionText;
        public string adNetworkZoneId;

        public string iconUrl;
        public string portraitStaticImageUrl;
        public string landscapeStaticImageUrl;

        public Texture2D portraitBannerImage;
        public Texture2D landscapeBannerImage;
        public Texture2D iconImage;
        public UnifiedNativeAd UnifiedNativeAd { get; set; }

        private void clicked()
        {
            TapsellPlus.nativeBannerAdClicked(zoneId, adId);
        }

        public void RegisterBodyTextGameObject(GameObject gameObject)
        {
            if (callTapsellRegister(gameObject)) return;
            UnifiedNativeAd.RegisterBodyTextGameObject(gameObject);
        }
        public void RegisterCallToActionGameObject(GameObject gameObject)
        {
            if (callTapsellRegister(gameObject)) return;
            UnifiedNativeAd.RegisterCallToActionGameObject(gameObject);
        }
        public void RegisterHeadlineTextGameObject(GameObject gameObject)
        {
            if (callTapsellRegister(gameObject)) return;
            UnifiedNativeAd.RegisterHeadlineTextGameObject(gameObject);
        }
        public void RegisterIconImageGameObject(GameObject gameObject)
        {
            if (callTapsellRegister(gameObject)) return;
            UnifiedNativeAd.RegisterIconImageGameObject(gameObject);
        }
        public void RegisterImageGameObject(GameObject gameObject)
        {
            if (callTapsellRegister(gameObject)) return;
            var list = new List<GameObject> {gameObject.gameObject};
            UnifiedNativeAd.RegisterImageGameObjects(list);
        }

        public void Register3DItem(GameObject gameObject)
        {
            RegisterTapsellComponent(gameObject);
            
        }

        private bool callTapsellRegister(GameObject gameObject)
        {
            if (UnifiedNativeAd != null) return false;
            RegisterTapsellComponent(gameObject);
                
            return true;

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
                
                component.onClick.AddListener((clicked));
            }
            else
            {

                var collider = gameObject.GetComponent<Collider>(); 
                
                if (collider == null)
                {
                
                    collider = gameObject.AddComponent<Collider>();
                }

                collider.gameObject.AddComponent<ClickHandler>().ONClick += clicked;
            }
        }
    }
}
