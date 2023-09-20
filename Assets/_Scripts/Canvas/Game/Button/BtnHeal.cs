using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnHeal : BaseButton
{
    protected override void OnClick()
    {
        AbilityCtrl.Instance.AbilityHeal.SetPress();
    }
}
