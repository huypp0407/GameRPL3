using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : _MonoBehaviour
{
    [SerializeField] private static Character instance;
    public static Character Instance => instance;

    [SerializeField] protected int maxSlot = 70;
    [SerializeField] protected List<ItemProfileSO> items;
    public List<ItemProfileSO> Items => items;

    protected override void Awake()
    {
        base.Awake();
        if (Character.instance != null) return;
        Character.instance = this;
    }

    public virtual bool AddClothing(ItemProfileSO itemInventory)
    {
        if (this.IsIventoryFull()) return false;
        if (!this.CheckClothing(itemInventory)) return false;
        this.items.Add(itemInventory);
        return true;
    }

    protected virtual bool CheckClothing(ItemProfileSO itemInventory)
    {
        foreach(ItemProfileSO itemProfileSO in items)
        {
            if (itemProfileSO.itemName == itemInventory.itemName) return false;
        }
        return true;
    }

    protected virtual bool IsIventoryFull()
    {
        if (this.items.Count >= this.maxSlot) return true;
        return false;
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
        
    public virtual void InventoryFromJson(string jsonString)
    {
        CharacterData obj = JsonUtility.FromJson<CharacterData>(jsonString);
        this.maxSlot = obj.maxSlot;
        this.items = obj.items;
    }
}