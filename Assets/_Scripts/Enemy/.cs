using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementAbstract : _MonoBehaviour
{
    [SerializeField] protected Vector3 targetPosition;
    
    [SerializeField] protected EnemyCtrl enemyCtrl;
    public EnemyCtrl EnemyCtrl => enemyCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadEnemyCtrl();
    }

    protected virtual void LoadEnemyCtrl()
    {
        if (this.enemyCtrl != null) return;
        this.enemyCtrl = transform.parent.GetComponent<EnemyCtrl>();
    }

    protected virtual void FixedUpdate()
    {
        this.GetTargetPosition();
    }

    protected virtual void GetTargetPosition()
    {
        this.targetPosition = PlayerCtrl.Instance.transform.position;
    }
}


