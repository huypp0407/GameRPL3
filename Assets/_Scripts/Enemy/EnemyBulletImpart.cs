using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletImpart : BulletImpact
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") 
        {
            other.ClosestPointOnBounds(transform.position);       
            this.allBulletCtrl.DamageSender.Send(other.transform);
            AudioClip audioClip = this.allBulletCtrl.BulletSO.bloodSplat;
            SoundSpawner.Instance.PlayEffect(audioClip, transform.position, transform.rotation);
        }
        
    }
}
