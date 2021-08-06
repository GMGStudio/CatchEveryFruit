using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobManager
{

    private BannerView bannerAd;
    private InterstitialAd interstitialAd;

#if UNITY_IOS
    private string BannerID = "ca-app-pub-YouriOSBannerID";
    private string InterstitialID = "ca-app-pub-YouriOSInterstitialID";
#elif UNITY_ANDROID
    private string BannerID = "ca-app-pub-YourAndroidBannerID";
    private string InterstitialID = "ca-app-pub-YourAndroidInterstitialID";
#else
    private string BannerID = "";
    private string InterstitialID = "";
#endif

    public void Initialize()
    {
        MobileAds.Initialize(initStatus => { });
        CreateBanner();
        RequestBanner();
        CreateInterstitial();
        RequestInterstitial();
    }

    public void ShowBanner()
    {
        bannerAd?.Show();
    }

    public void HideBanner()
    {
        if (bannerAd == null) { return; }
        bannerAd.Hide();
        RequestBanner();
    }

    public void ShowInterstitial()
    {
        interstitialAd.Show();
    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
        ShowBanner();
    }

    private void RequestInterstitial()
    {
        AdRequest request = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(request);
    }

    private void CreateInterstitial()
    {
        interstitialAd = new InterstitialAd(InterstitialID);
        interstitialAd.OnAdClosed += HandleOnAdClosed;
    }

    private void RequestBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        bannerAd.LoadAd(request);
    }

    private void CreateBanner()
    {
        bannerAd = new BannerView(BannerID, AdSize.Banner, AdPosition.Bottom);
    }

}
