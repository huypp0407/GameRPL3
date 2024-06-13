using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingCtrl : Shooter
{
    public EnemyCtrl enemyCtrl;

    protected virtual void OnEnable()
    {
        int currentLvel = MapLevel.Instance.LevelCurrent-1;
        this.delay = this.enemyCtrl.EnemySO.upgradeLevels[currentLvel].ememySpeed;
    }
}
