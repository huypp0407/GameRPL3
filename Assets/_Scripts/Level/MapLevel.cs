using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLevel : _MonoBehaviour
{
    [SerializeField] private static MapLevel instance;
    public static MapLevel Instance => instance;

    [SerializeField] protected bool canSpawnBoss = true;
    [SerializeField] protected int levelCurrent = 1;
    [SerializeField] protected int levelMax = 99;
    public int LevelCurrent => levelCurrent;
    public int LevelMax => levelMax;

    public virtual void LevelSet(int newLevel)
    {
        this.levelCurrent = newLevel;
        this.LimitLevel();
    }

    protected virtual void LimitLevel()
    {
        if (this.levelCurrent > this.levelMax) this.levelCurrent = this.levelMax;
        if (this.levelCurrent < 1) this.levelCurrent = 1;
    }

    protected override void Awake()
    {
        base.Start();
        if (MapLevel.instance != null) return;
        MapLevel.instance = this;
    }

    public virtual void LevelUp()
    {
        this.levelCurrent++;
        this.LimitLevel();
        PlayerCtrl.Instance.PlayerDamageReceiver.AddMaxHP();
        PlayerCtrl.Instance.PlayerDamageReceiver.Add(1);
        
        BulletSO bullet_3 = Resources.Load<BulletSO>("bullet/bullet_3");
        bullet_3.DamageUpgrade(0.2f);
        this.canSpawnBoss = true;
    }

    public virtual void Leveling()
    {
        if (!this.canSpawnBoss) return;
        EnemySpawner.Instance.canSpawn = false;

        Vector3 pos = PlayerCtrl.Instance.transform.position;
        pos.z += 10;
        EnemySpawner.Instance.Spawn("Boss", pos, transform.rotation);
        TextScore.Instance.canUpgradeScore = false;
        this.canSpawnBoss = false;
    }

    public virtual void SetCurrentLevel(int level)
    {
        this.levelCurrent = level;
    }
}
