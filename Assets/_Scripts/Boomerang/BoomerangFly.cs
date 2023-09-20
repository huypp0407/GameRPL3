using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangFly : _MonoBehaviour
{
    [SerializeField] protected bool go = true;
    [SerializeField] protected Vector3 currentTarget;
    public AllBulletCtrl allBulletCtrl;

    private void OnEnable()
    {
        this.currentTarget = PlayerCtrl.Instance.transform.forward;
        this.timer = 0;
        this.go = true;
        StartCoroutine(BoomerangReturn());
    }

    public float timer = 0;

    IEnumerator BoomerangReturn()
    {
        yield return new WaitForSeconds(1.05f);
        this.go = !go;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.parent.Rotate(0, Time.deltaTime * 500, 0);

        if (go)
        {
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, transform.position + currentTarget * 2f, Time.deltaTime * 10);
        }
        if (!go)
        {
            this.MoveReturn();
            if (timer >= 1f && transform.parent.position == PlayerCtrl.Instance.transform.position) this.allBulletCtrl.bulletSpawner.Despawn(transform.parent);
        }
    }

    void MoveReturn()
    {
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, PlayerCtrl.Instance.transform.position, Time.deltaTime * 10);
    }
}
