using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossDamageReceive : EnemyDamageReceive
{
    

    protected override void OnDead()
    {
        EnemySpawner.Instance.ClearEnemyFromBoss();
        BulletSpawner.Instance.DespawnAllBullet();
        UIReward.Instance.ShowReward(this.enemyCtrl);
        TextScore.Instance.canUpgradeScore = true;
       
        UIReward.Instance.Toggle();
        base.OnDead();
    }

    public override void Reborn()
    {
        base.Reborn();
        this.enemyCtrl.Animator.SetFloat("speed", 0f);
    }
}
