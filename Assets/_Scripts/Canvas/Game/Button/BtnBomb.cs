using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBomb : BaseButton
{
    protected override void OnClick()
    {
        PlayerShooting.Instance.GetBomb();
        UIButtonBoomCtrl.Instance.SetTypeBomb("Bomb");
        if(UIButtonBom.Instance.IsOpen) return;
        UIButtonBom.Instance.Toggle();
    }
}
