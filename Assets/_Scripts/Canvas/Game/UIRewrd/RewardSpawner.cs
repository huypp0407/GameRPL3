using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpawner : Spawner
{
    private static RewardSpawner instance;
    public static RewardSpawner Instance { get => instance; }
    public static string itemNormal = "UIInvItem";

    [SerializeField] protected UIRewradCtrl rewradCtrl;
    public UIRewradCtrl UIRewradCtrl => rewradCtrl;

    protected override void Awake()
    {
        base.Awake();
        if (RewardSpawner.instance != null) return;
        RewardSpawner.instance = this;
    }

    protected override void LoadHolder()
    {
        this.LoadUIRewradCtrl();

        if (this.holder != null) return;
        this.holder = this.rewradCtrl.Content;
    }

    protected virtual void LoadUIRewradCtrl()
    {
        if (this.rewradCtrl != null) return;
        this.rewradCtrl = transform.parent.GetComponent<UIRewradCtrl>();
    }

    public virtual void ClearItems(Transform transform)
    {
        foreach (Transform item in holder)
        {
            this.Despawn(item);
        }
        foreach (Transform item in transform)
        {
            this.Despawn(item);
        }
    }

    public virtual void SpawnItem(ItemDropRate item)
    {
        Transform uiItem = this.Spawn("UIInvItem", transform.position, transform.rotation);
        uiItem.localScale = new Vector3(1, 1, 1);

        UIItemInventory itemReward = uiItem.GetComponent<UIItemInventory>();
        itemReward.ShowItem(item);
    }
}
