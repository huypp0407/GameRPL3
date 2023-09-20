using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/Bullet")]
public class BulletSO : ScriptableObject
{
    public string bulletName;
    public float damage;
    public AudioClip bloodSplat;

    public void DamageUpgrade(float add)
    {
        this.damage += add;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
