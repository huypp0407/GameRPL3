using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : _MonoBehaviour
{
    [SerializeField] protected List<Transform> points;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPoint();
    }

    protected virtual void LoadPoint()
    {
        if (this.points.Count > 0) return;

        foreach(Transform point in transform)
        {
            this.points.Add(point);
        }
    }

    public virtual Transform GetRandom()
    {
        int rand = Random.Range(0, this.points.Count);
        while(Vector3.Distance(this.points[rand].position, PlayerCtrl.Instance.transform.position) < 15f)
        {
            rand = Random.Range(0, this.points.Count);
        }
        return this.points[rand];
    }

    public virtual List<Transform> GetRandomEnemy(int i)
    {
        return this.points.GetRange(0,i);
    }
}
