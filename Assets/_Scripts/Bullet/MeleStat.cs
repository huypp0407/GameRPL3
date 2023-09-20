using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleStat : MonoBehaviour
{
    public float damage;
    public DamageSender damageSender;
    public bool isAttack = false;

    protected virtual void Attack(string eventName)
    {
        if (eventName != "Attack") return;
        isAttack = true;
    }

    protected virtual void StopAttack(string eventName)
    {
        if (eventName != "StopAttack") return;
        isAttack = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
    }
}
