public class AbilityHeal : BaseAbility
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 10f;
    }

    protected override void Update()
    {
        base.Update();
        this.Heal();
    }

    protected virtual void Heal()
    {
        if (!this.isReady) return;
        if (this.pressed == true) this.Healing();
    }

    protected virtual void Healing()
    {
        PlayerCtrl.Instance.PlayerDamageReceiver.Add(10);
        this.Active();;
    }
}
