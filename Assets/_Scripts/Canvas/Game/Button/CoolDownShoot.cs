public class CoolDownShoot : BaseCoolDown
{
    protected override void Start()
    {
        this.delay = AbilityCtrl.Instance.AbilityShoot.Delay;

    }

    public override void Update()
    {
        this.timer = AbilityCtrl.Instance.AbilityShoot.Timer;
        base.Update();
    }
}
