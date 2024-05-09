using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DailyBonusSpawner : Spawner
{
    private static DailyBonusSpawner instance;
    public static DailyBonusSpawner Instance { get => instance; }
    public static string itemNormal = "UIInvItem";

    [SerializeField] protected UIInventoryCtrl inventoryCtrl;
    public UIInventoryCtrl UIInventoryCtrl => inventoryCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (DailyBonusSpawner.instance != null) return;
        DailyBonusSpawner.instance = this;
    }

    protected override void LoadHolder()
    {
        this.LoadUIInventoryCtrl();

        if (this.holder != null) return;
        this.holder = this.inventoryCtrl.Content;
    }

    protected virtual void LoadUIInventoryCtrl()
    {
        if (this.inventoryCtrl != null) return;
        this.inventoryCtrl = transform.parent.GetComponent<UIInventoryCtrl>();
    }

    public virtual void ClearItems()
    {
        foreach(Transform item in holder)
        {
            this.Despawn(item);
        }
    }

    public virtual void SpawnItem(ItemInventory item)
    {
        Transform uiItem = this.Spawn("UIInvItem", transform.position, transform.rotation);
        uiItem.localScale = new Vector3(1, 1, 1);
        UIItemInventory itemInventory = uiItem.GetComponent<UIItemInventory>();
        itemInventory.ShowItem(item);
    }
}
