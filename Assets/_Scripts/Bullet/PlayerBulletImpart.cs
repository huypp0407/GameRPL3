using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerBulletImpart : BulletImpact
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") return;
        this.allBulletCtrl.DamageSender.Send(other.transform);
    }
}
