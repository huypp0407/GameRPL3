using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Map", menuName = "ScriptableObjects/Map")]
public class MapSO : ScriptableObject
{
    public List<LevelMap> levelMaps;
}

[Serializable]
public class LevelMap
{
    public List<EnemyTimer> enemyTimers;
}


[Serializable]
public class EnemyTimer
{
    public float timer;
    public int count;
}
