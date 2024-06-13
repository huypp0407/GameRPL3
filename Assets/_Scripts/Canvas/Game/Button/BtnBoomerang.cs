using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBoomerang : BaseButton
{
    protected override void OnClick()
    {
        PlayerShooting.Instance.GetBoomerang();
        if(!UIButtonBom.Instance.IsOpen) return;
        UIButtonBom.Instance.Toggle();
    }
}
