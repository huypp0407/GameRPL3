using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInformCtrl : _MonoBehaviour
{
    //[SerializeField] protected Text txtPlayerName;
    [SerializeField] protected Text txtHP;
    [SerializeField] protected Text txtHPLevel;
    [SerializeField] protected Text txtDamage;
    [SerializeField] protected Text txtDamageLevel;
    [SerializeField] protected Text txtMapLevel;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTxtHP();
        this.LoadTxtHPLevel();
        this.LoadTxtDamage();
        this.LoadTxtDamageLevel();
        this.LoadTxtMapLevel();
    }

    protected virtual void LoadTxtHP()
    {
        if (this.txtHP != null) return;
        this.txtHP = GameObject.Find("UITxtPlayerHp").GetComponent<Text>();
    }

    protected virtual void LoadTxtHPLevel()
    {
        if (this.txtHPLevel != null) return;
        this.txtHPLevel = GameObject.Find("UITxtPlayerHpLevel").GetComponent<Text>();
    }

    protected virtual void LoadTxtDamage()
    {
        if (this.txtDamage != null) return;
        this.txtDamage = GameObject.Find("UITxtPlayerDamage").GetComponent<Text>();
    }

    protected virtual void LoadTxtDamageLevel()
    {
        if (this.txtDamageLevel != null) return;
        this.txtDamageLevel = GameObject.Find("UITxtPlayerDamageLevel").GetComponent<Text>();
    }

    protected virtual void LoadTxtMapLevel()
    {
        if (this.txtMapLevel != null) return;
        this.txtMapLevel = GameObject.Find("UITxtMapLevel").GetComponent<Text>();
    }

    public virtual void SetText()
    {
        this.txtHP.text = "Health Point: " + PlayerCtrl.Instance.PlayerDamageReceiver.Hp + " / " + PlayerCtrl.Instance.PlayerDamageReceiver.HpMax;
        //this.txtHPLevel.text = "HP Level: " + PlayerCtrl.Instance.Inventory.Items[0].upgradeLevel;
        //this.txtDamage.text = "Damage" 
        //this.txtHP.text = PlayerCtrl.Instance.PlayerDamageReceiver.Hp + " / " + PlayerCtrl.Instance.PlayerDamageReceiver.HpMax;
        this.txtMapLevel.text = "Map Level: " + MapLevel.Instance.LevelCurrent;
    }
}
