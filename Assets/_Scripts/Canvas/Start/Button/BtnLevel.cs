using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLevel : BaseButton
{
    protected override void OnClick()
    {
        UILevel.Instance.Toggle();
        Debug.Log("Choose lv");
    }
}
