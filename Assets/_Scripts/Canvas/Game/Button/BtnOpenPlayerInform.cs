using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOpenPlayerInform : BaseButton
{
    protected override void OnClick()
    {
        UIInform.Instance.Toggle();
    }
}
