using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AbilitySummon : BaseAbility
{
    [Header("Ability Summon")]

    [SerializeField] protected int enemyLimit = 5;

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.Summoning();
        //this.ClearDeadMinion();
    }

    protected virtual void Summoning()
    {
        if (!this.isReady) return;
        this.abilities.enemyCtrl.Animator.SetTrigger("isSpawn");
        this.Summon();
    }

    //protected virtual void ClearDeadMinion()
    //{
    //    foreach(Transform enemy in this.enemies)
    //    {
    //        if(enemy.gameObject.activeSelf == false)
    //        {
    //            this.enemies.Remove(enemy);
    //            return;
    //        }
    //    }
    //}

    protected virtual void Summon()
    {
        this.Active();
        var spawnPos = this.abilities.enemyCtrl.SpawnPoints.GetRandomEnemy(enemyLimit);
        for (int i=0; i<enemyLimit; i++)
        {
            FXSpawner.Instance.SpawnFx("Impact_5", spawnPos[i].position, spawnPos[i].rotation);
            Transform enemyPrefab = this.abilities.enemyCtrl.enemySpawner.RandomPrefab();
            Transform enemy = this.abilities.enemyCtrl.enemySpawner.Spawn(enemyPrefab, spawnPos[i].position, spawnPos[i].rotation);
            enemy.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            StartCoroutine(ScaleEnemy(enemy));
        }
    }

    IEnumerator ScaleEnemy(Transform enemy)
    {
        enemy.DOScale(Vector3.one, 1.5f);
        EnemyCtrl enemyCtrl = enemy.GetComponent<EnemyCtrl>();
        enemyCtrl.enemyMove.isWalk = false;
        if(enemy.name == "Enemy_1") enemyCtrl.EnemyShooting.isAttack = false;
        if(enemy.name == "Enemy_2") enemyCtrl.EnemyShooting.isShoot = false;
        yield return new WaitForSeconds(2f);
        enemyCtrl.enemyMove.isWalk = true;
        if (enemy.name == "Enemy_1") enemyCtrl.EnemyShooting.isAttack = true;
        if (enemy.name == "Enemy_2") enemyCtrl.EnemyShooting.isShoot = true;
    }
}
