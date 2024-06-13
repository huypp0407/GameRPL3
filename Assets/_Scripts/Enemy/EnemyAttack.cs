using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyAttack : EnemyMovementAbstract
{
    private void OnEnable()
    {
        this.minDistance = 7f;
        this.enemyCtrl.Animator.SetFloat("walk", 1);
        this.speed = 2.5f;
        this.isWalk = true;
    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }
    private void FixedUpdate() {
      this.Move();
    }

    protected virtual void Move()
    {
        if (!isWalk) return;
        this.distance = Vector3.Distance(transform.parent.position, this.target.position);
       
        if (this.distance < this.minDistance)
        {
            StartCoroutine(Jump());
        }

        if (this.distance < 2f) {
          this.isUpdatePath = false;
            this.enemyCtrl.Animator.SetFloat("walk", 0);
            this.enemyCtrl.EnemyShooting.isAttack = true;
            transform.parent.LookAt(target);
            Vector3 directionAttack = (this.target.position - transform.position).normalized;
            this.enemyCtrl.Rigibody.velocity = Vector3.Lerp(enemyCtrl.Rigibody.velocity, new Vector3(directionAttack.x, this.enemyCtrl.Rigibody.velocity.y, directionAttack.z) * speed,Time.fixedDeltaTime*12);
            return;
        }
        
        this.enemyCtrl.Animator.SetFloat("walk", 1);
        this.enemyCtrl.EnemyShooting.isAttack = false;

        Vector3 direction = transform.parent.forward;
        this.enemyCtrl.Rigibody.velocity = Vector3.Lerp(enemyCtrl.Rigibody.velocity, new Vector3(direction.x, this.enemyCtrl.Rigibody.velocity.y, direction.z) * speed,Time.fixedDeltaTime*12);
        // this.enemyCtrl.Rigibody.velocity = Vector3.Lerp(enemyCtrl.Rigibody.velocity, new Vector3(direction.x, this.enemyCtrl.Rigibody.velocity.y, direction.z) * speed, Time.fixedDeltaTime * 12);
    }

    IEnumerator Jump()
    {
        this.minDistance = 0;
        this.enemyCtrl.Animator.SetFloat("walk", 0);
        Transform fx = FXSpawner.Instance.SpawnFx("Impact_4", this.target.position, transform.rotation);
        this.isWalk = false;
        this.enemyCtrl.EnemyLookAtTarget.target = fx;
        yield return new WaitForSeconds(1.5f);
        FXSpawner.Instance.Despawn(fx);
        this.enemyCtrl.Animator.SetTrigger("Jump");
        if(transform.parent != null) transform.parent.DOJump(fx.transform.position, 1, 1, .5f, snapping);
        yield return new WaitForSeconds(1f);
        this.enemyCtrl.EnemyLookAtTarget.target = PlayerCtrl.Instance.transform;
        this.speed = 3;
        this.isWalk = true;
        this.enemyCtrl.Animator.SetFloat("speed", 2);
    }

}
