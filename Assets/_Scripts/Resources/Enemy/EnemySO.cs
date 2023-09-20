using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName = "ScriptableObjects/Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public EnemyCode enemyCode = EnemyCode.NoItem;
    public List<EnemyInform> upgradeLevels;
    public List<AudioClip> walkSteps;
    public AudioClip punch;

    public virtual AudioClip WalkStep()
    {
        int rand = Random.Range(0, this.walkSteps.Count);
        return this.walkSteps[rand];
    }
}
