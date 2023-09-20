using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAdmob : MonoBehaviour
{
    [System.Obsolete]
    private void Start()
    {
        AdmobAds.Instance.LoadBanner();
        AdmobAds.Instance.LoadInterAd();
    }
}
