using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BtnOpenUpgrade : BaseButton
{
    protected override void OnClick()
    {
        UIUpgrade.Instance.Toggle();
    }
}
