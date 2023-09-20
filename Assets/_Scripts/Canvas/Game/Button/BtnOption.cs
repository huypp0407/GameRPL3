using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOption : BaseButton
{
    protected override void OnClick()
    {
        UIOption.Instance.Toggle();
    }
}
