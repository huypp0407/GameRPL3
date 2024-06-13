using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GhostShooting : EnemyShootingCtrl
{
    protected override void SpawnBullet(Vector3 spawnPos, Quaternion rotation) {
      enemyCtrl.Animator.SetTrigger("Attack");
      int rand = Random.Range(0, 3);
      Vector3 pos = default;
      if (rand == 0) pos = new Vector3(1.3f, 0, 0);
      if(rand == 1) pos = new Vector3(0, 0, 1.3f);
      if(rand == 2) pos = new Vector3(0.92f, 0, 0.92f);
      List<Transform> fxPos= new List<Transform>();
      for (int i = -1; i < 2; i++) {
        Transform fx = FXSpawner.Instance.SpawnFx("Impact_4", PlayerCtrl.Instance.transform.position + i * pos, transform.rotation);
        fxPos.Add(fx);
      }
      float delay = 1f;
      foreach (var fx in fxPos) {
        DOVirtual.DelayedCall(delay += 0.2f, () =>
                                             {
                                               BulletSpawner.Instance.Spawn("Rockfire", fx.position + new Vector3(0,20f,0), rotation);
                                             });
      }
      AudioClip audioClip = this.enemyCtrl.EnemySO.punch;
      SoundSpawner.Instance.PlayEffect(audioClip, transform.position, transform.rotation);
    }

    public override void Attack()
    {
        //yield return new WaitForSeconds(0.5f);
        this.enemyCtrl.Animator.SetTrigger("Attack");
    }
}
