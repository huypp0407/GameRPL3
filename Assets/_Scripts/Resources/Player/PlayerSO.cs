using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Player", menuName = "ScriptableObjects/Player")]
public class PlayerSO : ScriptableObject
{
    public List<AudioClip> walkSteps;

    public virtual AudioClip WalkStep()
    {
        int rand = Random.Range(0, this.walkSteps.Count);
        return this.walkSteps[rand];
    }
}
