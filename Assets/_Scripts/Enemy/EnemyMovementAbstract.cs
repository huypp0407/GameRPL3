using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAbstract : _MonoBehaviour
{
    public Transform target;
    public bool isWalk = true;
    public EnemyCtrl enemyCtrl;

    protected override void Start()
    {
        target = PlayerCtrl.Instance.transform;
    }
}
