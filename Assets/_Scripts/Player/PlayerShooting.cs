using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : Shooter
{
    public PlayerCtrl playerCtrl;
    [SerializeField] protected static PlayerShooting instance;
    public static PlayerShooting Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerShooting.instance != null) return;
        PlayerShooting.instance = this;
    }

    protected override void SpawnBullet(Vector3 spawnPos, Quaternion rotation)
    {
        this.playerCtrl.Animator.SetTrigger("Attack");
        Transform newBullet = BulletSpawner.Instance.Spawn(BulletSpawner.bullet, spawnPos, rotation);
    }

    public override void Attack()
    {
        this.playerCtrl.Animator.SetTrigger("Attack");
    }

    public virtual void GetAxe()
    {
        Transform weapon = this.GetWeapon("AXE");
        if (weapon == null) return;
        this.playerCtrl.Animator.SetInteger("State",1);
        StartCoroutine(ChangeAxe(weapon));
        this.isAttack = true;
        this.isShoot = false;
    }

    IEnumerator ChangeAxe(Transform weapon)
    {
        yield return new WaitForSeconds(0.3f);
        Transform gun = this.playerCtrl.handPos.transform.GetChild(0);
        gun.parent = this.playerCtrl.weaponPos.transform;
        gun.localRotation = Quaternion.Euler(0, -90, 90);
        gun.localPosition = new Vector3(-0.27f, -0.06f, 0.088f);
        gun.localScale = new Vector3(0.18f, 0.18f, 0.18f);

        weapon.transform.parent = this.playerCtrl.handPos.transform;
        weapon.transform.localRotation = Quaternion.Euler(90, -90, 90);
        weapon.transform.localPosition = new Vector3(0, 0, -.18f);
        weapon.transform.localScale = Vector3.one;
    }

    public virtual void GetGun()
    {
        Transform weapon = this.GetWeapon("Gun");
        if (weapon == null) return;
        this.playerCtrl.Animator.SetInteger("State", 0);
        StartCoroutine(ChangeGun(weapon));
        this.isAttack = false;
        this.isShoot = true;
        BulletSpawner.Instance.SetBullet();
    }

    IEnumerator ChangeGun(Transform weapon)
    {
        yield return new WaitForSeconds(0.3f);
        Transform axe = this.playerCtrl.handPos.transform.GetChild(0);
        axe.parent = this.playerCtrl.weaponPos.transform;
        axe.localRotation = Quaternion.Euler(90, 0, 90);
        axe.localPosition = new Vector3(0.09f, -0.04f, -0.082f);
        axe.localScale = new Vector3(1,1,1);

        weapon.parent = this.playerCtrl.handPos.transform;
        weapon.localRotation = Quaternion.Euler(-24.348f, -101.764f, 92.357f);
        weapon.localPosition = new Vector3(0.294f, 0.01f, -0.052f);
        weapon.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    }

    protected virtual Transform GetWeapon(string weapon)
    {
        foreach (Transform tran in this.playerCtrl.weaponPos.transform)
        {
            if (tran.name == weapon)
                return tran;
        }
        return null;
    }
}
