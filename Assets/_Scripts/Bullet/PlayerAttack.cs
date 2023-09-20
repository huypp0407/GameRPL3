using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MeleStat
{
    public PlayerCtrl playerCtrl;

    private void Awake()
    {
        this.playerCtrl.AnimationEvent.OnCustomEvent += this.Attack;
        this.playerCtrl.AnimationEvent.OnCustomEvent += this.StopAttack;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" || other.name == "ItemLooter" || !isAttack) return;
        this.damageSender.Send(other.transform);
        isAttack = false;
    }
}
