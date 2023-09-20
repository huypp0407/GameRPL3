using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAdmob : MonoBehaviour
{
    private void Start()
    {
        AdmobAds.Instance.LoadBanner();
    }
}
