using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBullet : BaseButton
{
    protected override void OnClick()
    {
        PlayerShooting.Instance.GetGun();
    }
}
