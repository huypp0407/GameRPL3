using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FXSpawner : Spawner
{
    private static FXSpawner instance;
    public static FXSpawner Instance { get => instance; }
    public static string smokeOne = "Smoke_1";
    public static string impactOne = "Impact_1";
    public static string impactTrue = "Impact_2";
    public static string impactThree = "Impact_3";

    protected override void Awake()
    {
        base.Awake();
        if (FXSpawner.instance != null) return;
        FXSpawner.instance = this;
    }

    public virtual Transform SpawnFx(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = Spawn(prefabName, spawnPos, rotation);
        StartCoroutine(DespawnBytime(prefab));
        return prefab;
    }

    IEnumerator DespawnBytime(Transform transform)
    {
        yield return new WaitForSeconds(2f);
        if(transform != null)
            Despawn(transform);
    }
}
