using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : _MonoBehaviour
{
    public PlayerCtrl playerCtrl;
    public float moveSpeed = 3.5f;
    [SerializeField] protected FixedJoystick fixedJoystick;
    //[SerializeField] protected Vector3 direction;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Transform target;
    public bool isWalk;

    protected override void LoadComponent()
    {
        base.LoadComponent();
    }
    
    private void FixedUpdate()
    {
        this.LookAtTarget();
        this.playerCtrl.Rigidbody.velocity = new Vector3(fixedJoystick.Horizontal * this.moveSpeed,
                                                         0,
                                                         fixedJoystick.Vertical * this.moveSpeed);
        float angle = Vector2.SignedAngle(new Vector2(transform.parent.forward.x, transform.parent.forward.z), Vector2.up);
        if (angle > 0) angle = ((float)Math.PI / 180) * (360 - angle);
        else angle = ((float)Math.PI / 180) * Mathf.Abs(angle);

        this.playerCtrl.Animator.SetFloat("xaxis", fixedJoystick.Horizontal * Mathf.Cos(angle) + fixedJoystick.Vertical * Mathf.Sin(angle));
        this.playerCtrl.Animator.SetFloat("yaxis", fixedJoystick.Horizontal * Mathf.Sin(angle) - fixedJoystick.Vertical * Mathf.Cos(angle));
    }

    protected virtual void LookAtTarget()
    {
        this.target = this.FindClosestEnemy();
        if(this.target != null)
        {
            Vector3 pos = this.target.position;
            pos.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(pos - transform.parent.position);
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.fixedDeltaTime*15f);
        }
            //transform.parent.LookAt(this.target.position, Vector3.up);
        else if(fixedJoystick.Horizontal != 0 || fixedJoystick.Vertical != 0) transform.parent.rotation = Quaternion.LookRotation(this.playerCtrl.Rigidbody.velocity);

    }

    protected virtual Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        float closestDis = Mathf.Infinity;

        Transform trans = null;

        foreach (GameObject enemy in enemies)
        {
            float currentDis = Vector3.Distance(transform.position, enemy.transform.position);
            if (currentDis < closestDis)
            {
                closestDis = currentDis;
                trans = enemy.transform;
            }
        }
        return trans;
    }
}
