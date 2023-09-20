using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackMele : MeleStat
{
    public EnemyCtrl enemyCtrl;

    private void Awake()
    {
        this.enemyCtrl.AnimationEvent.OnCustomEvent += this.Attack;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && isAttack)
        {
            this.damageSender.Send(other.transform);
            isAttack = false;
        }
    }
}
