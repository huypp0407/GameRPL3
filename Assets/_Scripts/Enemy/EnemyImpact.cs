using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpact : _MonoBehaviour
{
    public EnemyCtrl enemyCtrl;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            this.enemyCtrl.EnemyDamageSender.Send(other.transform);
        }
    }
}
