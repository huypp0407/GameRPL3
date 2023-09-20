using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAdmob : MonoBehaviour
{
    private void Start()
    {
        AdmobAds.Instance.LoadBanner();
        AdmobAds.Instance.LoadInterAd();
    }
}
