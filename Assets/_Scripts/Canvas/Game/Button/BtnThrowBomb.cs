using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnThrowBomb : BaseButton {
    protected override void OnClick()
    {
        
    }
    // [Header("References")]
    public Transform cam;
    public Transform attackPoint;
    public GameObject objectToThrow;
    //
    // [Header("Settings")]
    // public int totalThrows;
    // public float throwCooldown;
    //
    // [Header("Throwing")]
    // public KeyCode throwKey = KeyCode.Mouse0;
    public float throwForce;
    public float throwUpwardForce;

    protected bool isThrow;
    //
    bool readyToThrow;
    //
    // private void Start()
    // {
    //   readyToThrow = true;
    // }
    //

    private void FixedUpdate() {
      if (isThrow) {
        throwForce += Time.fixedDeltaTime;
      }
    }

    private void Throw()
    {
      throwForce = throwForce > 1f ? 1f : throwForce;
      // readyToThrow = false;
      
      // instantiate object to throw
      GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);
      projectile.gameObject.SetActive(true);
      Debug.LogError($"HUYPP :: Throw :: {projectile.name}");
      
      
      // get rigidbody component
      Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
      
      // calculate direction
      Vector3 forceDirection = cam.transform.forward;
      Debug.LogError($"HUYPP :: Throw :: forceDirection {forceDirection}");
      
      RaycastHit hit;
      
      // if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
      // {
      //   forceDirection = (hit.point - attackPoint.position).normalized;
      // }
      
      // add force
      Vector3 forceToAdd = PlayerCtrl.Instance.transform.forward * throwForce * 5 + PlayerCtrl.Instance.transform.up * throwUpwardForce;
      Debug.LogError($"HUYPP :: Throw :: forceToAdd {forceToAdd}");

      
      projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
      
      // implement throwCooldown
      // Invoke(nameof(ResetThrow), throwCooldown);
    }

    public void OnClickDownThrow() {
      throwForce = 0;
      isThrow = true;
    }
    
    public void OnClickUpThrow() {
      isThrow = false;
      this.Throw();
    }
}
