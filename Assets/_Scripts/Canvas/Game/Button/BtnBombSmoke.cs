using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBombSmoke : BaseButton
{
    protected override void OnClick()
    {
      PlayerShooting.Instance.GetSmoke();
      UIButtonBoomCtrl.Instance.SetTypeBomb("Smoke");
      if(UIButtonBom.Instance.IsOpen) return;
      UIButtonBom.Instance.Toggle();
    }
}
