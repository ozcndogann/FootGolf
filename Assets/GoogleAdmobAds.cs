using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class GoogleAdmobAds : MonoBehaviour
{
    [SerializeField] public const string _bannerId = "banner id";
    [SerializeField] public const string _interstitialId = " interstitial id ";
    [SerializeField] private const string _rewardedId = " rewarded id ";

    private BannerView _bannerView;
    private InterstitialAd _interstitialAd;
    private RewardedAd _rewardedAd;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
        RequestInterstitial();
        RequestRewarded();
    }

    private void RequestBanner()
    {
        _bannerView = new BannerView(_bannerId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        _bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        _interstitialAd = new InterstitialAd(_interstitialId);
        _interstitialAd.OnAdClosed += HandleOnAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(request);
    }

    public void DisplayInterstitialAd()
    {
        if (_interstitialAd.IsLoaded())
            _interstitialAd.Show();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        _interstitialAd.Destroy();
        RequestInterstitial();
    }

    // REWARDED

    private void RequestRewarded()
    {
        _rewardedAd = new RewardedAd(_rewardedId);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        _rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(request);
    }

    public void DisplayRewardedAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }

    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {

    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RequestRewarded();
    }

    private void OnDestroy()
    {
        _rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        _rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
    }
}
