using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DropTest : MonoBehaviour
{
    public EnemyCtrl enemyCtrl;
    public int dropCount = 0;
    public List<ItemDropCount> itemDropCounts = new List<ItemDropCount>();
    private void Start()
    {
        InvokeRepeating(nameof(Droping), 0.01f, 0.5f);

    }

    protected virtual void Droping()
    {
        Vector3 dropPos = transform.position;
        Quaternion dropRot = transform.rotation;
        int currentLvel = MapLevel.Instance.LevelCurrent - 1;
        List<ItemDropRate> itemDropRates = ItemDropSpawner.Instance.Drop(this.enemyCtrl.EnemySO.upgradeLevels[currentLvel].dropList, dropPos, dropRot);
        dropCount++;
        ItemDropCount itemDropCount;
        foreach (ItemDropRate itemDropRate in itemDropRates)
        {
            itemDropCount = this.itemDropCounts.Find(i => i.itemName == itemDropRate.itemSO.itemName);
            if(itemDropCount == null)
            {
                itemDropCount = new ItemDropCount();
                itemDropCount.itemName = itemDropRate.itemSO.itemName;
                this.itemDropCounts.Add(itemDropCount);
            }

            itemDropCount.count++;
            itemDropCount.rate = (float)Math.Round((float)itemDropCount.count / this.dropCount, 2);
        }

    }
}


[Serializable]
public class ItemDropCount
{
    public string itemName;
    public int count;
    public float rate;
}