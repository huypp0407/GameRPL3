using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOpenDailyBonus : BaseButton
{
    protected override void OnClick()
    {
        UIDailyBonus.Instance.Toggle();
    }
}
