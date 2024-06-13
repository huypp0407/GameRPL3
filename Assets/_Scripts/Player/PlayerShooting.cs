using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShooting : Shooter
{
    public PlayerCtrl playerCtrl;
    [SerializeField] protected static PlayerShooting instance;
    public static PlayerShooting Instance => instance;
    protected string currentWeapon = "Gun";

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
    
    public virtual void SetCurrentWeapon()
    {
      if(playerCtrl.Inventory.btnChange != null) Destroy(playerCtrl.Inventory.btnChange);
      if(UIButtonBom.Instance.IsOpen) UIButtonBom.Instance.Toggle();
      switch (currentWeapon) {
        case "AXE": 
          GetAxe();
          break; 
        case "boomerang":
          GetBoomerang();
          break;
        case "Gun":
          GetGun();
          break;
      }
    }
    
    public virtual void GetBomb()
    {
      playerCtrl.Inventory.CreateBtnChange();
      Transform weapon = this.GetWeapon("Bomb");
      if (weapon == null) return;
      this.playerCtrl.Animator.SetInteger("State",2);
      Vector3 pos = new Vector3(0.014f, 0.069f, 0.035f);
      Quaternion rot = Quaternion.Euler(167.404f, -49.20001f, -197.064f);
      StartCoroutine(ChangeWeapon(weapon, pos, rot, Vector3.one*0.3f));
      this.isAttack = false;
      this.isShoot = false;
    }
    
    public virtual void GetSmoke()
    {
      playerCtrl.Inventory.CreateBtnChange();
      Transform weapon = this.GetWeapon("Smoke");
      if (weapon == null) return;
      this.playerCtrl.Animator.SetInteger("State",2);
      Vector3 pos = new Vector3(0.081f, 0.086f, -0.007f);
      Quaternion rot = Quaternion.Euler(-59.575f, 69.149f, -78.178f);
      StartCoroutine(ChangeWeapon(weapon, pos, rot, Vector3.one*1.8f));
      this.isAttack = false;
      this.isShoot = false;
    }
    public virtual void GetAxe() {
      currentWeapon = "AXE";
        Transform weapon = this.GetWeapon("AXE");
        if (weapon == null) return;
        this.playerCtrl.Animator.SetInteger("State",1);
        Vector3 pos = new Vector3(0, 0, -.18f);
        Quaternion rot = Quaternion.Euler(90, -90, 90);
        StartCoroutine(ChangeWeapon(weapon, pos, rot, Vector3.one));
        this.isAttack = true;
        this.isShoot = false;
    }

    IEnumerator ChangeWeapon(Transform weapon, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        yield return new WaitForSeconds(0.3f);
        Transform gun = this.playerCtrl.handPos.transform.GetChild(0);
        gun.parent = this.playerCtrl.weaponPos.transform;
        gun.gameObject.SetActive(false);

        weapon.transform.parent = this.playerCtrl.handPos.transform;
        weapon.gameObject.SetActive(true);
        weapon.transform.localRotation = rot;
        weapon.transform.localPosition = pos;
        weapon.transform.localScale = scale;
    }

    public virtual void GetGun()
    {
      currentWeapon = "Gun";
        Transform weapon = this.GetWeapon("Gun");
        if (weapon == null) return;
        this.playerCtrl.Animator.SetInteger("State", 0);
        Vector3 pos = new Vector3(0.294f, 0.01f, -0.052f);
        Vector3 scale = new Vector3(0.15f, 0.15f, 0.15f);
        Quaternion rot = Quaternion.Euler(-24.348f, -101.764f, 92.357f);
        StartCoroutine(ChangeWeapon(weapon, pos, rot, scale));
        this.isAttack = false;
        this.isShoot = true;
        BulletSpawner.Instance.SetBullet();
    }
    
    public virtual void GetBoomerang()
    {
      currentWeapon = "boomerang";
      Transform weapon = this.GetWeapon("boomerang");
      if (weapon == null) return;
      this.playerCtrl.Animator.SetInteger("State", 0);
      Vector3 pos = new Vector3(0.262f, -0.009f, 0.042f);
      Vector3 scale = new Vector3(0.8f, 0.8f, 0.8f);
      Quaternion rot = Quaternion.Euler(6.749f, 66.991f, -73.885f);
      StartCoroutine(ChangeWeapon(weapon, pos, rot, scale));
      this.isAttack = false;
      this.isShoot = true;
      BulletSpawner.Instance.SetBoomerang();
    }

    protected virtual Transform GetWeapon(string weapon)
    {
        foreach (Transform tran in this.playerCtrl.weaponPos.transform)
        {
          if (tran.name == weapon) {
            tran.gameObject.SetActive(true);
            return tran;
          }
        }
        return null;
    }
    
    public void Throw(float throwForce, string typebomb)
    {
      throwForce = throwForce > 1f ? 1f : throwForce;
      Transform bomb = BulletSpawner.Instance.Spawn(typebomb, this.playerCtrl.handPos.transform.position, transform.rotation);
      Rigidbody rigi = bomb.GetComponentInChildren<Rigidbody>();
      Vector3 forceToAdd = PlayerCtrl.Instance.transform.forward * throwForce * 5 + PlayerCtrl.Instance.transform.up * 6;
      rigi.AddForce(forceToAdd, ForceMode.Impulse);
      playerCtrl.Inventory.DeductAbilities(typebomb);
    }
}
