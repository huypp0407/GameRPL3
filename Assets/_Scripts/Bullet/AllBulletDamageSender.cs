using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBulletDamageSender : DamageSender
{
    public AllBulletCtrl allBulletCtrl;

    private void OnEnable()
    {
        if (this.allBulletCtrl == null)
        {
            this.damage = 5;
            return;
        }
        this.damage = this.allBulletCtrl.BulletSO.damage;
    }
}
