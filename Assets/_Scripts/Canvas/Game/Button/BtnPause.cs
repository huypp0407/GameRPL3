using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnPause : BaseButton
{
    protected override void OnClick()
    {
      Debug.LogError($"HUYPP :: OnClick");
        UIPause.Instance.Toggle();
    }
}
