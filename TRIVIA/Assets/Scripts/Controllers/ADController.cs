using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
public class ADController : MonoBehaviour
{
    private InterstitialAD interstitial;
    private BannerAD banner;

    public static ADController Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        banner = GetComponent<BannerAD>();
        interstitial = GetComponent<InterstitialAD>();
    }
    private void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            interstitial.LoadInterstitialAd();
            ShowBanner();
        });
    }
    public void ShowInterstitial()
    {
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber <= 0.4)
        {
            interstitial.ShowAd();
        }
        
    }

    public void ShowBanner()
    {
        banner.LoadAd();
    }
}
