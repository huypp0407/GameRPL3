using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : Shooter
{
    public EnemyCtrl enemyCtrl;

    protected virtual void OnEnable()
    {
        int currentLvel = MapLevel.Instance.LevelCurrent-1;
        this.delay = this.enemyCtrl.EnemySO.upgradeLevels[currentLvel].ememySpeed;
    }

    protected override void SpawnBullet(Vector3 spawnPos, Quaternion rotation)
    {
        BulletSpawner.Instance.Spawn("Bullet_3", spawnPos, rotation);
        AudioClip audioClip = this.enemyCtrl.EnemySO.punch;
        SoundSpawner.Instance.PlayEffect(audioClip, transform.position, transform.rotation);
    }

    public override void Attack()
    {
        //yield return new WaitForSeconds(0.5f);
        this.enemyCtrl.Animator.SetTrigger("Attack");
    }
}
