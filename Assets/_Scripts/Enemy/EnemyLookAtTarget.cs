using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLookAtTarget : EnemyMovementAbstract
{
    protected virtual void FixedUpdate()
    {
        this.LookAtTarget();
    }

    protected virtual void LookAtTarget()
    {
        transform.parent.LookAt(this.target.position, Vector3.up);
    }
}
