using System;
using UnityEngine;

[Serializable]
public class ItemInventory
{
    public ItemProfileSO itemProfileSO;
    public int itemCount = 0;
    public int maxStack = 7;
    public int upgradeLevel = 0;
    public GameObject button;

    public virtual ItemInventory Clone()
    {
        ItemInventory item = new ItemInventory
        {
            itemProfileSO = this.itemProfileSO,
            itemCount = this.itemCount,
            maxStack = this.maxStack,
            upgradeLevel = this.upgradeLevel
        };
        return item;
    }
}
