using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnFlash : BaseButton
{
    protected override void OnClick()
    {
        AbilityCtrl.Instance.AbilityWarp.SetPress();
    }
}
