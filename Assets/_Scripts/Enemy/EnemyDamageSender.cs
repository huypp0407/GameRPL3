using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageSender : DamageSender
{
    public EnemyCtrl enemyCtrl;

    private void OnEnable()
    {
        int currentLvel = MapLevel.Instance.LevelCurrent-1;
        this.damage = this.enemyCtrl.EnemySO.upgradeLevels[currentLvel].ememyDamage;
    }
}
