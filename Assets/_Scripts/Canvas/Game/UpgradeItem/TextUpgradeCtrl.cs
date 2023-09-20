using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextUpgradeCtrl : _MonoBehaviour
{
    //[SerializeField] protected Text txtPlayerName;
    [SerializeField] protected Text txtHPLevel;
    [SerializeField] protected Text txtDamageLevel;
    [SerializeField] protected Text txtAttackSpeedLevel;

    public virtual void SetText(int index)
    {
        switch (index)
        {
            case 0:
                this.txtHPLevel.text = "HP Level: " + PlayerCtrl.Instance.Inventory.ItemsEquipment[0].upgradeLevel + " / 10";
                break;
            case 1:
                this.txtDamageLevel.text = "Damage Level: " + PlayerCtrl.Instance.Inventory.ItemsEquipment[1].upgradeLevel + " / 10";
                break;
            default:
                this.txtAttackSpeedLevel.text = "Attack Speed Level: " + PlayerCtrl.Instance.Inventory.ItemsEquipment[2].upgradeLevel + " / 10";
                break;
        }
    }

    public virtual void WarningUpgrade(int index)
    {
        StopAllCoroutines();
        switch (index)
        {
            case 0:
                StartCoroutine(WarningText(this.txtHPLevel));
                break;
            case 1:
                StartCoroutine(WarningText(this.txtDamageLevel));
                break;
            default:
                StartCoroutine(WarningText(this.txtAttackSpeedLevel));
                break;
        }
    }

    public float duration = 0.5f;

    IEnumerator WarningText(Text text)
    {
        text.color = Color.red;
        Tween myTween = text.transform.DOShakePosition(this.duration, 5, 10, 0, false, true, ShakeRandomnessMode.Harmonic).SetUpdate(true);
        yield return myTween.WaitForCompletion();
        text.color = Color.black;

    }
}
