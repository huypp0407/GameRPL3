using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBurn : BaseAbility
{
    Vector3 rand = Vector3.one;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Burning();
    }

    protected virtual void Burning()
    {
        if (!this.isReady) return;
        this.Burn();
    }

    protected virtual void Burn()
    {
        rand = new Vector3(Random.Range(-3f, 3f), 0, Random.Range(-3f, 3f));
        Vector3 pos;
        if (rand != Vector3.zero)
            pos = PlayerCtrl.Instance.transform.position + rand;
        else pos = new Vector3(3, 0, 3) + PlayerCtrl.Instance.transform.position;
        Transform burn = BulletSpawner.Instance.Spawn("Bullet_5", pos, transform.rotation);
        this.Active();
    }

}
