using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnChangeWeapon : BaseButton
{
    protected override void OnClick()
    {
        PlayerShooting.Instance.SetCurrentWeapon();
        if (!UIButtonBom.Instance.IsOpen) return;
        UIButtonBom.Instance.Toggle();
    }
}
