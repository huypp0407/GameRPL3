using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    public int maxSlot = 70;
    public List<ItemInventory> items;
}