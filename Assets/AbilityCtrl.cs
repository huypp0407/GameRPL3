using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCtrl : _MonoBehaviour
{

    private static AbilityCtrl instance;
    public static AbilityCtrl Instance => instance;

    [SerializeField] protected AbilityWarp abilityWarp;
    public AbilityWarp AbilityWarp => abilityWarp;

    [SerializeField] protected AbilityHeal abilityHeal;
    public AbilityHeal AbilityHeal => abilityHeal;

    [SerializeField] protected AbilityShoot abilityShoot;
    public AbilityShoot AbilityShoot => abilityShoot;

    protected override void Awake()
    {
        base.Awake();
        if (AbilityCtrl.instance != null) return;
        AbilityCtrl.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadAbilityWarp();
        this.LoadAbilityHeal();
        this.LoadAbilityShoot();
    }

    protected virtual void LoadAbilityWarp()
    {
        if (this.abilityWarp != null) return;
        this.abilityWarp = GetComponentInChildren<AbilityWarp>();
    }

    protected virtual void LoadAbilityShoot()
    {
        if (this.abilityShoot != null) return;
        this.abilityShoot = GetComponentInChildren<AbilityShoot>();
    }

    protected virtual void LoadAbilityHeal()
    {
        if (this.abilityHeal != null) return;
        this.abilityHeal = GetComponentInChildren<AbilityHeal>();
    }
}
