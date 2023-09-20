using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : _MonoBehaviour
{
    [SerializeField] protected float timer = 0;
    public float delay = 0.7f;
    public bool isShoot = true;
    public bool isAttack = false;
    public Transform bulletPos;

    private void FixedUpdate()
    {
        this.IsShooting();
        this.IsAttack();
    }

    public void IsAttack()
    {
        if (!isAttack) return;
        timer += Time.fixedDeltaTime;
        if (timer < delay) return;
        timer = 0;
        this.Attack();
    }

    protected virtual void IsShooting()
    {
        if (!isShoot) return;
        timer += Time.fixedDeltaTime;
        if (timer < delay) return;
        timer = 0;

        Vector3 spawnPos = transform.position;
        Quaternion rotation = transform.parent.rotation;
        this.SpawnBullet(bulletPos.position, rotation);
    }

    protected virtual void SpawnBullet(Vector3 spawnPos, Quaternion rotation)
    {
        //Override
    }

    public virtual void Attack()
    {
        //Override
    }
}
