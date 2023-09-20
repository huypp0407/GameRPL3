using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWarp : BaseAbility
{
    [Header("AbilityWarp")]

    [SerializeField] protected bool isWarping = false;
    [SerializeField] protected Vector3 warpDirection;
    [SerializeField] protected float warpSpeed = 0.1f;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.delay = 10f;
    }

    protected override void Update()
    {
        base.Update();
        this.CheckWarp();
    }

    protected virtual void CheckWarp()
    {
        if (!this.isReady) return;
        if (this.isWarping) return;
        if (this.pressed == true) this.Warp();
    }

    protected virtual void Warp()
    {
        this.warpDirection = PlayerCtrl.Instance.transform.forward;
        this.warpDirection.y = 0;
        this.Warping();
    }

    protected virtual void Warping()
    {
        this.isWarping = true;
        this.WarpFinish();
    }

    protected virtual void WarpFinish()
    {
        this.MoveObj();
        this.warpDirection.Set(0, 0, 0);
        this.isWarping = false;
        this.Active();
    }

    protected virtual void MoveObj()
    {
        Transform obj = PlayerCtrl.Instance.transform;
        Vector3 newPos = obj.position + warpDirection * 2f;
        obj.position = newPos;
    }
}
