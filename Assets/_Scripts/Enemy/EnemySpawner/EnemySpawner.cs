using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance { get => instance; }

    [SerializeField] protected SpawnPoints enemySpawnPoints;
    public SpawnPoints EnemySpawnPoints { get => enemySpawnPoints; }

    [SerializeField] protected MapSO mapSO;
    public MapSO MapSO => mapSO;

    [SerializeField] protected float randomDelay = 0f;
    [SerializeField] protected float randomTimer = 0f;
    [SerializeField] protected float timer = 0f;
    [SerializeField] protected float timerDelay = 1;
    [SerializeField] protected int index = 0;
    public int randomLimit = 0;
    public bool canSpawn = true;

    public static string enemyOne = "Enemy_2";

    protected override void Awake()
    {
        base.Awake();
        if (EnemySpawner.instance == null) EnemySpawner.instance = this;
        
        this.SetUp();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawnPoints();
        this.LoadMapSO();
    }

    private void FixedUpdate()
    {
        this.IncreaseEnemy();
        this.EnemySpawning();
        this.DespawnEnemy();
    }

    protected virtual void SetUp()
    {
        foreach(var prefab in prefabs)
        {
            var enemy = prefab.GetComponent<EnemyCtrl>();
            enemy.enemySpawner = this;
            enemy.SetUp();
        }
    }

    protected virtual void IncreaseEnemy()
    {
        if (this.index >= this.mapSO.levelMaps[MapLevel.Instance.LevelCurrent - 1].enemyTimers.Count)
        {
          if (this.CheckEnemyCount())
            MapLevel.Instance.Leveling();
          return;
        }
        if (!canSpawn) return;
        timer += Time.fixedDeltaTime;
        if (timer < this.timerDelay) return;
        
        this.randomLimit = this.mapSO.levelMaps[MapLevel.Instance.LevelCurrent - 1].enemyTimers[index].count;
        this.timerDelay = this.mapSO.levelMaps[MapLevel.Instance.LevelCurrent - 1].enemyTimers[index].timer;

        this.index++;

    }

    protected virtual bool CheckEnemyCount()
    {
        foreach(Transform enemy in holder)
        {
            if (enemy.gameObject.activeSelf) return false;
        }

        return true;
    }

    protected virtual void LoadMapSO()
    {
        if (this.mapSO != null) return;
        string resPath = "MapLevel/Map";
        this.mapSO = Resources.Load<MapSO>(resPath);
    }

    protected virtual void LoadSpawnPoints()
    {
        if (this.enemySpawnPoints != null) return;
        this.enemySpawnPoints = Transform.FindObjectOfType<SpawnPoints>();
    }

    protected virtual void EnemySpawning()
    {
        if (!canSpawn) return;

        //if (this.RandomReachLimit()) return;
        if (randomLimit < 1) return;
        this.randomTimer += Time.fixedDeltaTime;
        if (this.randomTimer < this.randomDelay) return;
        this.randomTimer = 0;

        Transform randPoint = this.enemySpawnPoints.GetRandom();
        Vector3 pos = randPoint.position;
        Quaternion rot = transform.rotation;

        string prefab = this.RandomPrefab().name;
        this.Spawn(prefab, pos, rot);
        this.randomLimit--;
    }

    protected virtual bool RandomReachLimit()
    {
        int currentEnemy = this.SpawnedCount;
        return currentEnemy >= this.randomLimit;
    }

    protected virtual void DespawnEnemy()
    {
        foreach (Transform enemy in holder)
        {
            if (!enemy.gameObject.activeSelf) continue;
            if (Vector3.Distance(PlayerCtrl.Instance.transform.position, enemy.position) >= 70f)
            {
                Despawn(enemy.transform);
            }
        }
    }
}
