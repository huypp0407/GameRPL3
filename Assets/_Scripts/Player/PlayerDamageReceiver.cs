using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageReceiver : DamageReceiver
{
    public PlayerCtrl playerCtrl;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float delay = 12f;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.hpMax = 100;
        this.hp = 100;
    }

    protected override void OnDead()
    {
        this.OnDeadFX();
        SaveManager.Instance.SaveGame();
        SceneManager.LoadScene("Start");
    }

    protected virtual void OnDeadFX()
    {
        string fxName = this.GetOnDeadFXName();
        Vector3 spawnPos = transform.position;
        spawnPos.y = 20;
        Transform fxOnDead = FXSpawner.Instance.Spawn(fxName, spawnPos, transform.rotation);
        //fxOnDead.gameObject.SetActive(true);
    }

    public override void Deduct(float add)
    {
        base.Deduct(add);
        this.playerCtrl.Animator.SetBool("isHit", false);

        this.playerCtrl.Animator.SetBool("isHit", true);
        StartCoroutine(this.StopAnimation());
    }

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(0.32f);
        this.playerCtrl.Animator.SetBool("isHit", false);
    }

    public override void Add(float add)
    {
        base.Add(add);
        Transform fxHeal = FXSpawner.Instance.Spawn(FXSpawner.impactTrue, transform.position, transform.rotation);
        fxHeal.parent = transform;
        StartCoroutine(setFxRealParent(fxHeal));
    }

    IEnumerator setFxRealParent(Transform fxHeal)
    {
        yield return new WaitForSeconds(2f);
        fxHeal.parent = FXSpawner.Instance.Holder;
        FXSpawner.Instance.Despawn(fxHeal);
    }

    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.smokeOne;
    }

    public virtual void AddMaxHP()
    {
        this.hpMax++;
    }

    public virtual void SetHP(float hp, float hpMax)
    {
        this.playerDame.SetMaxHp(hpMax);
        this.playerDame.SetCurrentHp(hp);
        this.hp = hp;
        this.hpMax = hpMax;
    }

    public override void Reborn()
    {
        base.Reborn();
        this.playerDame.SetMaxHp(this.hpMax);
        this.playerDame.SetCurrentHp(this.hp);
    }
}
