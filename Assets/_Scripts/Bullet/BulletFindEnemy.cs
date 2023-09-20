using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFindEnemy : _MonoBehaviour
{
    [SerializeField] protected bool go = true;
    [SerializeField] protected bool findEnemy = false;
    [SerializeField] protected float timer = 0;
    [SerializeField] protected float moveSpeed = 20f;
    public Vector3 direction = new Vector3(0, 0, 1);

    [SerializeField] protected Transform enemyTarget;

    private void OnEnable()
    {
        this.timer = 0;
        this.go = true;
        StartCoroutine(BulletFollowEnemy());
    }

    IEnumerator BulletFollowEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        this.go = !go;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.parent.Translate(this.moveSpeed * Time.deltaTime * this.direction);
        if (!go)
        {
            this.enemyTarget = this.FindClosestEnemy();
            if (enemyTarget != null)
            {
                transform.parent.LookAt(enemyTarget);
            }
            else go = !go;
        }
    }

    protected virtual Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        float closestDis = Mathf.Infinity;

        Transform trans = null;

        foreach(GameObject enemy in enemies)
        {
            float currentDis = Vector3.Distance(transform.position, enemy.transform.position);
            if(currentDis > closestDis)
            {
                closestDis = currentDis;
                trans = enemy.transform;
            }
        }
        return trans;
    }
}