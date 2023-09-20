using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class EnemyDamageReceive : DamageReceiver
{
    public EnemyCtrl enemyCtrl;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float delay = 3f;
    [SerializeField] protected bool canAdd = false;
    protected void FixedUpdate()
    {
        if (this.canAdd)
        {
            this.timer += Time.fixedDeltaTime;
            if (this.timer < this.delay) return;
            this.timer = 0;
            this.Add(1);
        }
    }

    protected virtual void CanAdd()
    {
        this.Add(1);
    }

    protected override void OnDead()
    {
        this.enemyCtrl.enemySpawner.Despawn(transform.parent);
        this.OnDeadFX();
        TextScore.Instance.UpdateScore();
        //DropItem
        this.OnDeadDropItem();
        this.enemyCtrl.enemyMove.isWalk = true;
    }

    protected virtual void OnDeadDropItem()
    {
        int currentLvel = MapLevel.Instance.LevelCurrent - 1;
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        ItemDropSpawner.Instance.Drop(this.enemyCtrl.EnemySO.upgradeLevels[currentLvel].dropList, dropPos, dropRot);
    }

    protected virtual void OnDeadFX()
    {
        string fxName = this.GetOnDeadFXName();
        Vector3 spawnPos = transform.position;
        Transform fxOnDead = FXSpawner.Instance.SpawnFx(fxName, spawnPos, transform.rotation);
        //fxOnDead.gameObject.SetActive(true);
    }

    public override void Reborn()
    {
        this.enemyCtrl.CanvasHealth.gameObject.SetActive(false);
        //transform.parent.DOScale(new Vector3(2, 2, 2), 2f);
        int currentLvel = MapLevel.Instance.LevelCurrent-1;
        this.hpMax = this.enemyCtrl.EnemySO.upgradeLevels[currentLvel].enemyHp;
        base.Reborn();
        this.enemyCtrl.CanvasHealth.Dame.SetMaxHp(this.hpMax);
        this.enemyCtrl.CanvasHealth.Dame.SetCurrentHp(this.hp);
    }

    public override void Deduct(float add)
    {
        base.Deduct(add);
        if (this.isDead) return;
        this.enemyCtrl.Animator.SetBool("isHit", false);
        this.enemyCtrl.CanvasHealth.Dame.gameObject.SetActive(true);
        this.canAdd = false;
        this.timer = 0;
        this.enemyCtrl.Animator.SetBool("isHit", true);
        StopAllCoroutines();
        StartCoroutine(this.StopAnimation());
    }

    IEnumerator StopAnimation()
    {
        yield return new WaitForSeconds(.4f);

        this.enemyCtrl.Animator.SetBool("isHit", false);
        yield return new WaitForSeconds(3.6f);
        this.enemyCtrl.CanvasHealth.Dame.gameObject.SetActive(false);
        this.canAdd = true;
    }

    protected virtual string GetOnDeadFXName()
    {
        return FXSpawner.smokeOne;
    }

}
