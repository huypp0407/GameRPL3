using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnBulletImpart : BulletImpact
{
    public BoxCollider boxCollider;
    public Transform model;

    private void OnEnable()
    {
        this.boxCollider.enabled = true;
        this.model.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            StartCoroutine(Burn(other));
            VolumePost.Instance.StartFlicker(); ;
            this.model.gameObject.SetActive(false);
            this.boxCollider.enabled = false;
        }
    }

    IEnumerator Burn(Collider other)
    {
        int rand = Random.Range(20, 30);
        while(rand > 0)
        {
            this.allBulletCtrl.DamageSender.Send(other.transform);
            rand--;
            yield return new WaitForSeconds(0.5f);
        }
        BulletSpawner.Instance.Despawn(transform.parent);
        VolumePost.Instance.StopFlicker(); ;
    }
}
