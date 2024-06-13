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
        this.distance = Vector3.Distance(transform.parent.position, this.target.position);
        if (this.distance < this.minDistanceShoot) {
          this.enemyCtrl.EnemyShooting.isShoot = true;
          this.enemyCtrl.Animator.SetBool("isWalk", false);
          this.isWalk = false;
          this.isUpdatePath = false;
          this.enemyCtrl.Rigibody.velocity = new Vector3(0, this.enemyCtrl.Rigibody.velocity.y, 0) * 2f;
          transform.parent.LookAt(target);
          return;
        } else {
          this.enemyCtrl.EnemyShooting.isShoot = false;
          this.enemyCtrl.Animator.SetBool("isWalk", true);
          isWalk = true;
          isUpdatePath = true;
        }

        Vector3 direction = transform.parent.forward;
        this.enemyCtrl.Rigibody.velocity = Vector3.Lerp(enemyCtrl.Rigibody.velocity, new Vector3(direction.x, this.enemyCtrl.Rigibody.velocity.y, direction.z) * speed,Time.fixedDeltaTime*12);
    }
}
