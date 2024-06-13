using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletImpart : BulletImpact
{
    protected virtual void OnTriggerEnter(Collider other)
    {
      Debug.LogError($"HUYPP :: EnemyBulletImpart :: {other.name}");
        if (other.name == "Player") 
        {
            this.allBulletCtrl.DamageSender.Send(other.transform);
            AudioClip audioClip = this.allBulletCtrl.BulletSO.bloodSplat;
            SoundSpawner.Instance.PlayEffect(audioClip, transform.position, transform.rotation);
        }
        
    }
}
