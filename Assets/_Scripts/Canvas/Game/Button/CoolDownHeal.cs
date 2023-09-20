public class CoolDownHeal : BaseCoolDown
{
    protected override void Start()
    {
        this.delay = AbilityCtrl.Instance.AbilityHeal.Delay;
    }

    public override void Update()
    {
        this.timer = AbilityCtrl.Instance.AbilityHeal.Timer;
        base.Update();
    }
}
