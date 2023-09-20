using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : _MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 30f;
    public Vector3 direction = new Vector3(0, 0, 1);

    private void Update()
    {
        transform.parent.Translate(this.moveSpeed * Time.deltaTime * this.direction);
    }
}
