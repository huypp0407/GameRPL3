using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesPick : _MonoBehaviour
{
    [SerializeField] protected int maxSlot = 10;

    [SerializeField] protected List<AbilityInventory> abilityInventory;
    public List<AbilityInventory> AbilityInventory => abilityInventory;

    protected override void LoadComponent()
    {
        base.LoadComponent();
    }

    protected override void Awake()
    {
        base.Awake();
        this.SetUp();
    }

    protected virtual void SetUp()
    {
        
    }

    public virtual bool AddItem(AbilityInventory itemInventory)
    {
        int addCount = itemInventory.itemCount;
        ItemProfileSO itemProfileSO = itemInventory.itemProfileSO;
        ItemCode itemCode = itemProfileSO.itemCode;
        ItemType itemType = itemProfileSO.itemType;

        return AddItem(itemCode, addCount);
    }

    public virtual bool AddItem(ItemCode itemCode, int addCount)
    {
        ItemProfileSO itemProfileSO = ItemProfileSO.FindByItemCode(itemCode);

        int addRemain = addCount;
        int newcount;
        int itemMaxStack;
        int addMore;

        AbilityInventory itemExist;

        for(int i=0; i< this.maxSlot; i++)
        {
            itemExist = this.GetItemNotFullStack(itemCode);
            if(itemExist == null)
            {
                if (this.IsIventoryFull()) return false;
                itemExist = this.CreateEmptyItem(itemProfileSO);
                this.abilityInventory.Add(itemExist);
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
        if (this.abilityInventory.Count >= this.maxSlot) return true;
        return false;
    }

    protected virtual int GetMaxStack(AbilityInventory itemInventory)
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

    protected virtual AbilityInventory GetItemNotFullStack(ItemCode itemCode)
    {
        foreach(AbilityInventory itemInventory in this.abilityInventory)
        {
            if (itemCode != itemInventory.itemProfileSO.itemCode) continue;
            if (this.IsFullStack(itemInventory)) continue;
            return itemInventory;
        }

        return null;
    }

    protected virtual bool IsFullStack(AbilityInventory itemInventory)
    {
        if (itemInventory == null) return true;
        int maxStack = this.GetMaxStack(itemInventory);
        return itemInventory.itemCount >= maxStack;

    }

    protected virtual AbilityInventory CreateEmptyItem(ItemProfileSO itemProfileSO)
    {
      AbilityInventory itemInventory = new AbilityInventory
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
        foreach(AbilityInventory itemInventory in this.abilityInventory)
        {
            if (itemInventory.itemProfileSO.itemCode != itemCode) continue;
            totalCount += itemInventory.itemCount;
        }

        return totalCount;
    }

    public virtual void DeductItem(ItemCode itemCode, int deductCount)
    {
      AbilityInventory itemInventory;
        int deduct;

        for(int i=this.abilityInventory.Count-1; i>=0; i--)
        {
            if (deductCount <= 0) break;

            itemInventory = this.abilityInventory[i];
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
            if (itemInventory.itemCount == 0) this.abilityInventory.RemoveAt(i);
        }
    }
}
