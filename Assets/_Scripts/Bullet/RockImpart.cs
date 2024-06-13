using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class RockImpart : BulletImpact {
    
  [SerializeField] private GameObject Model;
  protected bool isTrigger = false;
  [SerializeField] protected Rigidbody _rigidbody;
  private void OnEnable() {
    isTrigger = false;
    _rigidbody.velocity = Vector3.zero;
  }

  protected virtual void OnTriggerEnter(Collider other)
  {
      RockSendDamge(other);
  }

  protected void RockSendDamge(Collider other) {
    if (!isTrigger) {
      Transform fxDamage = FXSpawner.Instance.SpawnFx("BombFX", transform.position, transform.rotation);
      isTrigger = true;
      Collider[] enemy = Physics.OverlapBox(transform.position, transform.localScale);
      foreach(var e in enemy)
      {
        if(Regex.Match(e.name, "Player.*").Success)
          this.allBulletCtrl.DamageSender.Send(e.transform);
      }
      this.allBulletCtrl.bulletSpawner.Despawn(transform.parent);
    }
  }
}
