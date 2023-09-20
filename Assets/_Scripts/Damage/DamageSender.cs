using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : _MonoBehaviour
{
    [SerializeField] protected float damage = 0.1f;
    [SerializeField] protected float damageCrit;

    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponentInChildren<DamageReceiver>();
        if (damageReceiver == null) return;
        this.CreateImpactFX(FXSpawner.impactOne, obj.transform.position + obj.forward);
        this.CreateDamageFX(FXSpawner.impactThree, obj.position);
        this.Send(damageReceiver);
    }

    protected virtual void CreateImpactFX(string fxName, Vector3 pos)
    {
        Transform fxImpact = FXSpawner.Instance.SpawnFx(fxName, pos, transform.rotation);
    }

    protected void CreateDamageFX(string fxName, Vector3 pos)
    {
        Transform fxDamage = FXSpawner.Instance.SpawnFx(fxName, pos, transform.rotation);
        fxDamage.gameObject.SetActive(true);
        float rand = Random.Range(0, 100);
        bool isCrit = false;
        this.damageCrit = this.damage;
        if (rand <= 50)
        {
            this.damageCrit = this.damageCrit * 1.5f;
            isCrit = true;
        }
        DamageCtrl damageCtrl = fxDamage.GetComponent<DamageCtrl>();
        damageCtrl.SetUp(isCrit, this.damageCrit);
    }

    public virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.Deduct(this.damageCrit);
    }

}
