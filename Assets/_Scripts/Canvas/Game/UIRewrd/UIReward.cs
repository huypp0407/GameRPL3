using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIReward : _MonoBehaviour
{
    private static UIReward instance;
    public static UIReward Instance => instance;

    [SerializeField] protected UIRewradCtrl rewardCtrl;

    [SerializeField] protected bool isOpen = true;
    public bool IsOpen => isOpen;

    public ItemInventory itemInventory;
    [SerializeField] protected Transform holder;
    [SerializeField] protected Transform inventory;

    protected override void Awake()
    {
        base.Awake();
        if (UIReward.instance != null) return;
        UIReward.instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.rewardCtrl != null) return;
        this.rewardCtrl = transform.parent.GetComponent<UIRewradCtrl>();
    }

    protected override void Start()
    {
        base.Start();
        //this.rewardCtrl.AutoScroll.StartCorou();
        this.Toggle();
        //this.Toggle();
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) Open();
        else this.Close();
    }

    public virtual void Open()
    {
        this.rewardCtrl.SetAlphaCanvas(1);
        this.rewardCtrl.ScrollContent.ResizeContent();
        this.rewardCtrl.AutoScroll.StartCorou();
    }

    public virtual void Close()
    {
        this.rewardCtrl.SetAlphaCanvas(0);
    }

    public virtual void ShowReward(EnemyCtrl enemyCtrl)
    {
        List<ItemDropRate> items = enemyCtrl.EnemySO.upgradeLevels[MapLevel.Instance.LevelCurrent-1].dropList;
        RewardSpawner spawner = this.rewardCtrl.RewardSpawner;
        if (items.Count < 1) return;
        for (int i = 0; i < items.Count; i++)
        {
            spawner.SpawnItem(items[i]);
        }
    }

    public virtual void AddReward(UIItemInventory uIItemInventory)
    {
        Transform uiItem = RewardSpawner.Instance.Spawn("UIInvItemReward", transform.position, transform.rotation);

        uiItem.transform.parent = this.holder;
        uiItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(100, 0, 0);
        UIItemInventory uIItemInventory1;
        uIItemInventory1 = uiItem.GetComponent<UIItemInventory>();

        uIItemInventory1.ShowItem(uIItemInventory);

        StartCoroutine(GetReward(uiItem));

        itemInventory.itemProfileSO = ItemProfileSO.FindByItemName(uIItemInventory1.ItemName.text.ToString());
        //if(itemInventory.itemProfileSO.itemType == ItemType.Clothing)
        //{

        //}

        itemInventory.itemCount = Int32.Parse(uIItemInventory1.ItemCount.text);
        PlayerCtrl.Instance.Inventory.AddItem(itemInventory);
    }

    public virtual void AddReward(ItemInventory item)
    {
        Transform uiItem = RewardSpawner.Instance.Spawn("UIInvItemReward", transform.position, transform.rotation);
        uiItem.transform.localScale = Vector3.one;
        uiItem.transform.parent = this.holder;
        uiItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(100, 0, 0);
        UIItemInventory uIItemInventory1;
        uIItemInventory1 = uiItem.GetComponent<UIItemInventory>();

        uIItemInventory1.ShowItem(item);

        StartCoroutine(GetReward(uiItem));

        //if(itemInventory.itemProfileSO.itemType == ItemType.Clothing)
        //{

        //}
        PlayerCtrl.Instance.Inventory.AddItem(item);
    }

    IEnumerator GetReward(Transform uiItem)
    {
        yield return new WaitForSeconds(2f);
        uiItem.transform.DOMove(this.inventory.transform.position, 1).SetUpdate(true);
        uiItem.transform.DOScale(Vector3.zero, 1).SetUpdate(true);
        yield return new WaitForSeconds(1f);
        RewardSpawner.Instance.ClearItems(this.holder);
    }
}
