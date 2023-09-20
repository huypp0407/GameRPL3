using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoolDownFlash : BaseCoolDown
{
    protected override void Start()
    {
        base.Start();
        this.delay = AbilityCtrl.Instance.AbilityWarp.Delay;

    }

    public override void Update()
    {
        this.timer = AbilityCtrl.Instance.AbilityWarp.Timer;
        base.Update();
    }
}
