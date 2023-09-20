using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BtnChooseCharacter : BaseButton
{
    [SerializeField] protected UIItemInventory itemInventory;
    public UIItemInventory UIItemInventory => itemInventory;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.itemInventory != null) return;
        this.itemInventory = GetComponent<UIItemInventory>();
    }

    protected override void OnClick()
    {
        PlayerPrefs.SetString("Character", this.itemInventory.ItemName.text.ToString());
        UICharacter.Instance.Toggle();
    }
}
