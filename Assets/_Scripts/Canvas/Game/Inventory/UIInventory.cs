using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIInventory : _MonoBehaviour
{
    private static UIInventory instance;
    public static UIInventory Instance => instance;
    [SerializeField] protected bool isOpen = false;
    public bool IsOpen => isOpen;
    [SerializeField] protected InventorySort inventorySort = InventorySort.SortByName;
    [SerializeField] protected UIInventoryCtrl inventoryCtrl;
    public UIInventoryCtrl UIPlayerCtrl => inventoryCtrl;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadUIInventoryCtrl();
    }
    protected virtual void LoadUIInventoryCtrl()
    {
        if (this.inventoryCtrl != null) return;
        this.inventoryCtrl = transform.parent.GetComponent<UIInventoryCtrl>();
    }
    protected override void Awake()
    {
        base.Awake();
        if (UIInventory.instance != null) return;
        UIInventory.instance = this;
    }

    protected override void Start()
    {
        base.Start();
        this.inventoryCtrl.SetAlphaCanvas(0,0f);
    }

    public virtual void Toggle()
    {
        this.isOpen = !this.isOpen;
        if (this.isOpen) Open();
        else this.Close();
        
    }

    public virtual void Open()
    {
        RectTransform rt = this.inventoryCtrl.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(1150, 750);
        rt.anchoredPosition = new Vector3(960, 17, 0);
        Time.timeScale = 0;
        this.inventoryCtrl.SetAlphaCanvas(1, 0.3f);
        this.ShowItems();
    }

    public virtual void Close()
    {
        Time.timeScale = 1;
        this.inventoryCtrl.SetAlphaCanvas(0, 0.3f);
        if (UIInform.Instance.IsOpen) UIInform.Instance.Toggle();
        if (UIUpgrade.Instance.IsOpen) UIUpgrade.Instance.Toggle();
    }

    public virtual void ShowItems()
    {
        if (!isOpen) return;


        this.ClearItems();

        List<ItemInventory> items = PlayerCtrl.Instance.Inventory.Items;

        InvItemSpawner spawner = this.inventoryCtrl.InvItemSpawner;

        for(int i=0; i< items.Count; i++)
        {
            spawner.SpawnItem(items[i]);
        }
        this.SortItem();
    }

    protected virtual void SortItem()
    {
        switch (inventorySort)
        {
            case InventorySort.SortByName:
                this.SortByName();
                break;
            case InventorySort.SortByCount:
                break;
            default:
                break;
        }
    }

    protected virtual void SortByName()
    {
        int itemCount = this.inventoryCtrl.Content.childCount;

        Transform currentItem, nextItem;
        UIItemInventory currentUItem, nextIUIItem;
        ItemProfileSO currentProfile, nextProfile;
        string currentName, nextName;
        bool isSorting = false;

        for (int i = 0; i < itemCount - 1; i++)
        {
            currentItem = this.inventoryCtrl.Content.GetChild(i);
            nextItem = this.inventoryCtrl.Content.GetChild(i+1);

            currentUItem = currentItem.GetComponent<UIItemInventory>();
            nextIUIItem = nextItem.GetComponent<UIItemInventory>();

            currentProfile = currentUItem.itemInventory.itemProfileSO;
            nextProfile = nextIUIItem.itemInventory.itemProfileSO;

            currentName = currentProfile.itemName;
            nextName = nextProfile.itemName;

            int compare = string.Compare(currentName, nextName);

            if(compare == 1)
            {
                this.SwapItem(currentItem, nextItem);
                isSorting = true;
            }
        }

        if (isSorting) this.SortByName();
    }

    protected virtual void SwapItem(Transform currentItem, Transform nextItem)
    {
        int currentIndex = currentItem.GetSiblingIndex();
        int nextIndex = nextItem.GetSiblingIndex();

        currentItem.SetSiblingIndex(nextIndex);
        nextItem.SetSiblingIndex(currentIndex);
    }

    protected virtual void ClearItems()
    {
        this.inventoryCtrl.InvItemSpawner.ClearItems();
    }
}
