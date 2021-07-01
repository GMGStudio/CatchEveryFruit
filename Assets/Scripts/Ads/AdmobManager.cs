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
    private string BannerID = "ca-app-pub-3940256099942544/2934735716";
    private string InterstitialID = "ca-app-pub-3940256099942544/4411468910";
#elif UNITY_ANDROID
    private string BannerID = "ca-app-pub-3940256099942544/6300978111";
    private string InterstitialID = "ca-app-pub-3940256099942544/1033173712";

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
        if(bannerAd == null) {  return; }
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
