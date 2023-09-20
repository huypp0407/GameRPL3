using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class RewardAdmob : MonoBehaviour
{
    protected virtual void Start()
    {
        AdmobAds.Instance.LoadRewardAd();
        AdmobAds.Instance.DestroyBanner();
    }
}
