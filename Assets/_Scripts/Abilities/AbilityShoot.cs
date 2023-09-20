using System.Collections;
using UnityEngine;

public class AbilityShoot : BaseAbility
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 20f;
    }

    protected override void Update()
    {
        base.Update();
        this.Shoot();
    }

    protected virtual void Shoot()
    {
        if (!this.isReady) return;
        if (this.pressed == true) this.Shooting();
    }

    protected virtual void Shooting()
    {
        BulletSpawner.Instance.SetAbilityBullet();
        StartCoroutine(StopSkill());
        this.shootFinish();
    }

    IEnumerator StopSkill()
    {
        yield return new WaitForSeconds(5.2f);
        BulletSpawner.Instance.SetBullet();
    }

    protected virtual void shootFinish()
    {
        this.Active();
    }
}
