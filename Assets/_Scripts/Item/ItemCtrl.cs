using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCtrl : _MonoBehaviour
{
    [SerializeField] protected ItemInventory itemInventory;
    public ItemInventory ItemInventory => itemInventory;

    [SerializeField] protected ItemPickupable itemPickupable;

    public ItemDropSpawner itemSpawner;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadItemInventory();
        this.LoadItemPickupable();
    }

    public void SetUp()
    {
        this.itemPickupable.itemCtrl = this;
    }

    protected virtual void LoadItemPickupable()
    {
        if (this.itemPickupable != null) return;
        this.itemPickupable = GetComponentInChildren<ItemPickupable>();
    }

    public virtual void SetItemInventory(ItemInventory itemInventory)
    {
        this.itemInventory = itemInventory.Clone();
    }

    protected virtual void LoadItemInventory()
    {
        if (this.itemInventory.itemProfileSO != null) return;
        ItemCode itemCode = ItemCodeParse.FromString(transform.name);
        ItemProfileSO itemProfileSO = ItemProfileSO.FindByItemCode(itemCode);
        this.itemInventory.itemProfileSO = itemProfileSO;
        this.ResetItem();
    }

    protected virtual void ResetItem()
    {
        this.itemInventory.itemCount = 1;
        this.itemInventory.upgradeLevel = 0;

    }
}
