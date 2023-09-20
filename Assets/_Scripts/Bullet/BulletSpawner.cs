using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (BulletSpawner.instance == null) BulletSpawner.instance = this;
        this.SetUp();
    }

    protected virtual void SetUp()
    {
        foreach (var prefab in prefabs)
        {
            AllBulletCtrl bullet = prefab.GetComponent<AllBulletCtrl>();
            bullet.bulletSpawner = this;
            bullet.SetUp();
        }
    }

    private void FixedUpdate()
    {
        this.DespawnBullet();
    }

    public static string bullet = "Bullet_1";
    public static float speed = 0;

    public virtual void SetBullet()
    {
        BulletSpawner.bullet = "Bullet_1";
        PlayerShooting.Instance.delay = 0.4f;
    }

    public virtual void SetBoomerang()
    {
        BulletSpawner.bullet = "Boomerang";
        PlayerShooting.Instance.delay = 2f;
    }

    public virtual void SetAbilityBullet()
    {
        BulletSpawner.bullet = "Bullet_4";
        PlayerShooting.Instance.delay = 1.4f;
    }

    protected virtual void DespawnBullet()
    {
        foreach(Transform bullet in holder)
        {
            if (!bullet.gameObject.activeSelf) continue;
            if(Vector3.Distance(PlayerCtrl.Instance.transform.position, bullet.position) >= 100f)
            {
                Despawn(bullet.transform);
            }
        }
    }

    public void DespawnAllBullet()
    {
        foreach (Transform bullet in holder)
        {
            if (!bullet.gameObject.activeSelf) continue;
                Despawn(bullet.transform);
        }
    }
}
