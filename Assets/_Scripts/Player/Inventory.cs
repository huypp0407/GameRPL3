using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : _MonoBehaviour
{
    [SerializeField] protected int maxSlot = 70;
    [SerializeField] protected List<ItemInventory> items;
    public List<ItemInventory> Items => items;

    [SerializeField] protected List<ItemInventory> itemsEquipment;
    public List<ItemInventory> ItemsEquipment => itemsEquipment;
    [SerializeField] protected ItemUpgrade itemUpgrade;
    public ItemUpgrade ItemUpgrade => itemUpgrade;
    [SerializeField] protected ItemDrop itemDrop;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadItemUpgrade();
        this.LoadItemDrop();
    }

    protected virtual void LoadItemUpgrade()
    {
        if (this.itemUpgrade != null) return;
        this.itemUpgrade = GetComponentInChildren<ItemUpgrade>();
    }

    protected virtual void LoadItemDrop()
    {
        if (this.itemDrop != null) return;
        this.itemDrop = GetComponentInChildren<ItemDrop>();
    }

    protected override void Awake()
    {
        base.Awake();
        this.SetUp();
    }

    protected virtual void SetUp()
    {
        this.itemUpgrade.inventory = this;
        this.itemDrop.inventory = this;
    }

    public virtual bool AddItem(ItemInventory itemInventory)
    {
        int addCount = itemInventory.itemCount;
        ItemProfileSO itemProfileSO = itemInventory.itemProfileSO;
        ItemCode itemCode = itemProfileSO.itemCode;
        ItemType itemType = itemProfileSO.itemType;

        if (itemType == ItemType.Equiment || itemType == ItemType.Clothing) return AddEquipment(itemInventory);
        return AddItem(itemCode, addCount);
    }

    public virtual bool AddEquipment(ItemInventory itemInventory)
    {
        if (this.IsIventoryFull()) return false;
        itemInventory = itemInventory.Clone();
        this.items.Add(itemInventory);
        return true;
    }

    public virtual bool AddItem(ItemCode itemCode, int addCount)
    {
        ItemProfileSO itemProfileSO = ItemProfileSO.FindByItemCode(itemCode);

        int addRemain = addCount;
        int newcount;
        int itemMaxStack;
        int addMore;

        ItemInventory itemExist;

        for(int i=0; i< this.maxSlot; i++)
        {
            itemExist = this.GetItemNotFullStack(itemCode);
            if(itemExist == null)
            {
                if (this.IsIventoryFull()) return false;
                itemExist = this.CreateEmptyItem(itemProfileSO);
                this.items.Add(itemExist);
            }

            newcount = itemExist.itemCount + addRemain;

            itemMaxStack = this.GetMaxStack(itemExist);
            if(newcount > itemMaxStack)
            {
                addMore = itemMaxStack - itemExist.itemCount;
                newcount = itemExist.itemCount + addMore; ;
                addRemain -= addMore;
            }
            else
            {
                addRemain -= newcount;
            }

            itemExist.itemCount = newcount;
            if (addRemain < 1) break;
        }
        return true;
    }

    protected virtual bool IsIventoryFull()
    {
        if (this.items.Count >= this.maxSlot) return true;
        return false;
    }

    protected virtual int GetMaxStack(ItemInventory itemInventory)
    {
        if (itemInventory == null) return 0;
        return itemInventory.maxStack;
    }

    protected virtual ItemProfileSO GetItemProfile(ItemCode itemCode)
    {
        var profiles = Resources.LoadAll("ItemProfiles", typeof(ItemProfileSO));
        foreach (ItemProfileSO profile in profiles)
        {
            if (profile.itemCode != itemCode) continue;
            return profile;
        }
        return null;
    }

    protected virtual ItemInventory GetItemNotFullStack(ItemCode itemCode)
    {
        foreach(ItemInventory itemInventory in this.items)
        {
            if (itemCode != itemInventory.itemProfileSO.itemCode) continue;
            if (this.IsFullStack(itemInventory)) continue;
            return itemInventory;
        }

        return null;
    }

    protected virtual bool IsFullStack(ItemInventory itemInventory)
    {
        if (itemInventory == null) return true;
        int maxStack = this.GetMaxStack(itemInventory);
        return itemInventory.itemCount >= maxStack;

    }

    protected virtual ItemInventory CreateEmptyItem(ItemProfileSO itemProfileSO)
    {
        ItemInventory itemInventory = new ItemInventory
        {
            itemProfileSO = itemProfileSO,
            maxStack = itemProfileSO.defaultMaxStack
        };

        return itemInventory;
    }

    public virtual bool ItemCheck(ItemCode itemCode, int numbeCheck)
    {
        int totalCount = this.ItemTotalCount(itemCode);
        return totalCount >= numbeCheck;
    }

    public virtual int ItemTotalCount(ItemCode itemCode)
    {
        int totalCount = 0;
        foreach(ItemInventory itemInventory in this.items)
        {
            if (itemInventory.itemProfileSO.itemCode != itemCode) continue;
            totalCount += itemInventory.itemCount;
        }

        return totalCount;
    }

    public virtual void DeductItem(ItemCode itemCode, int deductCount)
    {
        ItemInventory itemInventory;
        int deduct;

        for(int i=this.items.Count-1; i>=0; i--)
        {
            if (deductCount <= 0) break;

            itemInventory = this.items[i];
            if (itemInventory.itemProfileSO.itemCode != itemCode) continue;

            if(deductCount > itemInventory.itemCount)
            {
                deduct = itemInventory.itemCount;
                deductCount -= itemInventory.itemCount;
            }
            else
            {
                deduct = deductCount;
                deductCount = 0;
            }

            itemInventory.itemCount -= deduct;
            if (itemInventory.itemCount == 0) this.items.RemoveAt(i);
        }
    }

    public virtual void InventoryFromJson(string jsonString)
    {
        InventoryData obj = JsonUtility.FromJson<InventoryData>(jsonString);
        this.maxSlot = obj.maxSlot;
        this.items = obj.items;
    }
}
