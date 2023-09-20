using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : _MonoBehaviour
{
    [SerializeField] protected float hp  = 1;
    public float Hp => hp;

    [SerializeField] protected float hpMax = 3;
    public float HpMax => hpMax;

    [SerializeField] protected bool isDead = false;
    public bool Isdead => isDead;

    [SerializeField] protected PlayerDame playerDame;
    public PlayerDame PlayerDame => playerDame;

    protected override void Awake()
    {
        base.Awake();
        this.Reborn();

    }

    private void OnEnable()
    {
        this.Reborn();
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.Reborn();
    }

    public virtual void Reborn()
    {
        this.hp = this.hpMax;
        this.isDead = false;
    }

    public virtual void Add(float add)
    {
        if (this.isDead) return;
        this.hp += add;
        this.playerDame.Heal(add);
        if (this.hp > this.hpMax) this.hp = this.hpMax;
    }

    public virtual void Deduct(float add)
    {
        if (this.isDead) return;
        this.hp -= add;
        this.playerDame.TakeDamage(add);
        if (this.hp < 0) this.hp = 0;
        this.CheckIsDead();
    }

    protected virtual bool IsDead()
    {
        return this.hp <= 0;
    }

    protected virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.OnDead();
    }

    protected abstract void OnDead();
}