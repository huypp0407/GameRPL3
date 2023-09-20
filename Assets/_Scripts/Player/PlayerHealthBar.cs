using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : HealthBar
{
    
    private void FixedUpdate()
    {
        this.SetMaxHealth(PlayerCtrl.Instance.PlayerDamageReceiver.HpMax);
        this.SetHealth(PlayerCtrl.Instance.PlayerDamageReceiver.Hp);
    }
}
