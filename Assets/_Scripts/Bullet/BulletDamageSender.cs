using UnityEngine;

public class BulletDamageSender : AllBulletDamageSender
{
    

    public override void Send(DamageReceiver damageReceiver)
    {
        base.Send(damageReceiver);
        this.DestroyBullet();
    }

    protected virtual void DestroyBullet()
    {
        if(this.allBulletCtrl.bulletSpawner != null)
            this.allBulletCtrl.bulletSpawner.Despawn(transform.parent);
    }
}
