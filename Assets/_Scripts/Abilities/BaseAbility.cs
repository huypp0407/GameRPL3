using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : _MonoBehaviour
{
    [Header("Base Ability")]

    [SerializeField] protected bool pressed = false;

    [SerializeField] protected float timer = 4f;
    [SerializeField] protected float delay = 5f;
    public float Timer => timer;
    public float Delay => delay;
    [SerializeField] protected bool isReady = true;
    public bool IsReady => isReady;

    [SerializeField] protected Abilities abilities;
    public Abilities Abilities => abilities;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAbilities();
    }

    protected virtual void LoadAbilities()
    {
        if (this.abilities != null) return;
        this.abilities = transform.parent.GetComponent<Abilities>();
    }

    protected virtual void FixedUpdate()
    {
        this.Timing();
    }

    protected virtual void Update()
    {
    }

    protected virtual void Timing()
    {
        if (this.isReady) return;
        this.timer += Time.fixedDeltaTime;
        if (this.timer < this.delay) return;
        this.isReady = true;
    }

    public virtual void Active()
    {
        this.isReady = false;
        this.timer = 0;
        this.pressed = false;
    }

    public virtual void SetPress()
    {
        if (!this.isReady || UIInventory.Instance.IsOpen) return;
        this.pressed = true;
    }
}
