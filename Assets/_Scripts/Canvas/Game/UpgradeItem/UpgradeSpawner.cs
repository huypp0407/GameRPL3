using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeSpawner : Spawner
{
    private static UpgradeSpawner instance;
    public static UpgradeSpawner Instance { get => instance; }
    public static string itemNormal = "UIInvItem";

    public List<Transform> holders;

    protected override void Awake()
    {
        base.Awake();
        if (UpgradeSpawner.instance != null) return;
        UpgradeSpawner.instance = this;
    }

    public virtual void ClearItems(int index)
    {
        if (holders[index].childCount < 1) return;
        foreach(Transform item in holders[index])
        {
            this.Despawn(item);
        }
    }

    public virtual void SpawnItem(ItemRecipeIngredient item, int index)
    {
        this.holder = this.holders[index];
        Transform uiItem = this.Spawn("UIItemRecipeIngredient", transform.position, transform.rotation);
        uiItem.localScale = new Vector3(1, 1, 1);


        UIItemRecipeIngredient itemRecipeIngredient = uiItem.GetComponent<UIItemRecipeIngredient>();
        itemRecipeIngredient.ShowItem(item);
    }
}
