using UnityEngine;

public class BoomerangDamageSender : AllBulletDamageSender
{
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadBoomerangCrl();
    }

    protected virtual void LoadBoomerangCrl()
    {
        if (this.allBulletCtrl != null) return;
        this.allBulletCtrl = transform.parent.GetComponent<AllBulletCtrl>();
    }

    private void OnEnable()
    {
        this.damage = this.allBulletCtrl.BulletSO.damage;
    }
}
