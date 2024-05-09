using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIDailyBonus : _MonoBehaviour
{
    private static UIDailyBonus instance;
    public static UIDailyBonus Instance => instance;
    [SerializeField] protected bool isOpen = false;
    public bool IsOpen => isOpen;
    // [SerializeField] protected InventorySort inventorySort = InventorySort.SortByName;
    [SerializeField] protected UIDailyBonusCtrl dailyBonusCtrl;
    public UIDailyBonusCtrl DailyBonusCtrl => dailyBonusCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUIInventoryCtrl();
    }
    protected virtual void LoadUIInventoryCtrl()
    {
        if (this.dailyBonusCtrl != null) return;
        this.dailyBonusCtrl = transform.parent.GetComponent<UIDailyBonusCtrl>();
    }
    protected override void Awake()
    {
        base.Awake();
        if (UIDailyBonus.instance != null) return;
        UIDailyBonus.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.dailyBonusCtrl.SetAlphaCanvas(0,0f);
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) Open();
        else this.Close();
        
    }

    public virtual void Open()
    {
        this.dailyBonusCtrl.SetAlphaCanvas(1, 0.3f);
        // this.ShowItems();
    }

    public virtual void Close()
    {
        this.dailyBonusCtrl.SetAlphaCanvas(0, 0.3f);
    }

    public virtual void ShowItems()
    {
        if (!isOpen) return;


        this.ClearItems();

        List<ItemInventory> items = PlayerCtrl.Instance.Inventory.Items;

        DailyBonusSpawner spawner = this.dailyBonusCtrl.DailyBonusSpawner;

        for(int i=0; i< items.Count; i++)
        {
            spawner.SpawnItem(items[i]);
        }
    }


    protected virtual void ClearItems()
    {
        this.dailyBonusCtrl.DailyBonusSpawner.ClearItems();
    }
}
