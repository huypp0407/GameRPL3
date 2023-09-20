using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnAxe : BaseButton
{
    protected override void OnClick()
    {
        PlayerShooting.Instance.GetAxe();
    }
}
