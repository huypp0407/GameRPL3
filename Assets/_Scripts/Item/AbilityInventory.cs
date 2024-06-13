using System;
using UnityEngine;

[Serializable]
public class AbilityInventory
{
    public ItemProfileSO itemProfileSO;
    public int itemCount = 0;
    public int maxStack = 7;
    public int upgradeLevel = 0;

    public virtual AbilityInventory Clone()
    {
      AbilityInventory item = new AbilityInventory
        {
            itemProfileSO = this.itemProfileSO,
            itemCount = this.itemCount,
            maxStack = this.maxStack,
            upgradeLevel = this.upgradeLevel
        };
        return item;
    }
}
