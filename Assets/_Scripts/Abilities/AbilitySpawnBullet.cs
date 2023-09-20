using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpawnBullet : BaseAbility
{
    Vector3 rand = Vector3.one;
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Shooting();
    }

    protected virtual void Shooting()
    {
        if (!this.isReady) return;
        this.Shoot();
    }

    protected virtual void Shoot()
    {
        this.Active();
        StartCoroutine(SpawnBullet());
    }

    IEnumerator SpawnBullet()
    {
        float t = 10;
        while(t > 0)
        {
            for(int i = 0; i < 360; i+= 20)
            {
                Transform burn = BulletSpawner.Instance.Spawn("Bullet_3", transform.position, transform.rotation);
                burn.rotation = Quaternion.Euler(0, i, 0);
            }
            t--;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
