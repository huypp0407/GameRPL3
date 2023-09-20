using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnShoot : BaseButton
{
    protected override void OnClick()
    {
        AbilityCtrl.Instance.AbilityShoot.SetPress();
    }
}
