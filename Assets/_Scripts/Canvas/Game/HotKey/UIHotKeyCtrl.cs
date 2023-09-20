using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHotKeyCtrl : _MonoBehaviour
{
    private static UIHotKeyCtrl instance;
    public static UIHotKeyCtrl Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (UIHotKeyCtrl.instance != null) return;
        UIHotKeyCtrl.instance = this;
    }
}
