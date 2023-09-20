using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnBoomerang : BaseButton
{
    protected override void OnClick()
    {
        BulletSpawner.Instance.SetBoomerang();
    }
}
