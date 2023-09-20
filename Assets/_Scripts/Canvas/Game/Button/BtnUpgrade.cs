using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class BtnUpgrade : BaseButton
{
    protected override void OnClick()
    {
        int index;
        switch (transform.parent.name)
        {
            case "Hp":
                index = 0;
                break;
            case "Damage":
                index = 1;
                break;
            default:
                index = 2;
                break;
        }

        this.Upgrade(index);
    }

    protected virtual void Upgrade(int index)
    {
        if (PlayerCtrl.Instance.Inventory.ItemUpgrade.UpgradeItem(index))
        {
            UIUpgrade.Instance.UIUpgradeCtrl.TextUpgradeCtrl.SetText(index);
            UIUpgrade.Instance.SpawnIngredient(index);
        } else UIUpgrade.Instance.UIUpgradeCtrl.TextUpgradeCtrl.WarningUpgrade(index);
    }
}
