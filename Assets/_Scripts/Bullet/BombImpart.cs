using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BombImpart : BulletImpact {
    protected bool isTrigger = false;

    private void OnEnable() {
      isTrigger = false;
      Invoke(nameof(BombSendDamge),4f);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (Regex.Match(other.name, "Enemy.*").Success) {
          BombSendDamge();
        }
    }

    protected void BombSendDamge() {
      if (!isTrigger) {
        Transform fxDamage = FXSpawner.Instance.SpawnFx("BombFX", transform.position, transform.rotation);
        isTrigger = true;
        Collider[] enemy = Physics.OverlapBox(transform.position, transform.localScale);
        foreach(var e in enemy)
        {
          this.allBulletCtrl.DamageSender.Send(e.transform);
        }
        this.allBulletCtrl.bulletSpawner.Despawn(transform.parent);
      }
    }
}
