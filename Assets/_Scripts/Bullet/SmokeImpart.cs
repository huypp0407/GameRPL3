using System.Collections;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class SmokeImpart : BulletImpact {
    
    protected bool isTrigger = false;
    [SerializeField] private GameObject Model;
    private void OnEnable() {
      isTrigger = false;
      Model.gameObject.SetActive(true);
      // Invoke(nameof(BombSendDamge),4f);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (Regex.Match(other.name, "Enemy.*").Success || Regex.Match(other.name, "Plane.*").Success) {
          this.Model?.SetActive(false);
          BombSendDamge();
        }
    }

    protected async void BombSendDamge() {
      if (!isTrigger) {
        Transform fxSmoke = FXSpawner.Instance.SpawnFx("SmokeFX", transform.position, transform.rotation);
        isTrigger = true;
        Collider[] enemy = Physics.OverlapBox(transform.position, transform.localScale*2);
        foreach(var e in enemy) {
          EnemyCtrl enemyCtrl = e.GetComponent<EnemyCtrl>();
          if(enemyCtrl == null) continue;
          enemyCtrl.EnemyLookAtTarget.gameObject.SetActive(false);
          enemyCtrl.enemyMove.gameObject.SetActive(false);
          StartCoroutine(ContinueMove(enemyCtrl, fxSmoke));
        }
      }
    }
    
    IEnumerator ContinueMove(EnemyCtrl enemyCtrl, Transform fxSmoke)
    {
      yield return new WaitUntil(() => !fxSmoke.gameObject.activeSelf);
      enemyCtrl.EnemyLookAtTarget.gameObject.SetActive(true);
      enemyCtrl.enemyMove.gameObject.SetActive(true);
      yield return new WaitForSeconds(1f);
      this.allBulletCtrl.bulletSpawner.Despawn(transform.parent);
    }
}
