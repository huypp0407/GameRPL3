using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EnemyMovementAbstract
{
    [SerializeField] protected float speed = 3.5f;
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected float minDistance = 4f;
    [SerializeField] protected float minDistanceShoot = 7.5f;

    protected virtual void FixedUpdate()
    {
        this.Moving();
    }

    protected virtual void Moving()
    {
        if (!isWalk) return;
        this.distance = Vector3.Distance(transform.parent.position, this.target.position);
        if (this.distance < this.minDistanceShoot)
            this.enemyCtrl.EnemyShooting.isShoot = true;
        else
            this.enemyCtrl.EnemyShooting.isShoot = false;
        if (this.distance <= this.minDistance)
        {
            this.enemyCtrl.Animator.SetBool("isWalk", false);
            this.enemyCtrl.Rigibody.velocity = new Vector3(0, this.enemyCtrl.Rigibody.velocity.y, 0) * 2f;
            return;
        }
        this.enemyCtrl.Animator.SetBool("isWalk", true);

        Vector3 direction = (this.target.position - transform.position).normalized;
        this.enemyCtrl.Rigibody.velocity = Vector3.Lerp(enemyCtrl.Rigibody.velocity, new Vector3(direction.x, this.enemyCtrl.Rigibody.velocity.y, direction.z) * speed,Time.fixedDeltaTime*12);
    }
}
